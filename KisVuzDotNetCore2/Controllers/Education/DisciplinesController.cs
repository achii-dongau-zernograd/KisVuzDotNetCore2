﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Controllers.Education
{
    public class DisciplinesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public DisciplinesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Disciplines
        public async Task<IActionResult> Index(int? BlokDisciplChastId)
        {
            if (BlokDisciplChastId != null)
            {
                var appIdentityDBContext = _context.Disciplines
                .Include(d => d.BlokDisciplChast)
                .Include(d => d.DisciplineName)
                .Where(d => d.BlokDisciplChastId == BlokDisciplChastId);
                ViewBag.BlokDisciplChastId = BlokDisciplChastId;
                return View(await appIdentityDBContext.ToListAsync());
            }
            else
            {
                var appIdentityDBContext = _context.Disciplines
                .Include(d => d.BlokDisciplChast)
                .Include(d => d.DisciplineName);
                return View(await appIdentityDBContext.ToListAsync());
            }
            
        }

        // GET: Disciplines/Details/5
        public async Task<IActionResult> Details(int? id, int? BlokDisciplChastId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines
                .Include(d => d.BlokDisciplChast)
                .Include(d => d.DisciplineName)
                .SingleOrDefaultAsync(m => m.DisciplineId == id);
            if (discipline == null)
            {
                return NotFound();
            }

            ViewBag.BlokDisciplChastId = BlokDisciplChastId;
            return View(discipline);
        }

        // GET: Disciplines/Create
        public IActionResult Create(int? BlokDisciplChastId)
        {
            if (BlokDisciplChastId != null)
            {
                ViewBag.BlokDisciplChastId = BlokDisciplChastId;
                ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameName");
                return View();
            }
            else
            {
                return NotFound(); 
            }
            
        }

        // POST: Disciplines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisciplineId,DisciplineCode,DisciplineNameId,BlokDisciplChastId")] Discipline discipline, int? BlokDisciplChastId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discipline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { BlokDisciplChastId});
            }
            ViewData["BlokDisciplChastId"] = new SelectList(_context.BlokDisciplChast, "BlokDisciplChastId", "BlokDisciplChastId", discipline.BlokDisciplChastId);
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameId", discipline.DisciplineNameId);
            return View(discipline);
        }

        // GET: Disciplines/Edit/5
        public async Task<IActionResult> Edit(int? id, int? BlokDisciplChastId)
        {
            if (id == null || BlokDisciplChastId == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines.SingleOrDefaultAsync(m => m.DisciplineId == id);
            if (discipline == null)
            {
                return NotFound();
            }
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameName", discipline.DisciplineNameId);
            ViewBag.BlokDisciplChastId = BlokDisciplChastId;
            return View(discipline);
        }

        // POST: Disciplines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisciplineId,DisciplineCode,DisciplineNameId,BlokDisciplChastId")] Discipline discipline, int BlokDisciplChastId)
        {
            if (id != discipline.DisciplineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discipline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplineExists(discipline.DisciplineId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { BlokDisciplChastId});
            }
            ViewData["BlokDisciplChastId"] = new SelectList(_context.BlokDisciplChast, "BlokDisciplChastId", "BlokDisciplChastId", discipline.BlokDisciplChastId);
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameId", discipline.DisciplineNameId);
            return View(discipline);
        }

        // GET: Disciplines/Delete/5
        public async Task<IActionResult> Delete(int? id, int? BlokDisciplChastId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines
                .Include(d => d.BlokDisciplChast)
                .Include(d => d.DisciplineName)
                .SingleOrDefaultAsync(m => m.DisciplineId == id);
            if (discipline == null)
            {
                return NotFound();
            }

            ViewBag.BlokDisciplChastId = BlokDisciplChastId;
            return View(discipline);
        }

        // POST: Disciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discipline = await _context.Disciplines.SingleOrDefaultAsync(m => m.DisciplineId == id);
            _context.Disciplines.Remove(discipline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = discipline.BlokDisciplChastId});
        }

        private bool DisciplineExists(int id)
        {
            return _context.Disciplines.Any(e => e.DisciplineId == id);
        }
    }
}
