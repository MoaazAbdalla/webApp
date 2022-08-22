using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCreate.model;

namespace MyCreate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize]
    public class defaultController : Controller
    {
        NewsDb db;
        public defaultController(NewsDb database)
        {
            db = database;
        }
        public IActionResult Index()
        {
            int newssayi = db.news.Count();
            int Teamsayi = db.teammembers.Count();
            int connectsayi = db.contactUs.Count();
            return View(new AdminPanel
            {

                news = newssayi,
                Team = Teamsayi,
                connect = connectsayi,

            })  ;

        }
    }
    public class AdminPanel
    {
        public int news { get; set; }
        public int Team { get; set; }
        public int connect { get; set; }
    }
}
