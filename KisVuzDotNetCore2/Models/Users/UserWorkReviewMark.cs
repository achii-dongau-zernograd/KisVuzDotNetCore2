using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Оценка работы пользователя (справочник)"
    /// </summary>
    public class UserWorkReviewMark
    {
        /// <summary>
        /// УИД оценки
        /// </summary>
        public int UserWorkReviewMarkId { get; set; }

        /// <summary>
        /// Формулировка оценки
        /// </summary>
        [Display(Name= "Формулировка оценки")]
        public string UserWorkReviewMarkName { get; set; }
    }
}