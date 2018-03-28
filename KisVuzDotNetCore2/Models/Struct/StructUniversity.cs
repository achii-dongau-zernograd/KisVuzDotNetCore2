using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Модель "Университет"
    /// </summary>
    public class StructUniversity
    {
        /// <summary>
        /// УИД университета
        /// </summary>
        public int StructUniversityId { get; set; }

        /// <summary>
        /// Наименование университета
        /// </summary>
        [Display(Name = "Наименование университета")]
        public string StructUniversityName { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата создания")]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        /// <summary>
        /// Адрес
        /// </summary>
        [Display(Name = "Адрес")]
        public Address Address { get; set; } = new Address();

        public int AddressId { get; set; }
             


        /// <summary>
        /// Наличие филиалов
        /// </summary>
        [Display(Name = "Наличие филиалов")]
        public bool ExistenceOfFilials { get; set; } = false;

        /// <summary>
        /// Режим работы
        /// </summary>
        [Display(Name = "Режим работы")]
        public string WorkingRegime { get; set; }

        /// <summary>
        /// График работы
        /// </summary>
        [Display(Name = "График работы")]
        public string WorkingSchedule { get; set; }


        /// <summary>
        /// Телефоны
        /// </summary>
        [Display(Name = "Телефоны")]
        public List<Telephone> Telephones { get; set; } = new List<Telephone>();

        /// <summary>
        /// Факсы
        /// </summary>
        [Display(Name = "Факсы")]
        public List<Fax> Faxes { get; set; } = new List<Fax>();

        /// <summary>
        /// Адреса электронной почты
        /// </summary>
        [Display(Name = "Адреса электронной почты")]
        public List<Email> Emailes { get; set; } = new List<Email>();


        ////////// Навигационные свойства и поля ///////////
        /// <summary>
        /// Институты в составе университета
        /// </summary>
        public List<StructInstitute> StructInstitutes { get; set; } = new List<StructInstitute>();                
    }
}