using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Вариант ответа на задание СДО
    /// </summary>
    public class LmsTaskAnswer
    {
        public int LmsTaskAnswerId { get; set; }

        /// <summary>
        /// Задание СДО
        /// </summary>
        [Display(Name = "Задание СДО")]
        public int LmsTaskId { get; set; }
        public LmsTask LmsTask { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        [Display(Name = "Текст ответа")]
        public string LmsTaskAnswerText { get; set; }

        /// <summary>
        /// Ответ в виде рисунка
        /// </summary>
        [Display(Name = "Ответ в виде рисунка")]
        public int? FileModelId { get; set; }
        public FileModel FileModel { get; set; }

        /// <summary>
        /// Правильный ответ
        /// </summary>
        [Display(Name = "Правильный ответ")]
        public bool IsCorrect { get; set; } = false;
    }
}
