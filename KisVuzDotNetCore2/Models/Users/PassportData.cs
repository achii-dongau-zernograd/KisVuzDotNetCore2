using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Паспортные данные
    /// </summary>
    public class PassportData
    {
        public int PassportDataId { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Серия паспорта
        /// </summary>        
        [Display(Name = "Серия паспорта")]
        public string PassportSeriya { get; set; }

        /// <summary>
        /// Номер паспорта
        /// </summary>        
        [Display(Name = "Номер паспорта")]
        public string PassportNumber { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        [Required]
        [Display(Name = "Кем выдан")]
        public string KemVidan { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        [Required]
        [Display(Name = "Дата выдачи")]
        [DataType(DataType.Date)]
        public DateTime DataVidachi { get; set; }

        /// <summary>
        /// Код подразделения
        /// </summary>
        [Required]
        [Display(Name = "Код подразделения")]
        public string KodPodrazdeleniya { get; set; }

        /// <summary>
        /// Место рождения
        /// </summary>
        [Required]
        [Display(Name = "Место рождения")]
        public string MestoRojdeniya { get; set; }


        public int? RowStatusId { get; set; }
        public RowStatus RowStatus { get; set; }


        public int? UserDocumentId { get; set; }
        public UserDocument UserDocument { get; set; }

        /// <summary>
        /// Адрес по прописке
        /// </summary>
        public int AddressId { get; set; }
        public Address Address { get; set; }        
    }
}
