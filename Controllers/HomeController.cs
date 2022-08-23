using System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCreate.Migrations;
using MyCreate.model;

namespace MyCreate.Controllers
{
    

    public class HomeController : Controller
    {
        NewsDb db;
        public HomeController(NewsDb _db)
        {
            db = _db;
        }
       
        public IActionResult Index()
        {
            var tak = db.categoties.ToList();
            return View(tak);
        }

        public IActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        
        public IActionResult allmasaj(ContactUs mode)
        {
            if (ModelState.IsValid)
            {
                db.contactUs.Add(mode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Contact", mode);
        }
      
        public IActionResult readmasaj()
        {
            return View(db.contactUs.ToList());
            
        }
        
        public IActionResult Teammember()
        {

            return View(db.teammembers.ToList());
        }
      
        public IActionResult anahabers()
        {

            return View(db.anahaberleri.ToList());
        }
       
        public IActionResult News(int id)
        {
            Categoty ctu = db.categoties.Find(id);
            ViewBag.name = ctu.Name;
           var news= db.news.Where(x => x.categoeyId == id).OrderByDescending(x => x.Date).ToList();
            return View(news);
        }


        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult Deletenews(int id)
        //{
        //    var deletenew = db.news.Find(id);
        //    db.news.Remove(deletenew);
        //    db.SaveChanges();
        //    return RedirectToAction("News");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
