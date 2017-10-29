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
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.Get("id") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ShowWall", "Wall")
            }
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(User registerData)
        {
            if(ModelState.IsValid)
            {
                DbConnector.Execute($@"INSERT INTO user (name, email, password, created_at, updated_at) VALUES ('{registerData.name}', '{registerData.email}', '{registerData.password}', {DateTime.Now}, {DateTime.Now});");
                return RedirectToAction("")
            }
            return View("Index");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login loginData)
        {
            if (ModelState.IsValid)
            {
                List<Dictionary<string,object>> userCheck = DbConnector.Query($"SELECT * FROM users WHERE name='{loginData.firstName}';");
                if((userCheck.Count == 0) || ((string)userCheck[0]["password"] != loginData.password))
                {
                    return View("Index");
                }    
                HttpContext.Session.SetInt32("id", (int)userCheck[0]["id"])
            }
            return View("Index");
        }
    }
}
