using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Common
{
    [Authorize(Roles = "Администраторы")]
    public class InstituteLinksController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public InstituteLinksController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: InstituteLinks
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.InstituteLinks.Include(i => i.LinkType).Include(i => i.StructInstitute);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: InstituteLinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteLink = await _context.InstituteLinks
                .Include(i => i.LinkType)
                .Include(i => i.StructInstitute)
                .SingleOrDefaultAsync(m => m.InstituteLinkId == id);
            if (instituteLink == null)
            {
                return NotFound();
            }

            return View(instituteLink);
        }

        // GET: InstituteLinks/Create
        public IActionResult Create()
        {
            ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "LinkTypeId", "LinkTypeName");
            ViewData["StructInstituteId"] = new SelectList(_context.StructInstitutes, "StructInstituteId", "StructInstituteName");
            return View();
        }

        // POST: InstituteLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstituteLinkId,StructInstituteId,LinkTypeId,InstituteLinkLink,InstituteLinkDescription")] InstituteLink instituteLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instituteLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "LinkTypeId", "LinkTypeName", instituteLink.LinkTypeId);
            ViewData["StructInstituteId"] = new SelectList(_context.StructInstitutes, "StructInstituteId", "StructInstituteName", instituteLink.StructInstituteId);
            return View(instituteLink);
        }

        // GET: InstituteLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteLink = await _context.InstituteLinks.SingleOrDefaultAsync(m => m.InstituteLinkId == id);
            if (instituteLink == null)
            {
                return NotFound();
            }
            ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "LinkTypeId", "LinkTypeName", instituteLink.LinkTypeId);
            ViewData["StructInstituteId"] = new SelectList(_context.StructInstitutes, "StructInstituteId", "StructInstituteName", instituteLink.StructInstituteId);
            return View(instituteLink);
        }

        // POST: InstituteLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstituteLinkId,StructInstituteId,LinkTypeId,InstituteLinkLink,InstituteLinkDescription")] InstituteLink instituteLink)
        {
            if (id != instituteLink.InstituteLinkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituteLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteLinkExists(instituteLink.InstituteLinkId))
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
            ViewData["LinkTypeId"] = new SelectList(_context.LinkTypes, "LinkTypeId", "LinkTypeName", instituteLink.LinkTypeId);
            ViewData["StructInstituteId"] = new SelectList(_context.StructInstitutes, "StructInstituteId", "StructInstituteName", instituteLink.StructInstituteId);
            return View(instituteLink);
        }

        // GET: InstituteLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteLink = await _context.InstituteLinks
                .Include(i => i.LinkType)
                .Include(i => i.StructInstitute)
                .SingleOrDefaultAsync(m => m.InstituteLinkId == id);
            if (instituteLink == null)
            {
                return NotFound();
            }

            return View(instituteLink);
        }

        // POST: InstituteLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instituteLink = await _context.InstituteLinks.SingleOrDefaultAsync(m => m.InstituteLinkId == id);
            _context.InstituteLinks.Remove(instituteLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteLinkExists(int id)
        {
            return _context.InstituteLinks.Any(e => e.InstituteLinkId == id);
        }
    }
}
