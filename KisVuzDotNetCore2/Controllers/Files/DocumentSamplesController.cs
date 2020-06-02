using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Authorization;
using KisVuzDotNetCore2.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace KisVuzDotNetCore2.Controllers.Files
{
    [Authorize(Roles ="Администраторы, Приёмная комиссия")]
    public class DocumentSamplesController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IDocumentSamplesRepository _documentSamplesRepository;
        private readonly ISelectListRepository _selectListRepository;

        public DocumentSamplesController(AppIdentityDBContext context,
            IDocumentSamplesRepository documentSamplesRepository,
            ISelectListRepository selectListRepository)
        {
            _context = context;
            _documentSamplesRepository = documentSamplesRepository;
            _selectListRepository = selectListRepository;
        }

        // GET: DocumentSamples
        public async Task<IActionResult> Index()
        {
            IQueryable<DocumentSample> documentSamples = _documentSamplesRepository.GetDocumentSamples(FileDataTypeEnum.ApplicationForAdmission,
                FileDataTypeEnum.SoglasieNaObrabotkuPersonalnihDannih,
                FileDataTypeEnum.ConsentToEnrollment,
                FileDataTypeEnum.AbiturientCard,
                FileDataTypeEnum.DogovorOCelevomObuchenii,
                FileDataTypeEnum.DogovorObOkazaniiPlatnihObrazovatelnihUslug);
            
            return View(await documentSamples.ToListAsync());
        }

        // GET: DocumentSamples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentSample = await _context.DocumentSamples
                .Include(d => d.EduProfile)
                .Include(d => d.FileDataType)
                .Include(d => d.FileModel)
                .SingleOrDefaultAsync(m => m.DocumentSampleId == id);
            if (documentSample == null)
            {
                return NotFound();
            }

            return View(documentSample);
        }

        // GET: DocumentSamples/Create
        public IActionResult Create()
        {
            ViewData["EduProfiles"] = _selectListRepository.GetSelectListEduProfileFullNames();
            ViewData["FileDataTypes"] = _selectListRepository.GetSelectListFileDataTypes();            
            return View();
        }

        // POST: DocumentSamples/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentSample documentSample, IFormFile uploadedFile)
        {
            await _documentSamplesRepository.AddDocumentSampleAsync(documentSample, uploadedFile);
            return RedirectToAction(nameof(Index));
        }

        // GET: DocumentSamples/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var documentSample = await _documentSamplesRepository.GetDocumentSampleAsync(id);
            if (documentSample == null)
            {
                return NotFound();
            }
            ViewData["EduProfiles"] = _selectListRepository.GetSelectListEduProfileFullNames(documentSample.EduProfileId ?? 0);
            ViewData["FileDataTypes"] = _selectListRepository.GetSelectListFileDataTypes(documentSample.FileDataTypeId);
            return View(documentSample);
        }

        // POST: DocumentSamples/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DocumentSample documentSample, IFormFile uploadedFile)
        {
            await _documentSamplesRepository.UpdateDocumentSampleAsync(documentSample, uploadedFile);
            return RedirectToAction(nameof(Index));
        }

        // GET: DocumentSamples/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var documentSample = await _documentSamplesRepository.GetDocumentSampleAsync(id);
            if (documentSample == null)
            {
                return NotFound();
            }

            return View(documentSample);
        }

        // POST: DocumentSamples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _documentSamplesRepository.RemoveDocumentSampleAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentSampleExists(int id)
        {
            return _context.DocumentSamples.Any(e => e.DocumentSampleId == id);
        }
    }
}
