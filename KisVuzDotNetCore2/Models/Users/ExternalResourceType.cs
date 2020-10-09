using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Справочник типов внешнего ресурса
    /// </summary>
    public class ExternalResourceType
    {
        public int ExternalResourceTypeId { get; set; }

        /// <summary>
        /// Наименование Типов внешних ресурсов
        /// </summary>
        [Display(Name ="Тип внешнего ресурса")]
        public string ExternalResourceTypeName { get; set; }
    }
}