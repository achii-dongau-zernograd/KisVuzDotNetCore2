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
        public async Task<IActionResult> Print(int id)
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

            //await GeneratePdf(entranceTestRegistrationForm);

            await _pdfDocumentGenerator.GenerateEntranceTestRegistrationForm(entranceTestRegistrationForm);

            return View(entranceTestRegistrationForm);
        }

        /// <summary>
        /// Создаёт pdf-файл
        /// </summary>
        /// <returns></returns>
        public async Task GeneratePdf(EntranceTestRegistrationForm entranceTestRegistrationForm)
        {
            #region Free Spire.PDF
            //Create a pdf document.
            PdfDocument doc = new PdfDocument();
            // Create one page
            PdfPageBase page = doc.Pages.Add();

            //Draw the text
            //page.Canvas.DrawString(@"TestString",
            //                       new PdfFont(PdfFontFamily.TimesRoman, 14f),
            //                       new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
            //                       10, 10);

            var font_TNR_9 = new System.Drawing.Font("Times New Roman", 9f);
            PdfTrueTypeFont pdfTrueTypeFont_TNR_9 = new PdfTrueTypeFont(font_TNR_9, true);
            PdfFontBase fontUtf_TNR_9 = pdfTrueTypeFont_TNR_9;

            var font_TNR_14 = new System.Drawing.Font("Times New Roman", 14f);
            PdfTrueTypeFont pdfTrueTypeFont_TNR_14 = new PdfTrueTypeFont(font_TNR_14, true);
            PdfFontBase fontUtf_TNR_14 = pdfTrueTypeFont_TNR_14;

            page.Canvas.DrawString("ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ ВЫСШЕГО ОБРАЗОВАНИЯ",
                fontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 10,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("\"ДОНСКОЙ ГОСУДАРСТВЕННЫЙ АГРАРНЫЙ УНИВЕРСИТЕТ\"",
                fontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 25,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("(ФГБОУ ВО Донской ГАУ)",
                fontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 40,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("АЗОВО-ЧЕРНОМОРСКИЙ ИНЖЕНЕРНЫЙ ИНСТИТУТ - ФИЛИАЛ ФЕДЕРАЛЬНОГО",
                fontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 55,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("ГОСУДАРСТВЕННОГО БЮДЖЕТНОГО ОБРАЗОВАТЕЛЬНОГО УЧРЕЖДЕНИЯ ВЫСШЕГО ОБРАЗОВАНИЯ",
                fontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 70,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("\"ДОНСКОЙ ГОСУДАРСТВЕННЫЙ АГРАРНЫЙ УНИВЕРСИТЕТ\" В Г. ЗЕРНОГРАДЕ",
                fontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 85,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("(Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ)",
                fontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 100,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));




            page.Canvas.DrawString("Бланк регистрации",
                fontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                30, 200,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));


            //Draw the image
            //PdfImage image = PdfImage.FromFile(@"wordart.png");
            //float width = image.Width * 0.75f;
            //float height = image.Height * 0.75f;
            //float x = (page.Canvas.ClientSize.Width - width) / 2;
            //page.Canvas.DrawImage(image, x, 150, width, height);
            
            
            //Save pdf file.
            doc.SaveToFile("EntranceTestRegistrationForm.pdf");
            doc.Close();

            #endregion
        }

        // GET: EntranceTestRegistrationForms/Create
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
