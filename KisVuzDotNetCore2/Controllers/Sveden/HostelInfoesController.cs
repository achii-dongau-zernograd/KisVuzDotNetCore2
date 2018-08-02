using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Sveden;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class HostelInfoesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public HostelInfoesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: HostelInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.HostelInfo.ToListAsync());
        }

        // GET: HostelInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HostelInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameIndicator,Itemprop,Value,Link")] HostelInfo hostelInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hostelInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hostelInfo);
        }

        // GET: HostelInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hostelInfo = await _context.HostelInfo.SingleOrDefaultAsync(m => m.Id == id);
            if (hostelInfo == null)
            {
                return NotFound();
            }
            return View(hostelInfo);
        }

        // POST: HostelInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameIndicator,Itemprop,Value,Link")] HostelInfo hostelInfo)
        {
            if (id != hostelInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hostelInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HostelInfoExists(hostelInfo.Id))
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
            return View(hostelInfo);
        }

        // GET: HostelInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hostelInfo = await _context.HostelInfo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hostelInfo == null)
            {
                return NotFound();
            }

            return View(hostelInfo);
        }

        // POST: HostelInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hostelInfo = await _context.HostelInfo.SingleOrDefaultAsync(m => m.Id == id);
            _context.HostelInfo.Remove(hostelInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HostelInfoExists(int id)
        {
            return _context.HostelInfo.Any(e => e.Id == id);
        }
    }
}
