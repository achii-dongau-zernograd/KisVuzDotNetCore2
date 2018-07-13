using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    /// <summary>
    /// Учебное пособие
    /// </summary>
    public class UchPosobie
    {
        public int UchPosobieId { get; set; }

        /// <summary>
        /// Направление подготовки 
        /// </summary>
        [Display(Name = "Направление подготовки")]
        public List<UchPosobieEduNapravl> EduNapravls { get; set; }

        /// <summary>
        /// Вид учебного пособия
        /// </summary>
        [Display(Name = "Вид учебного пособия")]
        public UchPosobieVid UchPosobieVid { get; set; }

        /// <summary>
        /// Формы обучения 
        /// </summary>
        [Display(Name = "Формы обучения")]
        public List<UchPosobieEduForm> EduForms { get; set; }

        /// <summary>
        /// Авторы 
        /// </summary>
        [Display(Name = "Авторы")]
        public List<UchPosobieAuthor> UchPosobieAuthors { get; set; }

        /// <summary>
        /// Год издания
        /// </summary>
        [Display(Name = "Год издания")]
        public string GodIzdaniya { get; set; }

        /// <summary>
        /// Наименование учебного пособия
        /// </summary>
        [Display(Name = "Наименование учебного пособия")]
        public string UchPosobieName { get; set; }

        /// <summary>
        /// Библиографическое описание
        /// </summary>
        [Display(Name = "Библиографическое описание")]
        public string BiblOpisanie { get; set; }

        /// <summary>
        /// Для каких дисциплин предназначено учебное пособие
        /// </summary>
        [Display(Name = "Наименования дисциплин")]
        public List<UchPosobieDisciplineName> UchPosobieDisciplineNames { get; set; }

        /// <summary>
        /// Форма издания
        /// </summary>
        [Display(Name = "Форма издания")]
        public UchPosobieFormaIzdaniya UchPosobieFormaIzdaniya { get; set; }

        /// <summary>
        /// .pdf файл пособия
        /// </summary>
        public int FileModelId { get; set; }
        [Display(Name = ".pdf файл пособия")]
        public FileModel FileModel { get; set; }

    }
}