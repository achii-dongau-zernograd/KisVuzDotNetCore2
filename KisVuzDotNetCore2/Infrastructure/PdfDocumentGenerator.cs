using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.LMS;
using KisVuzDotNetCore2.Models.Priem;
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
        private readonly AppIdentityDBContext _context;

        public PdfDocumentGenerator(IHostingEnvironment appEnvironment,
            AppIdentityDBContext context)
        {
            _appEnvironment = appEnvironment;
            _context = context;
        }


        PdfFontBase FontUtf_TNR_9 
        {
            get {
                var font_TNR_9 = new System.Drawing.Font("Times New Roman", 9f);
                PdfTrueTypeFont pdfTrueTypeFont_TNR_9 = new PdfTrueTypeFont(font_TNR_9, true);
                return pdfTrueTypeFont_TNR_9;
            }
        }

        
        PdfFontBase FontUtf_TNR_14
        {
            get
            {
                var font_TNR_14 = new System.Drawing.Font("Times New Roman", 14f);
                PdfTrueTypeFont pdfTrueTypeFont_TNR_14 = new PdfTrueTypeFont(font_TNR_14, true);
                return pdfTrueTypeFont_TNR_14;
            }
        }
                
        /// <summary>
        /// Times New Roman 16
        /// </summary>
        PdfFontBase FontUtf_TNR_16_b
        {
            get
            {
                var font_TNR_16 = new System.Drawing.Font("Times New Roman", 16f, FontStyle.Bold);
                PdfTrueTypeFont pdfTrueTypeFont_TNR_16 = new PdfTrueTypeFont(font_TNR_16, true);
                return pdfTrueTypeFont_TNR_16;
            }
        }


        

        

        /// <summary>
        /// Создаёт pdf-файл бланка регистрации абитуриента на вступительное испытание
        /// и возвращает путь к созданному файлу
        /// </summary>
        /// <param name="entranceTestRegistrationForm"></param>
        /// <returns></returns>
        public string GenerateEntranceTestRegistrationForm(EntranceTestRegistrationForm entranceTestRegistrationForm)
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
                280, 200,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            int y = 250;
            int dy = 30;
            page.Canvas.DrawString("Вступительное испытание по: " + entranceTestRegistrationForm.DisciplineName,
                fontUtf_TNR_16,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));


            y += dy;
            page.Canvas.DrawString("Фамилия: " + entranceTestRegistrationForm.LastName,
                fontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            y += dy;
            page.Canvas.DrawString("Имя: " + entranceTestRegistrationForm.FirstName,
                fontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            y += dy;
            page.Canvas.DrawString("Отчество (при наличии): " + entranceTestRegistrationForm.Patronymic,
                fontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            y += dy;
            page.Canvas.DrawString("Документ: " + entranceTestRegistrationForm.DocumentSeriya + "   номер " + entranceTestRegistrationForm.DocumentNumber,
                fontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            y += dy;
            page.Canvas.DrawString("Дата проведения: " + entranceTestRegistrationForm.Date.ToString("dd.MM.yyyy"),
                fontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            y += dy;
            page.Canvas.DrawString("Подпись: V",
                fontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));








            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 30, 540, 250, 540);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 30, 590, 250, 590);

            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 70, 610, 250, 610);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 30, 630, 250, 630);

            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 30,  540, 30,  630);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 70,  590, 70,  630);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 250, 540, 250, 630);




            page.Canvas.DrawString("Результат вступительного",
                fontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                150, 550,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("испытания",
                fontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                150, 565,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("количество баллов (оценка)",
                fontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                150, 580,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString(entranceTestRegistrationForm.EntranceTestResult ?? "",
                fontUtf_TNR_16,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                50, 610,
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
            return Path.Combine(paths[1], paths[2]);
            #endregion
        }

        /// <summary>
        /// Создаёт pdf-файл бланка ответов на экзаменационное задание
        /// и возвращает путь к созданному файлу
        /// </summary>
        /// <param name="entranceTestRegistrationForm"></param>
        /// <returns></returns>
        public string GenerateEntranceTestBlankOtvetov(EntranceTestRegistrationForm entranceTestRegistrationForm,
            List<LmsTask> lmsEventTasks)
        {
            #region Free Spire.PDF
            //Create a pdf document.
            PdfDocument doc = new PdfDocument();
            // Create one page
            PdfPageBase page = doc.Pages.Add();
                        

            PrintShapka(page);


            page.Canvas.DrawString("Дата проведения: " + entranceTestRegistrationForm.Date.ToString("dd.MM.yyyy"),
                FontUtf_TNR_16_b,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, 150,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));


            page.Canvas.DrawString("ШИФР",
                FontUtf_TNR_16_b,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                350, 150,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString(entranceTestRegistrationForm.Code,
                FontUtf_TNR_16_b,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                440, 150,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 340, 140, 470, 140);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 340, 160, 470, 160);

            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 340, 140, 340, 160);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 410, 140, 410, 160);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 470, 140, 470, 160);




            page.Canvas.DrawString("БЛАНК ОТВЕТОВ НА ЭКЗАМЕНАЦИОННОЕ ЗАДАНИЕ",
                FontUtf_TNR_16_b,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                280, 200,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            int y = 250;
            int dy = 25;
            page.Canvas.DrawString("Предмет: " + entranceTestRegistrationForm.DisciplineName,
                FontUtf_TNR_16_b,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));


            y += dy;
            page.Canvas.DrawString("Записать номер правильного ответа или правильный ответ",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                280, y,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));





            for (int i = 0; i < lmsEventTasks.Count; i+=2)
            {
                string appUserAnswerString = "";

                if(lmsEventTasks[i].LmsEventLmsTaskSetAppUserAnswers != null)
                {
                    foreach (var lmsEventLmsTaskSetAppUserAnswer in lmsEventTasks[i].LmsEventLmsTaskSetAppUserAnswers)
                    {
                        foreach (var lmsEventLmsTasksetAppUserAnswerTaskAnswer in lmsEventLmsTaskSetAppUserAnswer.LmsEventLmsTasksetAppUserAnswerTaskAnswers)
                        {
                            // Определяем номер выбранного ответа 
                            int selectedNumber = 0;

                            for (int lmsTaskAnswerIndex = 0; lmsTaskAnswerIndex < lmsEventLmsTasksetAppUserAnswerTaskAnswer.LmsTaskAnswer.LmsTask.LmsTaskAnswers.Count; lmsTaskAnswerIndex++)
                            {
                                if (lmsEventLmsTasksetAppUserAnswerTaskAnswer.LmsTaskAnswer.LmsTask.LmsTaskAnswers[lmsTaskAnswerIndex].LmsTaskAnswerId == lmsEventLmsTasksetAppUserAnswerTaskAnswer.LmsTaskAnswer.LmsTaskAnswerId)
                                {
                                    selectedNumber = lmsTaskAnswerIndex + 1;
                                }
                            }

                            appUserAnswerString += selectedNumber + " ";
                        }

                        // Ответы пользователя в виде текста
                        if (!string.IsNullOrWhiteSpace(lmsEventLmsTaskSetAppUserAnswer.AnswerAsText))
                        {
                            appUserAnswerString += lmsEventLmsTaskSetAppUserAnswer.AnswerAsText;
                        }
                    }
                }

                


                y += dy;
                page.Canvas.DrawString($"Задание {i+1}: " + appUserAnswerString,
                    FontUtf_TNR_14,
                    new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                    20, y,
                    new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

                /////////// 2 столбец
                if(i + 1 < lmsEventTasks.Count)
                {
                    appUserAnswerString = "";
                    if (lmsEventTasks[i + 1].LmsEventLmsTaskSetAppUserAnswers != null)
                    {
                        foreach (var lmsEventLmsTaskSetAppUserAnswer in lmsEventTasks[i + 1].LmsEventLmsTaskSetAppUserAnswers)
                        {
                            foreach (var lmsEventLmsTasksetAppUserAnswerTaskAnswer in lmsEventLmsTaskSetAppUserAnswer.LmsEventLmsTasksetAppUserAnswerTaskAnswers)
                            {
                                // Определяем номер выбранного ответа 
                                int selectedNumber = 0;

                                for (int lmsTaskAnswerIndex = 0; lmsTaskAnswerIndex < lmsEventLmsTasksetAppUserAnswerTaskAnswer.LmsTaskAnswer.LmsTask.LmsTaskAnswers.Count; lmsTaskAnswerIndex++)
                                {
                                    if (lmsEventLmsTasksetAppUserAnswerTaskAnswer.LmsTaskAnswer.LmsTask.LmsTaskAnswers[lmsTaskAnswerIndex].LmsTaskAnswerId == lmsEventLmsTasksetAppUserAnswerTaskAnswer.LmsTaskAnswer.LmsTaskAnswerId)
                                    {
                                        selectedNumber = lmsTaskAnswerIndex + 1;
                                    }
                                }

                                appUserAnswerString += selectedNumber + " ";
                            }

                            // Ответы пользователя в виде текста
                            if (!string.IsNullOrWhiteSpace(lmsEventLmsTaskSetAppUserAnswer.AnswerAsText))
                            {
                                appUserAnswerString += lmsEventLmsTaskSetAppUserAnswer.AnswerAsText;
                            }
                        }
                    }
                                        
                    

                    page.Canvas.DrawString($"Задание {i + 2}: " + appUserAnswerString,
                        FontUtf_TNR_14,
                        new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                        300, y,
                        new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));
                }
                
            }





            //////////////////////////////////////////////////////////////////////////////
            y += dy * 2;
            page.Canvas.DrawString("Подсчет баллов:",
                FontUtf_TNR_16_b,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                280, y,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));




            y += dy;
            page.Canvas.DrawString("Оценка",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                40, y,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 20, y + 10, 60, y + 10);

            page.Canvas.DrawString("№ задания",
                FontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                40, y + 15,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));


            int x_Start = 100;
            int y_Start = y;

            int sum_points = 0;

            for (int i = 0; i < lmsEventTasks.Count; i ++)
            {
                if (lmsEventTasks[i].LmsEventLmsTaskSetAppUserAnswers != null)
                {
                    foreach (var lmsEventLmsTaskSetAppUserAnswer in lmsEventTasks[i].LmsEventLmsTaskSetAppUserAnswers)
                    {
                        int row_indx = i/9 + 1;
                        int col_indx = i - (row_indx - 1) * 9;

                        int x_cur = x_Start + col_indx * 50;
                        int y_cur = y_Start + (row_indx - 1) * 30;
                        y = y_cur;

                        sum_points += lmsEventLmsTaskSetAppUserAnswer.NumberOfPoints ?? 0;
                        //page.Canvas.DrawString( lmsEventLmsTaskSetAppUserAnswer.NumberOfPoints == null ? "" : lmsEventLmsTaskSetAppUserAnswer.NumberOfPoints.ToString(),
                        //FontUtf_TNR_14,
                        //new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                        //x_cur, y_cur,
                        //new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

                        page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), x_cur - 15, y_cur + 10, x_cur + 15, y_cur + 10);

                        page.Canvas.DrawString((i+1).ToString(),
                            FontUtf_TNR_9,
                            new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                            x_cur, y_cur + 15,
                            new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));


                        if(i < lmsEventTasks.Count - 1)
                        {
                            page.Canvas.DrawString("+",
                            FontUtf_TNR_14,
                            new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                            x_cur + 25, y_cur,
                            new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));
                        }
                        else
                        {
                            page.Canvas.DrawString("=  " /*+ sum_points*/,
                            FontUtf_TNR_14,
                            new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                            x_cur + 25, y_cur,
                            new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));
                        }
                    }
                }
            }





            /////////////////////////////////////////////////////////////////////////////////////

            y += 40;
            page.Canvas.DrawString("Члены экзаменационной комиссии",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 250, y + 10, 320, y + 10);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 350, y + 10, 470, y + 10);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 250, y + 45, 320, y + 45);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 350, y + 45, 470, y + 45);

            page.Canvas.DrawString("Подпись",
                FontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, y + 15,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("Подпись",
                FontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, y + 50,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));


            page.Canvas.DrawString("ФИО",
                FontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                410, y + 15,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("ФИО",
                FontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                410, y + 50,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));





            string fileName = "BlankOtvetov_" +
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
            return Path.Combine(paths[1], paths[2]);
            #endregion
        }

        /// <summary>
        /// Печатает шапку документа
        /// </summary>
        /// <param name="page"></param>
        private void PrintShapka(PdfPageBase page)
        {
            page.Canvas.DrawString("ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ ВЫСШЕГО ОБРАЗОВАНИЯ",
                            FontUtf_TNR_9,
                            new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                            270, 10,
                            new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("\"ДОНСКОЙ ГОСУДАРСТВЕННЫЙ АГРАРНЫЙ УНИВЕРСИТЕТ\"",
                FontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 25,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("(ФГБОУ ВО Донской ГАУ)",
                FontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 40,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("АЗОВО-ЧЕРНОМОРСКИЙ ИНЖЕНЕРНЫЙ ИНСТИТУТ - ФИЛИАЛ ФЕДЕРАЛЬНОГО",
                FontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 55,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("ГОСУДАРСТВЕННОГО БЮДЖЕТНОГО ОБРАЗОВАТЕЛЬНОГО УЧРЕЖДЕНИЯ ВЫСШЕГО ОБРАЗОВАНИЯ",
                FontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 70,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("\"ДОНСКОЙ ГОСУДАРСТВЕННЫЙ АГРАРНЫЙ УНИВЕРСИТЕТ\" В Г. ЗЕРНОГРАДЕ",
                FontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 85,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("(Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ)",
                FontUtf_TNR_9,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                270, 100,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));
        }


        /// <summary>
        /// Создаёт pdf-файл протокола вступительных испытаний
        /// и возвращает путь к созданному файлу
        /// </summary>
        /// <param name="entranceTestsProtocol"></param>
        /// <returns></returns>
        public string GenerateEntranceTestsProtocol(EntranceTestsProtocol entranceTestsProtocol,
            string userPhotoPath)
        {            
            #region Free Spire.PDF
            //Create a pdf document.
            PdfDocument doc = new PdfDocument();
            // Create one page
            PdfPageBase page = doc.Pages.Add();


            PrintShapka(page);

                      




            page.Canvas.DrawString("ПРОТОКОЛ ВСТУПИТЕЛЬНЫХ ИСПЫТАНИЙ № ____",
                FontUtf_TNR_16_b,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                280, 150,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            int y = 190;
            int dy = 25;
            page.Canvas.DrawString("Фамилия: " + entranceTestsProtocol.Abiturient.AppUser.LastName,
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));


            y += dy;
            page.Canvas.DrawString("Имя: " + entranceTestsProtocol.Abiturient.AppUser.FirstName,
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            y += dy;
            page.Canvas.DrawString("Отчество: " + entranceTestsProtocol.Abiturient.AppUser.Patronymic,
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            y += dy;
            page.Canvas.DrawString("Дата выдачи: " + entranceTestsProtocol.DataVidachi.ToString("dd.MM.yyyy"),
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));



            //Draw the image
            if (!string.IsNullOrWhiteSpace(userPhotoPath))
            {
                PdfImage image = PdfImage.FromFile(Path.Combine(_appEnvironment.WebRootPath, userPhotoPath));
                float y_to_x_coeff = (float)image.Height / image.Width;                                
                page.Canvas.DrawImage(image, 40, 290, 100, 100 * y_to_x_coeff);                
            }


            y += 50;
            page.Canvas.DrawString("и.о. Ответственный секретарь приёмной комиссии",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                160, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            y += 50;
            page.Canvas.DrawString("______________________________ В.П. Скворцов",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                160, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));




            y += 110;
            page.Canvas.DrawString("РЕЗУЛЬТАТЫ ВСТУПИТЕЛЬНЫХ ИСПЫТАНИЙ",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                280, y,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));


            y += 40;
            page.Canvas.DrawString("Предмет",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                60, y,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            


            page.Canvas.DrawString("Результаты вступительных",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                230, y-7,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("испытаний, баллов",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                230, y+7,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));




            page.Canvas.DrawString("Дата проведения",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                420, y-7,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

            page.Canvas.DrawString("вступительных испытаний",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                420, y+7,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));


            int y_top_line = y - 15;
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 20, y_top_line, 510, y_top_line);
            page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 20, y + 15, 510, y + 15);


            
            for (int i = 0; i < (entranceTestsProtocol.Abiturient.EntranceTestRegistrationForms?.Count ?? 0); i++)
            {
                y += dy;

                page.Canvas.DrawString(entranceTestsProtocol.Abiturient.EntranceTestRegistrationForms[i].DisciplineName,
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                25, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

                page.Canvas.DrawString(entranceTestsProtocol.Abiturient.EntranceTestRegistrationForms[i].EntranceTestResult ?? "",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                230, y,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

                page.Canvas.DrawString(entranceTestsProtocol.Abiturient.EntranceTestRegistrationForms[i].Date.ToString("dd.MM.yyyy"),
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                420, y,
                new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle));

                page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 20, y + 15, 510, y + 15);

                // Формируем вертикальные линии
                if(i == entranceTestsProtocol.Abiturient.EntranceTestRegistrationForms.Count - 1)
                {
                    page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 20, y_top_line, 20, y + 15);
                    page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 140, y_top_line, 140, y + 15);
                    page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 330, y_top_line, 330, y + 15);
                    page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 510, y_top_line, 510, y + 15);
                }
            }




            /////////////////////////////////////////////////////////////////////////////////////

            y += dy * 2;
            page.Canvas.DrawString("Члены комиссии",
                FontUtf_TNR_14,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                20, y,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));

            y += 10;
            for (int i = 0; i < 7; i++)
            {                
                page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 220, y, 320, y);
                page.Canvas.DrawLine(new PdfPen(new PdfRGBColor(0, 0, 0)), 350, y, 510, y);
                y += 15;
            }
            
            ///////////////////////////////////////////////////////////////////////////////////////           


            string fileName = "EntranceTestsProtocol_" +
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


            // Если файл существует, удаляем его
            if (!string.IsNullOrWhiteSpace(entranceTestsProtocol.FileName))
            {
                string oldFilePath = Path.Combine(paths[0], entranceTestsProtocol.FileName);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            string newFileName = Path.Combine(paths[1], paths[2]);
            entranceTestsProtocol.FileName = newFileName;
            _context.SaveChanges();

            return newFileName;
            #endregion
        }
    }
}
