using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель связи М:М между таблицами Semestr и VidUchebRabotiName
    /// </summary>
    public class SemestrVidUchebRaboti
    {
        /// <summary>
        /// УИД
        /// </summary>
        public int SemestrVidUchebRabotiId { get; set; }

        /// <summary>
        /// Вид учебной работы по учебному плану
        /// </summary>
        public int VidUchebRabotiNameId { get; set; }
        [Display(Name = "Вид учебной работы")]
        public VidUchebRabotiName VidUchebRabotiName { get; set; }

        /// <summary>
        /// Семестр по учебному плану
        /// </summary>
        public int SemestrId { get; set; }
        [Display(Name = "Семестр")]
        public Semestr Semestr { get; set; }

        /// <summary>
        /// Количество часов по учебному плану
        /// </summary>
        [Display(Name = "Количество часов")]
        public int Hour { get; set; }

    }
}