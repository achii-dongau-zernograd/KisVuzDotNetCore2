using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Authorization;
using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Struct;

namespace KisVuzDotNetCore2.Controllers.Users
{
    [Authorize(Roles ="Администраторы")]
    public class TeacherMetodKomissiyaController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly ISelectListRepository _selectListRepository;
        private readonly IMetodKomissiyaRepository _metodKomissiyaRepository;

        public TeacherMetodKomissiyaController(AppIdentityDBContext context,
            ISelectListRepository selectListRepository,
            IMetodKomissiyaRepository metodKomissiyaRepository)
        {
            _context = context;
            _selectListRepository = selectListRepository;
            _metodKomissiyaRepository = metodKomissiyaRepository;
        }

        // GET: TeacherMetodKomissiya
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.TeacherMetodKomissiya
                .Include(t => t.MetodKomissiya)
                .Include(t => t.Teacher.AppUser);
            return View(await appIdentityDBContext.ToListAsync());
        }               

        // GET: TeacherMetodKomissiya/Create
        public IActionResult Create()
        {
            ViewData["MetodKomissiyaId"] = _selectListRepository.GetSelectListMetodKomissiyaNames();
            ViewData["TeacherId"] = _selectListRepository.GetSelectListTeacherFio();
            return View();
        }

        // POST: TeacherMetodKomissiya/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherMetodKomissiyaId,TeacherId,MetodKomissiyaId")] TeacherMetodKomissiya teacherMetodKomissiya)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherMetodKomissiya);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MetodKomissiyaId"] = _selectListRepository.GetSelectListMetodKomissiyaNames(teacherMetodKomissiya.MetodKomissiyaId);
            ViewData["TeacherId"] = _selectListRepository.GetSelectListTeacherFio(teacherMetodKomissiya.TeacherId);            
            return View(teacherMetodKomissiya);
        }

        // GET: TeacherMetodKomissiya/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherMetodKomissiya = await _context.TeacherMetodKomissiya.SingleOrDefaultAsync(m => m.TeacherMetodKomissiyaId == id);
            if (teacherMetodKomissiya == null)
            {
                return NotFound();
            }
            ViewData["MetodKomissiyaId"] = _selectListRepository.GetSelectListMetodKomissiyaNames(teacherMetodKomissiya.MetodKomissiyaId);
            ViewData["TeacherId"] = _selectListRepository.GetSelectListTeacherFio(teacherMetodKomissiya.TeacherId);
            return View(teacherMetodKomissiya);
        }

        // POST: TeacherMetodKomissiya/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherMetodKomissiyaId,TeacherId,MetodKomissiyaId")] TeacherMetodKomissiya teacherMetodKomissiya)
        {
            if (id != teacherMetodKomissiya.TeacherMetodKomissiyaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherMetodKomissiya);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherMetodKomissiyaExists(teacherMetodKomissiya.TeacherMetodKomissiyaId))
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
            ViewData["MetodKomissiyaId"] = _selectListRepository.GetSelectListMetodKomissiyaNames(teacherMetodKomissiya.MetodKomissiyaId);
            ViewData["TeacherId"] = _selectListRepository.GetSelectListTeacherFio(teacherMetodKomissiya.TeacherId);
            return View(teacherMetodKomissiya);
        }

        // GET: TeacherMetodKomissiya/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherMetodKomissiya = await _context.TeacherMetodKomissiya
                .Include(t => t.MetodKomissiya)
                .Include(t => t.Teacher)
                .SingleOrDefaultAsync(m => m.TeacherMetodKomissiyaId == id);
            if (teacherMetodKomissiya == null)
            {
                return NotFound();
            }

            return View(teacherMetodKomissiya);
        }

        // POST: TeacherMetodKomissiya/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherMetodKomissiya = await _context.TeacherMetodKomissiya.SingleOrDefaultAsync(m => m.TeacherMetodKomissiyaId == id);
            _context.TeacherMetodKomissiya.Remove(teacherMetodKomissiya);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherMetodKomissiyaExists(int id)
        {
            return _context.TeacherMetodKomissiya.Any(e => e.TeacherMetodKomissiyaId == id);
        }
    }
}
