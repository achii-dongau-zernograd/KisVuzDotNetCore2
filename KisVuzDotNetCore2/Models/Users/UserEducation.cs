using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Образование пользователя"
    /// </summary>
    public class UserEducation
    {
        /// <summary>
        /// УИД образования пользователя
        /// </summary>
        public int UserEducationId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public AppUser AppUser { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата выдачи")]
        public DateTime EducationDocumentDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Серия
        /// </summary>
        [Display(Name = "Серия")]
        public string EducationDocumentSeries { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        [Display(Name = "Номер")]
        public string EducationDocumentNumber { get; set; }

        /// <summary>
        /// Регистрационный номер
        /// </summary>
        [Display(Name = "Регистрационный номер")]
        public string EducationDocumentNumberReg { get; set; }


        ////////////////////////////////////////////////////
        /// <summary>
        /// УИД скан-копии документа об образовании
        /// </summary>
        [Display(Name = "Скан-копия документа об образовании")]
        public int? UserDocumentId { get; set; }
        
        /// <summary>
        /// Скан-копия документа об образовании
        /// </summary>
        [Display(Name = "Скан-копия документа об образовании")]
        public UserDocument UserDocument { get; set; }
        //////////////////////////////////////////////////////

        ////////////////////////////////////////////////////
        /// <summary>
        /// УИД типа документа об образовании
        /// </summary>
        [Display(Name = "Тип документа об образовании")]
        public int FileDataTypeId { get; set; }

        /// <summary>
        /// Тип документа об образовании
        /// </summary>
        [Display(Name = "Тип документа об образовании")]
        public FileDataType FileDataType { get; set; }
        //////////////////////////////////////////////////////


        /// <summary>
        /// Замечание
        /// </summary>
        [Display(Name = "Замечание")]
        public string Remark { get; set; }

        ////////////////////////////////////////////////////
        /// <summary>
        /// Статус записи
        /// </summary>
        [Display(Name = "Статус")]
        public int? RowStatusId { get; set; }

        /// <summary>
        /// Статус записи
        /// </summary>
        [Display(Name = "Статус")]
        public RowStatus RowStatus { get; set; }
        //////////////////////////////////////////////////////

        //////////////////////////////////////////////////////
        /// <summary>
        /// Учебное заведение
        /// </summary>
        [Display(Name = "Учебное заведение")]
        public int? EducationalInstitutionId { get; set; }

        /// <summary>
        /// Учебное заведение
        /// </summary>
        [Display(Name = "Учебное заведение")]
        public EducationalInstitution EducationalInstitution { get; set; }
        //////////////////////////////////////////////////////

        /// <summary>
        /// Присвоенная квалификация
        /// </summary>
        [Display(Name = "Квалификация")]
        public int? QualificationId { get; set; }
        public Qualification Qualification { get; set; }
    }
}
