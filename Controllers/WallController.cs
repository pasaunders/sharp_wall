using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace wall.Controllers
{
    public class WallController : Controller
    {
        public IActionResult ShowWall()
        {
            return View()
        }
    }
}