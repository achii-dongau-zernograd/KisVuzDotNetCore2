using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Модель "Ссылка на информацию об институте на стороннем веб-сайте"
    /// </summary>
    public class InstituteLink
    {
        /// <summary>
        /// УИД ссылки
        /// </summary>
        public int InstituteLinkId { get; set; }

        /// <summary>
        /// Институт
        /// </summary>
        [Display(Name = "Институт")]
        public StructInstitute StructInstitute { get; set; }
        /// <summary>
        /// Институт
        /// </summary>
        [Display(Name = "Институт")]
        public int StructInstituteId { get; set; }

        /// <summary>
        /// Тип веб-ресурса, на который указывает ссылка
        /// </summary>
        [Display(Name = "Тип веб-ресурса, на который указывает ссылка")]
        public LinkType LinkType { get; set; }
        /// <summary>
        /// Тип веб-ресурса, на который указывает ссылка
        /// </summary>
        [Display(Name = "Тип веб-ресурса, на который указывает ссылка")]
        public int LinkTypeId { get; set; }

        /// <summary>
        /// Ссылка на информацию об институте на стороннем веб-сайте
        /// </summary>
        [DataType(DataType.Url)]
        [Display(Name = "Ссылка на информацию об институте на стороннем веб-сайте")]
        public string InstituteLinkLink { get; set; }

        /// <summary>
        /// Описание ссылки
        /// </summary>
        [Display(Name = "Описание ссылки")]
        public string InstituteLinkDescription { get; set; }
    }
}
