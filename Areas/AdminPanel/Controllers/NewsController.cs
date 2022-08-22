using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCreate.model;


namespace MyCreate.Controllers
{
    [Area("AdminPanel")]
    public class NewsController : Controller
    {
        private IHostingEnvironment host;
        private readonly NewsDb db;

        public NewsController(NewsDb context, IHostingEnvironment hosting)
        {
            db = context;
            host = hosting;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var newsDb = db.news.Include(n => n.categoty);
            return View(await newsDb.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await db.news
                .Include(n => n.categoty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news)
        {
            if (ModelState.IsValid)
            {
                voiduploadsphoto(news);
                db.Add(news);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoeyId"] = new SelectList(db.categoties, "Id", "Name", news.categoeyId);
            return View(news);
        }
        public IActionResult Create()
        {
            ViewData["categoeyId"] = new SelectList(db.categoties, "Id", "Name");
            return View();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await db.news.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["categoeyId"] = new SelectList(db.categoties, "Id", "Name", news.categoeyId);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                 voiduploadsphoto(news);
                try
                {
                    db.Update(news);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoeyId"] = new SelectList(db.categoties, "Id", "Name", news.categoeyId);
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await db.news
                .Include(n => n.categoty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await db.news.FindAsync(id);
            db.news.Remove(news);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return db.news.Any(e => e.Id == id);
        }
        void voiduploadsphoto(News model)
        {
            if (model.File != null)
            {
                string uploadfile = Path.Combine(host.WebRootPath, "img/news");
                string uniname = Guid.NewGuid() + ".jpg";
                string filepath = Path.Combine(uploadfile, uniname);

                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    model.File.CopyTo(fileStream);
                }
                model.img = uniname;
            }

        }
    }
}
