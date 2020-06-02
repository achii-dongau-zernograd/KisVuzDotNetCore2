using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Тип договора
    /// </summary>
    public class ContractType
    {
        public int ContractTypeId { get; set; }

        /// <summary>
        /// Наименование типа договора
        /// </summary>
        [Display(Name = "Наименование типа договора")]
        public string ContractTypeName { get; set; }
    }
}