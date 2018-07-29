using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Users
{
    // Модель "Рецензии и оценки работ пользователя"
    public class UserWorkReview
    {
        /// <summary>
        /// УИД рецензии
        /// </summary>
        public int UserWorkReviewId { get; set; }

        /// <summary>
        /// Рецензируемая работа пользователя
        /// </summary>
        [Display(Name ="Рецензируемая работа пользователя")]
        public UserWork UserWork { get; set; }
        /// <summary>
        /// Рецензируемая работа пользователя
        /// </summary>
        [Display(Name = "Рецензируемая работа пользователя")]
        public int UserWorkId { get; set; }

        /// <summary>
        /// Рецензент
        /// </summary>
        [Display(Name = "Рецензент")]
        public AppUser Reviewer { get; set; }
        /// <summary>
        /// Рецензент
        /// </summary>
        [Display(Name = "Рецензент")]
        public string ReviewerId { get; set; }

        /// <summary>
        /// Рецензия
        /// </summary>
        [Display(Name = "Рецензия")]
        public string UserWorkReviewText { get; set; }

        /// <summary>
        /// Сканированная копия рецензии или документа
        /// </summary>
        [Display(Name = "Сканированная копия рецензии или документа")]
        public FileModel FileModel { get; set; }
        /// <summary>
        /// Сканированная копия рецензии или документа
        /// </summary>
        [Display(Name = "Сканированная копия рецензии или документа")]
        public int? FileModelId { get; set; }

        // <summary>
        /// Оценка работы
        /// </summary>
        [Display(Name = "Оценка работы")]
        public UserWorkReviewMark UserWorkReviewMark { get; set; }
        // <summary>
        /// Оценка работы
        /// </summary>
        [Display(Name = "Оценка работы")]
        public int UserWorkReviewMarkId { get; set; }
    }
}