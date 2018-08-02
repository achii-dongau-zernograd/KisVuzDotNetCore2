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
    public class LinkTypesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public LinkTypesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: LinkTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.LinkTypes.ToListAsync());
        }

        // GET: LinkTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkType = await _context.LinkTypes
                .SingleOrDefaultAsync(m => m.LinkTypeId == id);
            if (linkType == null)
            {
                return NotFound();
            }

            return View(linkType);
        }

        // GET: LinkTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LinkTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LinkTypeId,LinkTypeName")] LinkType linkType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(linkType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(linkType);
        }

        // GET: LinkTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkType = await _context.LinkTypes.SingleOrDefaultAsync(m => m.LinkTypeId == id);
            if (linkType == null)
            {
                return NotFound();
            }
            return View(linkType);
        }

        // POST: LinkTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LinkTypeId,LinkTypeName")] LinkType linkType)
        {
            if (id != linkType.LinkTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(linkType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkTypeExists(linkType.LinkTypeId))
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
            return View(linkType);
        }

        // GET: LinkTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkType = await _context.LinkTypes
                .SingleOrDefaultAsync(m => m.LinkTypeId == id);
            if (linkType == null)
            {
                return NotFound();
            }

            return View(linkType);
        }

        // POST: LinkTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var linkType = await _context.LinkTypes.SingleOrDefaultAsync(m => m.LinkTypeId == id);
            _context.LinkTypes.Remove(linkType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinkTypeExists(int id)
        {
            return _context.LinkTypes.Any(e => e.LinkTypeId == id);
        }
    }
}
