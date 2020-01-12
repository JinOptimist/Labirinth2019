using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LabirinthCore.Labirinth;
using Microsoft.AspNetCore.Mvc;
using WebLab.Models;

namespace WebLab.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var dungeon = new Dungeon(20, 20);
            return View(dungeon.CurrentLevel);
        }
    }
}
