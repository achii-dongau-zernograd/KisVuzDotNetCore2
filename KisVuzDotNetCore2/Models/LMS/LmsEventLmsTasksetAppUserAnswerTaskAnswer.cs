using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Выбор ответа пользователем из списка ответов
    /// </summary>
    public class LmsEventLmsTasksetAppUserAnswerTaskAnswer
    {
        public int LmsEventLmsTasksetAppUserAnswerTaskAnswerId { get; set; }

        /// <summary>
        /// Фиксация ответа пользователя на задание СДО
        /// </summary>
        [Display(Name = "Фиксация ответа пользователя на задание СДО")]
        public int LmsEventLmsTasksetAppUserAnswerId { get; set; }
        public LmsEventLmsTasksetAppUserAnswer LmsEventLmsTasksetAppUserAnswer { get; set; }

        /// <summary>
        /// Выбранный вариант ответа на задание СДО
        /// </summary>
        [Display(Name = "Выбранный вариант ответа на задание СДО")]
        public int LmsTaskAnswerId { get; set; }
        public LmsTaskAnswer LmsTaskAnswer { get; set; }
    }
}
