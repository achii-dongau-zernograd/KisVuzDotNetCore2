using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Priem
{
    public class BlankNum
    {
        /// <summary>
        /// Модель шаблона представления информации о количестве поданных заявлений о приеме
        /// </summary>
        public int BlankNumId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Направление подготовки(специальности)")]
        public EduNapravl EduNapravl { get; set; }

        public int EduNapravlId { get; set; }

        /// <summary>
        /// Всего
        /// </summary>
        [Display(Name = "Всего")]
        public int Sum { get { return Och + Zaoch + OchZaoch; } }

        /// <summary>
        /// Очное
        /// </summary>
        [Display(Name = "Очное")]
        public int Och { get; set; }

        /// <summary>
        /// Заочное
        /// </summary>
        [Display(Name = "Заочное")]
        public int Zaoch { get; set; }

        /// <summary>
        /// Очно-заочное
        /// </summary>
        [Display(Name = "Очно-заочное")]
        public int OchZaoch { get; set; }

    }
}
