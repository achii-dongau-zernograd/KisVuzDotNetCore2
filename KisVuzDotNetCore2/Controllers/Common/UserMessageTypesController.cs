using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Common;

namespace KisVuzDotNetCore2.Controllers.Common
{
    public class UserMessageTypesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UserMessageTypesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UserMessageTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserMessageTypes.ToListAsync());
        }

        // GET: UserMessageTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessageType = await _context.UserMessageTypes
                .SingleOrDefaultAsync(m => m.UserMessageTypeId == id);
            if (userMessageType == null)
            {
                return NotFound();
            }

            return View(userMessageType);
        }

        // GET: UserMessageTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserMessageTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserMessageTypeId,UserMessageTypeName")] UserMessageType userMessageType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userMessageType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userMessageType);
        }

        // GET: UserMessageTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessageType = await _context.UserMessageTypes.SingleOrDefaultAsync(m => m.UserMessageTypeId == id);
            if (userMessageType == null)
            {
                return NotFound();
            }
            return View(userMessageType);
        }

        // POST: UserMessageTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserMessageTypeId,UserMessageTypeName")] UserMessageType userMessageType)
        {
            if (id != userMessageType.UserMessageTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userMessageType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMessageTypeExists(userMessageType.UserMessageTypeId))
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
            return View(userMessageType);
        }

        // GET: UserMessageTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessageType = await _context.UserMessageTypes
                .SingleOrDefaultAsync(m => m.UserMessageTypeId == id);
            if (userMessageType == null)
            {
                return NotFound();
            }

            return View(userMessageType);
        }

        // POST: UserMessageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userMessageType = await _context.UserMessageTypes.SingleOrDefaultAsync(m => m.UserMessageTypeId == id);
            _context.UserMessageTypes.Remove(userMessageType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserMessageTypeExists(int id)
        {
            return _context.UserMessageTypes.Any(e => e.UserMessageTypeId == id);
        }
    }
}
