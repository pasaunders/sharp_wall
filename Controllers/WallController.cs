using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;
using wall.Models;

namespace wall.Controllers
{
    public class WallController : Controller
    {
        public IActionResult ShowWall()
        {
            ViewBag.Messages = DbConnector.Query(@"SELECT messages.id AS message_id, messages.content, messages.created_at, users.first_name, users.last_name 
                             FROM messages JOIN users ON messages.users_id = users.id");
            ViewBag.Comments = DbConnector.Query(@"SELECT comments.id AS comment_id, comments.content, comments.created_at, users.name, comments.messages_id
                              FROM comments JOIN messages ON comments.messages_id = messages.id
                              JOIN users ON comments.users_id = users.id");
            ViewBag.User = DbConnector.Query($"SELECT name FROM users Where users.id = {(int)HttpContext.Session.GetInt32("id")}")[0];
            return View();
        }
        [HttpPost]
        [Route("makeComment")]
        public IActionResult makeComment(Comment newComment)
        {
            if (ModelState.IsValid)
            {
                DbConnector.Execute($@"INSERT INTO comments (comment, created_at, updated_at, messages_id, users_id) 
                    VALUES ('{newComment.comment}', '{DateTime.Now}', '{DateTime.Now}', {newComment.messagesId}, {(int)HttpContext.Session.GetInt32("id")})");
                return RedirectToAction("Index");
            }
            return View("Index");
        }
        [HttpPost]
        [Route("makeMessage")]
        public IActionResult makeMessage(Message newMessage)
        {
            if (ModelState.IsValid)
            {
                DbConnector.Execute($@"INSERT INTO messages (message, created_at, updated_at, users_id) 
                    VALUES ('{newMessage.message}', '{DateTime.Now}', '{DateTime.Now}', {(int)HttpContext.Session.GetInt32("id")})");
                return RedirectToAction("Index");
            }
            return View("Index");

        }
    }
}