using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Nir;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    [Authorize(Roles = "Администраторы, НИЧ")]
    public class ScienceJournalAddingClaimsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        private readonly IUserProfileRepository _userProfileRepository;

        public ScienceJournalAddingClaimsController(AppIdentityDBContext context,
            IUserProfileRepository userProfileRepository)
        {
            _context = context;
            _userProfileRepository = userProfileRepository;
        }

        // GET: ScienceJournalAddingClaims
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.ScienceJournalAddingClaims
                .Include(s => s.RowStatus)
                .Include(s => s.AppUser);
            return View(await appIdentityDBContext.ToListAsync());
        }        

        // GET: ScienceJournalAddingClaims/Create
        public IActionResult Create()
        {            
            return View();
        }

        // POST: ScienceJournalAddingClaims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScienceJournalAddingClaimId,ScienceJournalName,IsVak,IsZarubejn,ELibraryLink,CitationBasesList,RowStatusId")] ScienceJournalAddingClaim scienceJournalAddingClaim)
        {
            if (ModelState.IsValid)
            {
                if(scienceJournalAddingClaim.RowStatusId==null)
                {
                    scienceJournalAddingClaim.RowStatusId = (int?)RowStatusEnum.NotConfirmed;
                }
                scienceJournalAddingClaim.AppUserId = _userProfileRepository.GetAppUser(User.Identity.Name).Id;
                if(scienceJournalAddingClaim.AppUserId==null)
                {
                    return NotFound();
                }

                _context.Add(scienceJournalAddingClaim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(scienceJournalAddingClaim);
        }

        // GET: ScienceJournalAddingClaims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scienceJournalAddingClaim = await _context.ScienceJournalAddingClaims.SingleOrDefaultAsync(m => m.ScienceJournalAddingClaimId == id);
            if (scienceJournalAddingClaim == null)
            {
                return NotFound();
            }
            
            return View(scienceJournalAddingClaim);
        }

        // POST: ScienceJournalAddingClaims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScienceJournalAddingClaimId,ScienceJournalName,IsVak,IsZarubejn,ELibraryLink,CitationBasesList,RowStatusId")] ScienceJournalAddingClaim scienceJournalAddingClaim)
        {
            if (id != scienceJournalAddingClaim.ScienceJournalAddingClaimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (scienceJournalAddingClaim.RowStatusId == null)
                {
                    scienceJournalAddingClaim.RowStatusId = (int?)RowStatusEnum.NotConfirmed;
                }

                try
                {
                    scienceJournalAddingClaim.AppUserId = _userProfileRepository.GetAppUser(User.Identity.Name).Id;
                    _context.Update(scienceJournalAddingClaim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScienceJournalAddingClaimExists(scienceJournalAddingClaim.ScienceJournalAddingClaimId))
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
            
            return View(scienceJournalAddingClaim);
        }

        // GET: ScienceJournalAddingClaims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scienceJournalAddingClaim = await _context.ScienceJournalAddingClaims
                .Include(s => s.RowStatus)
                .SingleOrDefaultAsync(m => m.ScienceJournalAddingClaimId == id);
            if (scienceJournalAddingClaim == null)
            {
                return NotFound();
            }

            return View(scienceJournalAddingClaim);
        }

        // POST: ScienceJournalAddingClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scienceJournalAddingClaim = await _context.ScienceJournalAddingClaims.SingleOrDefaultAsync(m => m.ScienceJournalAddingClaimId == id);
            _context.ScienceJournalAddingClaims.Remove(scienceJournalAddingClaim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Подтверждение заявки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmClaim(int id)
        {
            var claim = await _context.ScienceJournalAddingClaims.FirstOrDefaultAsync(c => c.ScienceJournalAddingClaimId == id);
            if(claim!=null)
            {
                claim.RowStatusId = (int)RowStatusEnum.Confirmed;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ScienceJournalAddingClaimExists(int id)
        {
            return _context.ScienceJournalAddingClaims.Any(e => e.ScienceJournalAddingClaimId == id);
        }
    }
}
