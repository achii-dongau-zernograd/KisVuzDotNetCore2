using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Priem;

namespace KisVuzDotNetCore2.Controllers
{
    public class PriemExamsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public PriemExamsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: PriemExams
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.PriemExam.Include(p => p.EduNapravl);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: PriemExams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priemExam = await _context.PriemExam
                .Include(p => p.EduNapravl)
                .SingleOrDefaultAsync(m => m.PriemExamId == id);
            if (priemExam == null)
            {
                return NotFound();
            }

            return View(priemExam);
        }

        // GET: PriemExams/Create
        public IActionResult Create()
        {
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlName");
            return View();
        }

        // POST: PriemExams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PriemExamId,EduNapravlId,VstupIsp,MinKol,FormProv")] PriemExam priemExam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priemExam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", priemExam.EduNapravlId);
            return View(priemExam);
        }

        // GET: PriemExams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priemExam = await _context.PriemExam.SingleOrDefaultAsync(m => m.PriemExamId == id);
            if (priemExam == null)
            {
                return NotFound();
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlName", priemExam.EduNapravlId);
            return View(priemExam);
        }

        // POST: PriemExams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PriemExamId,EduNapravlId,VstupIsp,MinKol,FormProv")] PriemExam priemExam)
        {
            if (id != priemExam.PriemExamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priemExam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriemExamExists(priemExam.PriemExamId))
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
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", priemExam.EduNapravlId);
            return View(priemExam);
        }

        // GET: PriemExams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priemExam = await _context.PriemExam
                .Include(p => p.EduNapravl)
                .SingleOrDefaultAsync(m => m.PriemExamId == id);
            if (priemExam == null)
            {
                return NotFound();
            }

            return View(priemExam);
        }

        // POST: PriemExams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priemExam = await _context.PriemExam.SingleOrDefaultAsync(m => m.PriemExamId == id);
            _context.PriemExam.Remove(priemExam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriemExamExists(int id)
        {
            return _context.PriemExam.Any(e => e.PriemExamId == id);
        }
    }
}
