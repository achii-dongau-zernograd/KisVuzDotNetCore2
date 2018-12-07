using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Модель "Патенты и свидетельства"
    /// </summary>
    public class Patent
    {
        public int PatentId { get; set; }

        /// <summary>
        /// Наименование патента (свидетельства)
        /// </summary>
        [Display(Name = "Наименование патента (свидетельства)")]
        public string PatentName { get; set; }

        /// <summary>
        /// Номер патента (свидетельства)
        /// </summary>
        [Display(Name = "Номер патента (свидетельства)")]
        public string PatentNumber { get; set; }

        /// <summary>
        /// Год публикации патента (свидетельства)
        /// </summary>
        [Display(Name = "Год публикации патента (свидетельства)")]
        public Year Year { get; set; }
        /// <summary>
        /// Год публикации патента (свидетельства)
        /// </summary>
        [Display(Name = "Год публикации патента (свидетельства)")]
        public int? YearId { get; set; }

        /// <summary>
        /// Признак зарубежного патента (свидетельства)
        /// </summary>
        [Display(Name = "Зарубежный патент (свидетельство)")]
        public bool IsZarubejn { get; set; }

        /// <summary>
        /// Патентообладатель
        /// </summary>
        [Display(Name = "Патентообладатель")]
        public string PatentOwner { get; set; }

        /// <summary>
        /// Признак принадлежности ВУЗу
        /// </summary>
        [Display(Name = "Принадлежит АЧИИ")]
        public bool IsACHII { get; set; }

        /// <summary>
        /// Авторы - патенты - авторские доли
        /// </summary>
        [Display(Name = "Авторы")]
        public List<PatentAuthor> PatentAuthors { get; set; }

        /// <summary>
        /// Вид патента
        /// </summary>
        [Display(Name = "Вид патента")]
        public PatentVid PatentVid { get; set; }
        /// <summary>
        /// Вид патента
        /// </summary>
        [Display(Name = "Вид патента")]
        public int? PatentVidId { get; set; }

        /// <summary>
        /// Список тем НИР, которым соответствует патент (свидетельство)
        /// </summary>
        [Display(Name = "Темы НИР")]
        public List<PatentNirTema> PatentNirTemas { get; set; }

        /// <summary>
        /// Список специальностей научных работников, которым соответствует патент (свидетельство)
        /// </summary>
        [Display(Name = "Научные специальности")]
        public List<PatentNirSpecial> PatentNirSpecials { get; set; } = new List<PatentNirSpecial>();

        /// <summary>
        /// .pdf файл патента
        /// </summary>
        [Display(Name = ".pdf файл патента")]
        public FileModel FileModel { get; set; }
        [Display(Name = ".pdf файл патента")]
        public int? FileModelId { get; set; }

        /// <summary>
        /// Статус записи
        /// </summary>
        [Display(Name = "Статус записи")]
        public RowStatus RowStatus { get; set; }
        /// <summary>
        /// Статус записи
        /// </summary>
        [Display(Name = "Статус записи")]
        public int? RowStatusId { get; set; }

    }
}
