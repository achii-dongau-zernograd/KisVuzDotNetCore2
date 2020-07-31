using KisVuzDotNetCore2.Models.Abitur;
using Microsoft.AspNetCore.Hosting;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Infrastructure
{
    /// <summary>
    /// Генератор документов pdf
    /// </summary>
    public class PdfDocumentGenerator : IPdfDocumentGenerator
    {
        private readonly IHostingEnvironment _appEnvironment;

        public PdfDocumentGenerator(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        /// <summary>
        /// Создаёт pdf-файл бланка регистрации абитуриента на вступительное испытание
        /// </summary>
        /// <param name="entranceTestRegistrationForm"></param>
        /// <returns></returns>
        public async Task GenerateEntranceTestRegistrationForm(EntranceTestRegistrationForm entranceTestRegistrationForm)
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

            var font_TNR_16 = new System.Drawing.Font("Times New Roman", 16f, FontStyle.Bold);
            PdfTrueTypeFont pdfTrueTypeFont_TNR_16 = new PdfTrueTypeFont(font_TNR_16, true);
            PdfFontBase fontUtf_TNR_16 = pdfTrueTypeFont_TNR_16;


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



            page.Canvas.DrawString("ШИФР",
                fontUtf_TNR_16,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                350, 150,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString(entranceTestRegistrationForm.Code,
                fontUtf_TNR_16,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                440, 150,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 340, 140, 470, 140);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 340, 160, 470, 160);

            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 340, 140, 340, 160);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 410, 140, 410, 160);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 470, 140, 470, 160);




            page.Canvas.DrawString("Бланк регистрации",
                fontUtf_TNR_16,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 200,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));


            //Draw the image
            //PdfImage image = PdfImage.FromFile(@"wordart.png");
            //float width = image.Width * 0.75f;
            //float height = image.Height * 0.75f;
            //float x = (page.Canvas.ClientSize.Width - width) / 2;
            //page.Canvas.DrawImage(image, x, 150, width, height);


            //Save pdf file.
            //doc.SaveToFile("EntranceTestRegistrationForm.pdf");

            string fileName = "EntranceTestRegistrationForm_" +
                            Guid.NewGuid().ToString() +
                            ".pdf";
            // путь к папке files
            string[] paths = { _appEnvironment.WebRootPath, "tmp", fileName };

            string pathToDirectory = Path.Combine(paths[0], paths[1]);
            if (!Directory.Exists(pathToDirectory))
                Directory.CreateDirectory(pathToDirectory);

            string path = Path.Combine(paths);
            doc.SaveToFile(path);

            doc.Close();

            #endregion
        }
    }
}
