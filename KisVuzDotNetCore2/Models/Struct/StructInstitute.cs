using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Модель "Институт"
    /// </summary>
    public class StructInstitute
    {
        /// <summary>
        /// УИД института
        /// </summary>
        public int StructInstituteId { get; set; }

        /// <summary>
        /// Наименование института
        /// </summary>
        [Display(Name = "Наименование института")]
        public string StructInstituteName { get; set; }

        /// <summary>
        /// Дата создания образовательной организации
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата создания")]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        /// <summary>
        /// Адрес
        /// </summary>
        [Display(Name = "Адрес")]
        public Address Address { get; set; }

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
        public List<Email> Emailes { get; set; } = new List<Email>();


        ////////// Навигационные свойства и поля ///////////
        public int UniversityId { get; set; }
        public StructUniversity University { get; set; }

        /// <summary>
        /// Факультеты в составе института
        /// </summary>
        public List<StructFacultet> StructFacultets { get; set; } = new List<StructFacultet>();                
    }
}