using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Модель структурого подразделения
    /// </summary>
    public class StructSubvision
    {
        /// <summary>
        /// УИД структурного подразделения
        /// </summary>
        public int StructSubvisionId { get; set; }

        /// <summary>
        /// Наименование структурного подразделения
        /// </summary>
        [Display(Name = "Наименование структурного подразделения")]
        public string StructSubvisionName { get; set; }

        /// <summary>
        /// ФИО руководителя структурного подразделения
        /// </summary>
        [Display(Name = "ФИО руководителя структурного подразделения")]
        public string StructSubvisionFioChief { get; set; }

        /// <summary>
        /// Должность руководителя структурного подразделения
        /// </summary>
        [Display(Name = "Должность руководителя структурного подразделения")]
        public string StructSubvisionPostChief { get; set; }

        /// <summary>
        /// Адрес местонахождения структурного подразделения
        /// </summary>
        [Display(Name = "Адрес местонахождения структурного подразделения")]
        public Address StructSubvisionAdress { get; set; }
        public int StructSubvisionAdressId { get; set; }

        /// <summary>
        /// Адрес официального сайта структурного подразделения
        /// </summary>
        [Display(Name = "Адрес официального сайта структурного подразделения")]
        public string StructSubvisionSite { get; set; }

        /// <summary>
        /// Адрес электронной почты структурного подразделения
        /// </summary>
        [Display(Name = "Адрес электронной почты структурного подразделения")]
        public Email StructSubvisionEmail { get; set; }        

        /// <summary>
        /// Положение о структурном подразделении
        /// </summary>
        [Display(Name = "Положение о структурном подразделении")]
        public FileModel StructStandingOrder { get; set; }

        /// <summary>
        /// Тип структурного подразделения
        /// </summary>
        [Display(Name = "Тип структурного подразделения")]
        public StructSubvisionType StructSubvisionType { get; set; }

        /// <summary>
        /// УИД типа структурного подразделения
        /// </summary>
        public int StructSubvisionTypeId { get; set; }
    }
}
