using System;
using System.Collections.Generic;
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
    public class TeammembersController : Controller
    {
        private readonly NewsDb _context;
        private IHostingEnvironment host;

        public TeammembersController(NewsDb context , IHostingEnvironment hostt)
        {
            _context = context;
            host = hostt;
        }

        // GET: Teammembers
        public async Task<IActionResult> Index()
        {
            return View(await _context.teammembers.ToListAsync());
        }

        // GET: Teammembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teammember = await _context.teammembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teammember == null)
            {
                return NotFound();
            }

            return View(teammember);
        }

        // GET: Teammembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teammembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teammember teammember)
        {
            if (ModelState.IsValid)
            {
                voiduploadsphoto(teammember);
                _context.Add(teammember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teammember);
        }

        // GET: Teammembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teammember = await _context.teammembers.FindAsync(id);
            if (teammember == null)
            {
                return NotFound();
            }
            return View(teammember);
        }

        // POST: Teammembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Teammember teammember)
        {
            if (id != teammember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    voiduploadsphoto(teammember);
                    _context.Update(teammember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeammemberExists(teammember.Id))
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
            return View(teammember);
        }

        // GET: Teammembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teammember = await _context.teammembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teammember == null)
            {
                return NotFound();
            }

            return View(teammember);
        }

        // POST: Teammembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teammember = await _context.teammembers.FindAsync(id);
            _context.teammembers.Remove(teammember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeammemberExists(int id)
        {
            return _context.teammembers.Any(e => e.Id == id);
        }

        
        void voiduploadsphoto(Teammember model)
        {
            if (model.File != null)
            {
                string uploadfile = Path.Combine(host.WebRootPath, "img/news");
                string uniname =  Guid.NewGuid() + ".jpg";
                string filepath = Path.Combine(uploadfile, uniname);

                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    model.File.CopyTo(fileStream);
                }
                model.Image = uniname;
            }

        }
    }
}
