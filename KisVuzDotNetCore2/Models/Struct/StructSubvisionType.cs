using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Тип структурного подразделения
    /// </summary>
    public class StructSubvisionType
    {
        /// <summary>
        /// УИД типа структурного подразделения
        /// </summary>
        public int StructSubvisionTypeId { get; set; }

        /// <summary>
        /// Наименование типа структурного подразделения
        /// </summary>
        [Display(Name = "Наименование типа структурного подразделения")]
        public string StructSubvisionTypeName { get; set; }

        ///// Навигационные свойства
        
        /// <summary>
        /// Структурные подразделения
        /// </summary>
        public List<StructSubvision> StructSubvisions { get; set; }
    }
}