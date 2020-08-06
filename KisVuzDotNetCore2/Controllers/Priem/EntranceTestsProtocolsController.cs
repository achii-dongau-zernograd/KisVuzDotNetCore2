using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Priem;
using KisVuzDotNetCore2.Infrastructure;

namespace KisVuzDotNetCore2.Controllers.Priem
{
    public class EntranceTestsProtocolsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IEntranceTestsProtocolsRepository _entranceTestsProtocolsRepository;
        private readonly ISelectListRepository _selectListRepository;

        public EntranceTestsProtocolsController(AppIdentityDBContext context,
            IEntranceTestsProtocolsRepository entranceTestsProtocolsRepository,
            ISelectListRepository selectListRepository)
        {
            _context = context;
            _entranceTestsProtocolsRepository = entranceTestsProtocolsRepository;
            _selectListRepository = selectListRepository;
        }

        // GET: EntranceTestsProtocols
        public async Task<IActionResult> Index()
        {
            var query = _entranceTestsProtocolsRepository.GetEntranceTestsProtocols();
            return View(await query.ToListAsync());
        }

        // GET: EntranceTestsProtocols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entranceTestsProtocol = await _context.EntranceTestsProtocols
                .Include(e => e.Abiturient)
                .SingleOrDefaultAsync(m => m.EntranceTestsProtocolId == id);
            if (entranceTestsProtocol == null)
            {
                return NotFound();
            }

            return View(entranceTestsProtocol);
        }

        // GET: EntranceTestsProtocols/Create
        public IActionResult Create()
        {
            ViewBag.Abiturients = _selectListRepository.GetSelectListAbiturients();

            var entranceTestsProtocol = new EntranceTestsProtocol { DataVidachi = DateTime.Now };
                       
            return View(entranceTestsProtocol);
        }

        // POST: EntranceTestsProtocols/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntranceTestsProtocolId,AbiturientId,DataVidachi,FileName")] EntranceTestsProtocol entranceTestsProtocol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entranceTestsProtocol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbiturientId"] = new SelectList(_context.Abiturients, "AbiturientId", "AbiturientId", entranceTestsProtocol.AbiturientId);
            return View(entranceTestsProtocol);
        }

        // GET: EntranceTestsProtocols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entranceTestsProtocol = await _context.EntranceTestsProtocols.SingleOrDefaultAsync(m => m.EntranceTestsProtocolId == id);
            if (entranceTestsProtocol == null)
            {
                return NotFound();
            }
            ViewData["AbiturientId"] = new SelectList(_context.Abiturients, "AbiturientId", "AbiturientId", entranceTestsProtocol.AbiturientId);
            return View(entranceTestsProtocol);
        }

        // POST: EntranceTestsProtocols/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntranceTestsProtocolId,AbiturientId,DataVidachi,FileName")] EntranceTestsProtocol entranceTestsProtocol)
        {
            if (id != entranceTestsProtocol.EntranceTestsProtocolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entranceTestsProtocol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntranceTestsProtocolExists(entranceTestsProtocol.EntranceTestsProtocolId))
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
            ViewData["AbiturientId"] = new SelectList(_context.Abiturients, "AbiturientId", "AbiturientId", entranceTestsProtocol.AbiturientId);
            return View(entranceTestsProtocol);
        }

        // GET: EntranceTestsProtocols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entranceTestsProtocol = await _context.EntranceTestsProtocols
                .Include(e => e.Abiturient)
                .SingleOrDefaultAsync(m => m.EntranceTestsProtocolId == id);
            if (entranceTestsProtocol == null)
            {
                return NotFound();
            }

            return View(entranceTestsProtocol);
        }

        // POST: EntranceTestsProtocols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entranceTestsProtocol = await _context.EntranceTestsProtocols.SingleOrDefaultAsync(m => m.EntranceTestsProtocolId == id);
            _context.EntranceTestsProtocols.Remove(entranceTestsProtocol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntranceTestsProtocolExists(int id)
        {
            return _context.EntranceTestsProtocols.Any(e => e.EntranceTestsProtocolId == id);
        }




        public async Task<IActionResult> GeneratePdf(int entranceTestsProtocolId)
        {
            var entranceTestsProtocol = await _entranceTestsProtocolsRepository.GeneratePdf(entranceTestsProtocolId);

            return RedirectToAction(nameof(Index));
        }
    }
}
