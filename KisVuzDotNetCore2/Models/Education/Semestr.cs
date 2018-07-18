using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Семестр
    /// </summary>
    public class Semestr
    {
        /// <summary>
        /// УИД семестра по учебному плану
        /// </summary>
        public int SemestrId { get; set; }

        /// <summary>
        /// Формы контроля по учебному плану
        /// </summary>
        [Display(Name = "Формы контроля")]
        public List<SemestrFormKontrolName> SemestrFormKontrolName { get; set; }

        /// <summary>
        /// Виды учебной работы по учебному плану для текущего семестра
        /// </summary>
        [Display(Name = "Виды учебной работы по учебному плану для текущего семестра")]
        public List<SemestrVidUchebRaboti> SemestrVidUchebRaboti { get; set; }

        /// <summary>
        /// Курс по учебному плану
        /// </summary>
        public int KursId { get; set; }
        [Display(Name = "Курс")]
        public Kurs Kurs { get; set; }

        /// <summary>
        /// Семестр по учебному плану
        /// </summary>
        public int SemestrNameId { get; set; }
        [Display(Name = "Семестр")]
        public SemestrName SemestrName { get; set; }
    }
}