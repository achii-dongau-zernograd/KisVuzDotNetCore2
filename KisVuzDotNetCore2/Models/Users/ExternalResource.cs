using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Справочник Внешние ресурсы
    /// </summary>
    public class ExternalResource
    {        
        public int ExternalResourceId { get; set; }

        /// <summary>
        /// Наименование Внешнего ресурса
        /// </summary>
        [Display(Name = "Название внешнего ресурса")]
        public string ExternalResourceName { get; set; }

        /// <summary>
        /// Идентификатор Типа внешнего ресурса
        /// </summary>
        [Display(Name ="Идентификатор типа внешнего ресурса")]
        public int ExternalResourceTypeId { get; set; }
        /// <summary>
        /// Навигационное свойство Тип внешнего ресурса
        /// </summary>
        public ExternalResourceType ExternalResourceType { get; set; }

        public string ExternalResourceNameFull
        {
            get
            {
                return $"{ExternalResourceType?.ExternalResourceTypeName} - {ExternalResourceName}";
            } 
        }
    }
}