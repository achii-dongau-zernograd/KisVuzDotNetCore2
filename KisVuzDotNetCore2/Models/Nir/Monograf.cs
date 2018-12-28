using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Модель "Монографии"
    /// </summary>
    public class Monograf
    {
        public int MonografId { get; set; }

        /// <summary>
        /// Наименование монографии
        /// </summary>
        [Display(Name = "Наименование монографии")]
        public string MonografName { get; set; }

        /// <summary>
        /// Год публикации монографии
        /// </summary>
        [Display(Name = "Год публикации монографии")]
        public Year Year { get; set; }
        /// <summary>
        /// Год публикации монографии
        /// </summary>
        [Display(Name = "Год публикации монографии")]
        public int? YearId { get; set; }

        /// <summary>
        /// Авторы - монография - авторские доли
        /// </summary>
        [Display(Name = "Авторы")]
        public List<MonografAuthor> MonografAuthors { get; set; }

        /// <summary>
        /// Утверждено ученым советом АЧИИ
        /// </summary>
        [Display(Name = "Утверждено ученым советом АЧИИ")]
        public bool IsACHII { get; set; }

        /// <summary>
        /// Список тем НИР, которым соответствует монография
        /// </summary>
        [Display(Name = "Темы НИР")]
        public List<MonografNirTema> MonografNirTemas { get; set; }

        /// <summary>
        /// Список специальностей научных работников, которым соответствует монография
        /// </summary>
        [Display(Name = "Научные специальности")]
        public List<MonografNirSpecial> MonografNirSpecials { get; set; } = new List<MonografNirSpecial>();

        /// <summary>
        /// .pdf файл монографии
        /// </summary>
        [Display(Name = ".pdf файл монографии")]
        public FileModel FileModel { get; set; }
        [Display(Name = ".pdf файл монографии")]
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
