using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Sveden;

namespace KisVuzDotNetCore2.Controllers
{
    public class ElectronObrazovatInformResController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public ElectronObrazovatInformResController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: ElectronObrazovatInformRes
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.ElectronObrazovatInformRes.Include(e => e.Res);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: ElectronObrazovatInformRes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronObrazovatInformRes = await _context.ElectronObrazovatInformRes
                .Include(e => e.Res)
                .SingleOrDefaultAsync(m => m.ElectronObrazovatInformResId == id);
            if (electronObrazovatInformRes == null)
            {
                return NotFound();
            }

            return View(electronObrazovatInformRes);
        }

        // GET: ElectronObrazovatInformRes/Create
        public IActionResult Create()
        {
            ViewData["ResId"] = new SelectList(_context.Files, "Id", "Id");
            return View();
        }

        // POST: ElectronObrazovatInformRes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElectronObrazovatInformResId,NameRes,LinkRes,ResId,IsSobstv,DescriptionRes")] ElectronObrazovatInformRes electronObrazovatInformRes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(electronObrazovatInformRes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResId"] = new SelectList(_context.Files, "Id", "Id", electronObrazovatInformRes.ResId);
            return View(electronObrazovatInformRes);
        }

        // GET: ElectronObrazovatInformRes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronObrazovatInformRes = await _context.ElectronObrazovatInformRes.SingleOrDefaultAsync(m => m.ElectronObrazovatInformResId == id);
            if (electronObrazovatInformRes == null)
            {
                return NotFound();
            }
            ViewData["ResId"] = new SelectList(_context.Files, "Id", "Id", electronObrazovatInformRes.ResId);
            return View(electronObrazovatInformRes);
        }

        // POST: ElectronObrazovatInformRes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElectronObrazovatInformResId,NameRes,LinkRes,ResId,IsSobstv,DescriptionRes")] ElectronObrazovatInformRes electronObrazovatInformRes)
        {
            if (id != electronObrazovatInformRes.ElectronObrazovatInformResId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electronObrazovatInformRes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectronObrazovatInformResExists(electronObrazovatInformRes.ElectronObrazovatInformResId))
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
            ViewData["ResId"] = new SelectList(_context.Files, "Id", "Id", electronObrazovatInformRes.ResId);
            return View(electronObrazovatInformRes);
        }

        // GET: ElectronObrazovatInformRes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronObrazovatInformRes = await _context.ElectronObrazovatInformRes
                .Include(e => e.Res)
                .SingleOrDefaultAsync(m => m.ElectronObrazovatInformResId == id);
            if (electronObrazovatInformRes == null)
            {
                return NotFound();
            }

            return View(electronObrazovatInformRes);
        }

        // POST: ElectronObrazovatInformRes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electronObrazovatInformRes = await _context.ElectronObrazovatInformRes.SingleOrDefaultAsync(m => m.ElectronObrazovatInformResId == id);
            _context.ElectronObrazovatInformRes.Remove(electronObrazovatInformRes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Preview(bool? IsSobstv)
        {
            var appIdentityDBContext = _context.ElectronObrazovatInformRes.Where(e=>e.IsSobstv==IsSobstv);
            return View(await appIdentityDBContext.ToListAsync());
        }

        private bool ElectronObrazovatInformResExists(int id)
        {
            return _context.ElectronObrazovatInformRes.Any(e => e.ElectronObrazovatInformResId == id);
        }
    }
}
