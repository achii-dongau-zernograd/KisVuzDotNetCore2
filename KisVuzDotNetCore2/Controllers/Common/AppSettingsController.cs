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
    public class AppSettingsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public AppSettingsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: AppSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppSettings.ToListAsync());
        }                

        // GET: AppSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appSetting = await _context.AppSettings.SingleOrDefaultAsync(m => m.AppSettingId == id);
            if (appSetting == null)
            {
                return NotFound();
            }
            return View(appSetting);
        }

        // POST: AppSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppSettingId,AppSettingName,AppSettingValue")] AppSetting appSetting)
        {
            if (id != appSetting.AppSettingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppSettingExists(appSetting.AppSettingId))
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
            return View(appSetting);
        }

        private bool AppSettingExists(int id)
        {
            return _context.AppSettings.Any(e => e.AppSettingId == id);
        }
    }
}
