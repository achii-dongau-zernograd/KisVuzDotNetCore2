using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Тип индивидуального достижения абитуриента
    /// </summary>
    public class AbiturientIndividualAchievmentType
    {
        public int AbiturientIndividualAchievmentTypeId { get; set; }

        /// <summary>
        /// Наименование индивидуального достижения абитуриента
        /// </summary>
        [Display(Name = "Наименование индивидуального достижения абитуриента")]
        public string AbiturientIndividualAchievmentTypeName { get; set; }

        /// <summary>
        /// Балл
        /// </summary>
        [Display(Name = "Балл")]
        public int Point { get; set; }

        /// <summary>
        /// Индивидуальные достижения абитуриентов
        /// </summary>
        [Display(Name = "Индивидуальные достижения абитуриентов")]
        public List<AbiturientIndividualAchievment> AbiturientIndividualAchievments { get; set; }
    }
}
