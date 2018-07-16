using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Модель "Справочник типов веб-ресурсов"
    /// </summary>
    public class LinkType
    {
        public int LinkTypeId { get; set; }

        /// <summary>
        /// Наименование типа веб-ресурсов
        /// </summary>
        [Display(Name = "Наименование типа веб-ресурсов")]
        public string LinkTypeName { get; set; }
    }
}