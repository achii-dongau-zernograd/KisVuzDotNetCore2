using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Фиксация ответа пользователя на задание СДО
    /// </summary>
    public class LmsEventLmsTasksetAppUserAnswer
    {
        public int LmsEventLmsTasksetAppUserAnswerId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Дата/время ответа
        /// </summary>
        [Display(Name = "Дата/время ответа")]
        public DateTime AnswerDateTime { get; set; }

        /// <summary>
        /// Событие - Набор заданий
        /// </summary>
        [Display(Name = "Событие - Набор заданий")]
        public int LmsEventLmsTaskSetId { get; set; }
        public LmsEventLmsTaskSet LmsEventLmsTaskSet { get; set; }

        /// <summary>
        /// Задание
        /// </summary>
        [Display(Name = "Задание")]
        public int LmsTaskId { get; set; }
        public LmsTask LmsTask { get; set; }

        /// <summary>
        /// Засчитанное количество баллов
        /// </summary>
        [Display(Name = "Засчитанное количество баллов")]
        public int? NumberOfPoints { get; set; }

        /// <summary>
        /// Ответ пользователя, введённый с клавиатуры в текстовое поле
        /// </summary>
        [Display(Name = "Ответ пользователя, введённый с клавиатуры в текстовое поле")]
        public string AnswerAsText { get; set; }

        /// <summary>
        /// Ответ пользователя, введённый как скан-копия
        /// </summary>
        [Display(Name = "Ответ пользователя, введённый как скан-копия")]
        public int? AnswerAsFileId { get; set; }
        public FileModel AnswerAsFile { get; set; }

                
        public List<LmsEventLmsTasksetAppUserAnswerTaskAnswer> LmsEventLmsTasksetAppUserAnswerTaskAnswers { get; set; }
    }
}
