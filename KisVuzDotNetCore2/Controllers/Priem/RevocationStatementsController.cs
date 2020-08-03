using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Abitur;
using Microsoft.AspNetCore.Authorization;
using KisVuzDotNetCore2.Infrastructure;

namespace KisVuzDotNetCore2.Controllers.Priem
{
    [Authorize(Roles = "Администраторы, Приёмная комиссия")]
    public class RevocationStatementsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IRevocationStatementRepository _revocationStatementRepository;
        private readonly ISelectListRepository _selectListRepository;

        public RevocationStatementsController(AppIdentityDBContext context,
            IRevocationStatementRepository revocationStatementRepository,
            ISelectListRepository selectListRepository)
        {
            _context = context;
            _revocationStatementRepository = revocationStatementRepository;
            _selectListRepository = selectListRepository;
        }

        // GET: RevocationStatements
        public async Task<IActionResult> Index()
        {
            var data = _revocationStatementRepository.GetRevocationStatements();
            
            return View(await data.ToListAsync());
        }

        // GET: RevocationStatements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revocationStatement = await _context.RevocationStatements
                .Include(r => r.ApplicationForAdmission)
                .Include(r => r.FileModel)
                .Include(r => r.RowStatus)
                .SingleOrDefaultAsync(m => m.RevocationStatementId == id);
            if (revocationStatement == null)
            {
                return NotFound();
            }

            return View(revocationStatement);
        }

        // GET: RevocationStatements/Create
        public IActionResult Create()
        {
            ViewData["ApplicationForAdmissionId"] = new SelectList(_context.ApplicationForAdmissions, "ApplicationForAdmissionId", "ApplicationForAdmissionId");
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id");
            ViewData["RowStatusId"] = new SelectList(_context.RowStatuses, "RowStatusId", "RowStatusId");
            return View();
        }

        // POST: RevocationStatements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RevocationStatementId,ApplicationForAdmissionId,RejectionReason,Date,FileModelId,GeneratedPdfDocumentPath,Remark,RowStatusId")] RevocationStatement revocationStatement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revocationStatement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationForAdmissionId"] = new SelectList(_context.ApplicationForAdmissions, "ApplicationForAdmissionId", "ApplicationForAdmissionId", revocationStatement.ApplicationForAdmissionId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", revocationStatement.FileModelId);
            ViewData["RowStatusId"] = new SelectList(_context.RowStatuses, "RowStatusId", "RowStatusId", revocationStatement.RowStatusId);
            return View(revocationStatement);
        }

        // GET: RevocationStatements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revocationStatement = await _context.RevocationStatements.SingleOrDefaultAsync(m => m.RevocationStatementId == id);
            if (revocationStatement == null)
            {
                return NotFound();
            }
            ViewData["ApplicationForAdmissionId"] = new SelectList(_context.ApplicationForAdmissions, "ApplicationForAdmissionId", "ApplicationForAdmissionId", revocationStatement.ApplicationForAdmissionId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", revocationStatement.FileModelId);
            ViewData["RowStatusId"] = new SelectList(_context.RowStatuses, "RowStatusId", "RowStatusId", revocationStatement.RowStatusId);
            return View(revocationStatement);
        }

        // POST: RevocationStatements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RevocationStatementId,ApplicationForAdmissionId,RejectionReason,Date,FileModelId,GeneratedPdfDocumentPath,Remark,RowStatusId")] RevocationStatement revocationStatement)
        {
            if (id != revocationStatement.RevocationStatementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revocationStatement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevocationStatementExists(revocationStatement.RevocationStatementId))
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
            ViewData["ApplicationForAdmissionId"] = new SelectList(_context.ApplicationForAdmissions, "ApplicationForAdmissionId", "ApplicationForAdmissionId", revocationStatement.ApplicationForAdmissionId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", revocationStatement.FileModelId);
            ViewData["RowStatusId"] = new SelectList(_context.RowStatuses, "RowStatusId", "RowStatusId", revocationStatement.RowStatusId);
            return View(revocationStatement);
        }

        // GET: RevocationStatements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revocationStatement = await _context.RevocationStatements
                .Include(r => r.ApplicationForAdmission)
                .Include(r => r.FileModel)
                .Include(r => r.RowStatus)
                .SingleOrDefaultAsync(m => m.RevocationStatementId == id);
            if (revocationStatement == null)
            {
                return NotFound();
            }

            return View(revocationStatement);
        }

        // POST: RevocationStatements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var revocationStatement = await _context.RevocationStatements.SingleOrDefaultAsync(m => m.RevocationStatementId == id);
            _context.RevocationStatements.Remove(revocationStatement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevocationStatementExists(int id)
        {
            return _context.RevocationStatements.Any(e => e.RevocationStatementId == id);
        }
    }
}
