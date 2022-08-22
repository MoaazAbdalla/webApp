using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCreate.Models;
using MyCreate.model;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MyCreate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class anahabersController : Controller
    {
        private readonly NewsDb _context;
        private IHostingEnvironment host;

        public anahabersController(NewsDb context, IHostingEnvironment hostin)
        {
            _context = context;
            host = hostin;
        }

        // GET: AdminPanel/anahabers
        public async Task<IActionResult> Index()
        {
            return View(await _context.anahaberleri.ToListAsync());
        }

        // GET: AdminPanel/anahabers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anahaber = await _context.anahaberleri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anahaber == null)
            {
                return NotFound();
            }

            return View(anahaber);
        }

        // GET: AdminPanel/anahabers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/anahabers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(anahaber anahaber)
        {
            if (ModelState.IsValid)
            {
                voiduploadsphoto(anahaber);
                _context.Add(anahaber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anahaber);
        }

        // GET: AdminPanel/anahabers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anahaber = await _context.anahaberleri.FindAsync(id);
            if (anahaber == null)
            {
                return NotFound();
            }
            return View(anahaber);
        }

        // POST: AdminPanel/anahabers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, anahaber anahaber)
        {
            if (id != anahaber.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    voiduploadsphoto(anahaber);
                    _context.Update(anahaber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!anahaberExists(anahaber.Id))
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
            return View(anahaber);
        }

        // GET: AdminPanel/anahabers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anahaber = await _context.anahaberleri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anahaber == null)
            {
                return NotFound();
            }

            return View(anahaber);
        }

        // POST: AdminPanel/anahabers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anahaber = await _context.anahaberleri.FindAsync(id);
            _context.anahaberleri.Remove(anahaber);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool anahaberExists(int id)
        {
            return _context.anahaberleri.Any(e => e.Id == id);
        }

        void voiduploadsphoto(anahaber haber)
        {
            if (haber.File != null)
            {
                string uploadfile = Path.Combine(host.WebRootPath, "img/news");
                string uniname = Guid.NewGuid() + ".jpg";
                string filepath = Path.Combine(uploadfile, uniname);

                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    haber.File.CopyTo(fileStream);
                }
                haber.image = uniname;
            }

        }
    }
}
