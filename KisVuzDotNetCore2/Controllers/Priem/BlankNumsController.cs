using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Priem;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class BlankNumsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public BlankNumsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: BlankNums
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.BlankNums.Include(b => b.EduNapravl);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: BlankNums/Create
        public IActionResult Create()
        {
            var EduNapravls = _context.EduNapravls
                .Include(n=>n.EduUgs)
                .ThenInclude(ugs=>ugs.EduLevel);
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View();
        }

        // POST: BlankNums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlankNum blankNum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blankNum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var EduNapravls = _context.EduNapravls
                .Include(n => n.EduUgs)
                .ThenInclude(ugs => ugs.EduLevel);
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View(blankNum);
        }

        // GET: BlankNums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blankNum = await _context.BlankNums.SingleOrDefaultAsync(m => m.BlankNumId == id);
            if (blankNum == null)
            {
                return NotFound();
            }
            var EduNapravls = _context.EduNapravls
                .Include(n => n.EduUgs)
                .ThenInclude(ugs => ugs.EduLevel);
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View(blankNum);
        }

        // POST: BlankNums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlankNum blankNum)
        {
            if (id != blankNum.BlankNumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blankNum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlankNumExists(blankNum.BlankNumId))
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
            var EduNapravls = _context.EduNapravls
                .Include(n => n.EduUgs)
                .ThenInclude(ugs => ugs.EduLevel);
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View();
        }

        // GET: BlankNums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blankNum = await _context.BlankNums
                .Include(b => b.EduNapravl)
                    .ThenInclude(n=>n.EduUgs)
                        .ThenInclude(u=>u.EduLevel)
                .SingleOrDefaultAsync(m => m.BlankNumId == id);
            if (blankNum == null)
            {
                return NotFound();
            }

            return View(blankNum);
        }

        // POST: BlankNums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blankNum = await _context.BlankNums.SingleOrDefaultAsync(m => m.BlankNumId == id);
            _context.BlankNums.Remove(blankNum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlankNumExists(int id)
        {
            return _context.BlankNums.Any(e => e.BlankNumId == id);
        }
    }
}