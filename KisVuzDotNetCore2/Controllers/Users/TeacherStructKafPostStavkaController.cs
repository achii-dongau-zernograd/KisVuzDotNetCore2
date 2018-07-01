using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Users;

namespace KisVuzDotNetCore2.Controllers.Users
{
    public class TeacherStructKafPostStavkaController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public TeacherStructKafPostStavkaController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: TeacherStructKafPostStavka
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.TeacherStructKafPostStavka
                .Include(t => t.Post)
                .Include(t => t.StructKaf.StructSubvision)
                .Include(t => t.Teacher)
                .OrderBy(t=>t.Teacher.TeacherFio)
                .ThenBy(t=>t.StructKafId)
                .ThenBy(t=>t.StavkaDate);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: TeacherStructKafPostStavka/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherStructKafPostStavka = await _context.TeacherStructKafPostStavka
                .Include(t => t.Post)
                .Include(t => t.StructKaf.StructSubvision)
                .Include(t => t.Teacher)
                .SingleOrDefaultAsync(m => m.TeacherStructKafPostStavkaId == id);
            if (teacherStructKafPostStavka == null)
            {
                return NotFound();
            }

            return View(teacherStructKafPostStavka);
        }

        // GET: TeacherStructKafPostStavka/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName");
            ViewData["StructKafId"] = new SelectList(_context.StructKafs.Include(sk=>sk.StructSubvision), "StructKafId", "StructSubvision.StructSubvisionName");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio");
            return View();
        }

        // POST: TeacherStructKafPostStavka/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherStructKafPostStavkaId,TeacherId,StructKafId,PostId,StavkaDate,Stavka")] TeacherStructKafPostStavka teacherStructKafPostStavka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherStructKafPostStavka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName", teacherStructKafPostStavka.PostId);
            ViewData["StructKafId"] = new SelectList(_context.StructKafs.Include(sk => sk.StructSubvision), "StructKafId", "StructSubvision.StructSubvisionName", teacherStructKafPostStavka.StructKafId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio", teacherStructKafPostStavka.TeacherId);
            return View(teacherStructKafPostStavka);
        }

        // GET: TeacherStructKafPostStavka/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherStructKafPostStavka = await _context.TeacherStructKafPostStavka.SingleOrDefaultAsync(m => m.TeacherStructKafPostStavkaId == id);
            if (teacherStructKafPostStavka == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName", teacherStructKafPostStavka.PostId);
            ViewData["StructKafId"] = new SelectList(_context.StructKafs.Include(sk => sk.StructSubvision), "StructKafId", "StructSubvision.StructSubvisionName", teacherStructKafPostStavka.StructKafId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio", teacherStructKafPostStavka.TeacherId);
            return View(teacherStructKafPostStavka);
        }

        // POST: TeacherStructKafPostStavka/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherStructKafPostStavkaId,TeacherId,StructKafId,PostId,StavkaDate,Stavka")] TeacherStructKafPostStavka teacherStructKafPostStavka)
        {
            if (id != teacherStructKafPostStavka.TeacherStructKafPostStavkaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherStructKafPostStavka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherStructKafPostStavkaExists(teacherStructKafPostStavka.TeacherStructKafPostStavkaId))
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
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName", teacherStructKafPostStavka.PostId);
            ViewData["StructKafId"] = new SelectList(_context.StructKafs.Include(sk => sk.StructSubvision), "StructKafId", "StructSubvision.StructSubvisionName", teacherStructKafPostStavka.StructKafId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio", teacherStructKafPostStavka.TeacherId);
            return View(teacherStructKafPostStavka);
        }

        // GET: TeacherStructKafPostStavka/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherStructKafPostStavka = await _context.TeacherStructKafPostStavka
                .Include(t => t.Post)
                .Include(t => t.StructKaf.StructSubvision)
                .Include(t => t.Teacher)
                .SingleOrDefaultAsync(m => m.TeacherStructKafPostStavkaId == id);
            if (teacherStructKafPostStavka == null)
            {
                return NotFound();
            }

            return View(teacherStructKafPostStavka);
        }

        // POST: TeacherStructKafPostStavka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherStructKafPostStavka = await _context.TeacherStructKafPostStavka.SingleOrDefaultAsync(m => m.TeacherStructKafPostStavkaId == id);
            _context.TeacherStructKafPostStavka.Remove(teacherStructKafPostStavka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherStructKafPostStavkaExists(int id)
        {
            return _context.TeacherStructKafPostStavka.Any(e => e.TeacherStructKafPostStavkaId == id);
        }
    }
}
