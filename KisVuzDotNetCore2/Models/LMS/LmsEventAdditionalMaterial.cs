using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Дополнительный материал к мероприятию СДО
    /// </summary>
    public class LmsEventAdditionalMaterial
    {
        public int LmsEventAdditionalMaterialId { get; set; }

        /// <summary>
        /// Наименование дополнительного материала
        /// </summary>
        [Display(Name = "Наименование дополнительного материала")]
        public string LmsEventAdditionalMaterialName { get; set; }

        /// <summary>
        /// Ссылка на дополнительный материал (при наличии)
        /// </summary>
        [Display(Name = "Ссылка на дополнительный материал (при наличии)")]
        public string LmsEventAdditionalMaterialLink { get; set; }

        /// <summary>
        /// Мероприятие СДО
        /// </summary>
        [Display(Name = "Мероприятие СДО")]
        public int LmsEventId { get; set; }
        public LmsEvent LmsEvent { get; set; }
    }
}