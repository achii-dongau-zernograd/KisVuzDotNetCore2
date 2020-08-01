using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Priem;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using KisVuzDotNetCore2.Infrastructure;

namespace KisVuzDotNetCore2.Controllers.Priem
{
    public class EntranceTestRegistrationFormsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IEntranceTestRegistrationFormRepository _entranceTestRegistrationFormRepository;
        private readonly IPdfDocumentGenerator _pdfDocumentGenerator;

        public EntranceTestRegistrationFormsController(AppIdentityDBContext context,
            IEntranceTestRegistrationFormRepository entranceTestRegistrationFormRepository,
            IPdfDocumentGenerator pdfDocumentGenerator)
        {
            _context = context;
            _entranceTestRegistrationFormRepository = entranceTestRegistrationFormRepository;
            _pdfDocumentGenerator = pdfDocumentGenerator;
        }

        // GET: EntranceTestRegistrationForms
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _entranceTestRegistrationFormRepository.GetEntranceTestRegistrationForms();
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EntranceTestRegistrationForms/Details/5
        public async Task<IActionResult> Print(int id, bool needUpdatePdf)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var entranceTestRegistrationForm = await _entranceTestRegistrationFormRepository.GetEntranceTestRegistrationFormAsync(id);
            if (entranceTestRegistrationForm == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(entranceTestRegistrationForm.FileName) || needUpdatePdf)
            {
                if(!string.IsNullOrWhiteSpace(entranceTestRegistrationForm.FileName))
                {
                    await _entranceTestRegistrationFormRepository.RemovePdfFileAsync(entranceTestRegistrationForm.EntranceTestRegistrationFormId);
                }

                string createdFileName = _pdfDocumentGenerator.GenerateEntranceTestRegistrationForm(entranceTestRegistrationForm);
                await _entranceTestRegistrationFormRepository.SetPathToPdfFile(entranceTestRegistrationForm.EntranceTestRegistrationFormId, createdFileName);
            }

            return Redirect($"/{entranceTestRegistrationForm.FileName}");
        }

        
        public IActionResult Create()
        {
            ViewData["AbiturientId"] = new SelectList(_context.Abiturients, "AbiturientId", "AbiturientId");
            ViewData["LmsEventId"] = new SelectList(_context.LmsEvents, "LmsEventId", "LmsEventId");
            return View();
        }

        // POST: EntranceTestRegistrationForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntranceTestRegistrationFormId,Code,DisciplineName,LastName,FirstName,Patronymic,DocumentSeriya,DocumentNumber,Date,IsConfirmed,EntranceTestResult,AbiturientId,LmsEventId,FileName")] EntranceTestRegistrationForm entranceTestRegistrationForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entranceTestRegistrationForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbiturientId"] = new SelectList(_context.Abiturients, "AbiturientId", "AbiturientId", entranceTestRegistrationForm.AbiturientId);
            ViewData["LmsEventId"] = new SelectList(_context.LmsEvents, "LmsEventId", "LmsEventId", entranceTestRegistrationForm.LmsEventId);
            return View(entranceTestRegistrationForm);
        }

        // GET: EntranceTestRegistrationForms/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var entranceTestRegistrationForm = await _entranceTestRegistrationFormRepository.GetEntranceTestRegistrationFormAsync(id);
            if (entranceTestRegistrationForm == null)
            {
                return NotFound();
            }
            ViewData["AbiturientId"] = new SelectList(_context.Abiturients, "AbiturientId", "AbiturientId", entranceTestRegistrationForm.AbiturientId);
            ViewData["LmsEventId"] = new SelectList(_context.LmsEvents, "LmsEventId", "LmsEventId", entranceTestRegistrationForm.LmsEventId);
            return View(entranceTestRegistrationForm);
        }

        // POST: EntranceTestRegistrationForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntranceTestRegistrationFormId,Code,DisciplineName,LastName,FirstName,Patronymic,DocumentSeriya,DocumentNumber,Date,IsConfirmed,EntranceTestResult,AbiturientId,LmsEventId,FileName")] EntranceTestRegistrationForm entranceTestRegistrationForm)
        {
            if (id != entranceTestRegistrationForm.EntranceTestRegistrationFormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entranceTestRegistrationForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_entranceTestRegistrationFormRepository.IsEntranceTestRegistrationFormExists(entranceTestRegistrationForm.EntranceTestRegistrationFormId))
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
            ViewData["AbiturientId"] = new SelectList(_context.Abiturients, "AbiturientId", "AbiturientId", entranceTestRegistrationForm.AbiturientId);
            ViewData["LmsEventId"] = new SelectList(_context.LmsEvents, "LmsEventId", "LmsEventId", entranceTestRegistrationForm.LmsEventId);
            return View(entranceTestRegistrationForm);
        }

        // GET: EntranceTestRegistrationForms/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var entranceTestRegistrationForm = await _entranceTestRegistrationFormRepository.GetEntranceTestRegistrationFormAsync(id);
            if (entranceTestRegistrationForm == null)
            {
                return NotFound();
            }

            return View(entranceTestRegistrationForm);
        }

        // POST: EntranceTestRegistrationForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entranceTestRegistrationForm = await _entranceTestRegistrationFormRepository.GetEntranceTestRegistrationFormAsync(id);
            _context.EntranceTestRegistrationForms.Remove(entranceTestRegistrationForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }                
    }
}
