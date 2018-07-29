using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Students;

namespace KisVuzDotNetCore2.Controllers
{
    public class VedomostiController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public VedomostiController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Vedomosti
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.Vedomosti
                .Include(v => v.EduYear)
                .Include(v => v.SemestrName)
                .Include(v => v.StudentGroup.EduKurs)
                .Include(v => v.VedomostStudentMarks);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: Vedomosti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vedomost = await _context.Vedomosti
                .Include(v => v.EduYear)
                .Include(v => v.SemestrName)
                .Include(v => v.StudentGroup)
                .SingleOrDefaultAsync(m => m.VedomostId == id);
            if (vedomost == null)
            {
                return NotFound();
            }

            return View(vedomost);
        }

        // GET: Vedomosti/Create
        public IActionResult Create()
        {
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName");
            ViewData["SemestrNameId"] = new SelectList(_context.SemestrNames, "SemestrNameId", "SemestrNameName");
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups.Include(g=>g.EduKurs), "StudentGroupId", "StudentGroupName");
            return View();
        }

        // POST: Vedomosti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VedomostId,EduYearId,StudentGroupId,SemestrNameId,DisciplineName")] Vedomost vedomost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vedomost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName", vedomost.EduYearId);
            ViewData["SemestrNameId"] = new SelectList(_context.SemestrNames, "SemestrNameId", "SemestrNameName", vedomost.SemestrNameId);
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups.Include(g => g.EduKurs), "StudentGroupId", "StudentGroupName", vedomost.StudentGroupId);
            return View(vedomost);
        }

        public async Task<IActionResult> CreateVedomostStudentMarks(int id)
        {
            var vedomost = await _context.Vedomosti
                .Include(v => v.EduYear)
                .Include(v => v.SemestrName)
                .Include(v => v.StudentGroup.EduKurs)
                .Include(v => v.StudentGroup.Students)
                .Include(v => v.VedomostStudentMarks)
                .SingleOrDefaultAsync(v=>v.VedomostId == id);

            if(vedomost!=null && vedomost.VedomostStudentMarks.Count==0)
            {
                foreach (var student in vedomost.StudentGroup.Students)
                {
                    var studentMark = new VedomostStudentMark();
                    studentMark.StudentId = student.StudentId;
                    studentMark.VedomostStudentMarkNameId = (int)VedomostStudentMarkNameEnum.NeYavNeAtt;
                    vedomost.VedomostStudentMarks.Add(studentMark);                    
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Передаёт в представление таблицу с оценками по текущей ведомости
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> VedomostStudentMarks(int id)
        {
            var vedomost = await _context.Vedomosti
                .Include(v => v.EduYear)
                .Include(v => v.SemestrName)
                .Include(v => v.StudentGroup.EduKurs)
                .Include(v => v.StudentGroup.Students)
                .Include(v => v.VedomostStudentMarks)
                    .ThenInclude(m=>m.VedomostStudentMarkName)
                .SingleOrDefaultAsync(v => v.VedomostId == id);

            if (vedomost != null)
            {
                return View(vedomost);
            }

            return RedirectToAction(nameof(Index));
        }
             

        // GET: Vedomosti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vedomost = await _context.Vedomosti.SingleOrDefaultAsync(m => m.VedomostId == id);
            if (vedomost == null)
            {
                return NotFound();
            }
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName", vedomost.EduYearId);
            ViewData["SemestrNameId"] = new SelectList(_context.SemestrNames, "SemestrNameId", "SemestrNameName", vedomost.SemestrNameId);
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups.Include(g => g.EduKurs), "StudentGroupId", "StudentGroupName", vedomost.StudentGroupId);
            return View(vedomost);
        }

        // POST: Vedomosti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VedomostId,EduYearId,StudentGroupId,SemestrNameId,DisciplineName")] Vedomost vedomost)
        {
            if (id != vedomost.VedomostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vedomost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VedomostExists(vedomost.VedomostId))
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
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName", vedomost.EduYearId);
            ViewData["SemestrNameId"] = new SelectList(_context.SemestrNames, "SemestrNameId", "SemestrNameName", vedomost.SemestrNameId);
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups.Include(g => g.EduKurs), "StudentGroupId", "StudentGroupName", vedomost.StudentGroupId);
            return View(vedomost);
        }

        // GET: Vedomosti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vedomost = await _context.Vedomosti
                .Include(v => v.EduYear)
                .Include(v => v.SemestrName)
                .Include(v => v.StudentGroup.EduKurs)
                .SingleOrDefaultAsync(m => m.VedomostId == id);
            if (vedomost == null)
            {
                return NotFound();
            }

            return View(vedomost);
        }

        // POST: Vedomosti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vedomost = await _context.Vedomosti.SingleOrDefaultAsync(m => m.VedomostId == id);
            _context.Vedomosti.Remove(vedomost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VedomostExists(int id)
        {
            return _context.Vedomosti.Any(e => e.VedomostId == id);
        }
    }
}
