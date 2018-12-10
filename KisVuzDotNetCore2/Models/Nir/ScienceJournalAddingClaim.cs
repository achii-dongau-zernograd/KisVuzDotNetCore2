using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Заявка на добавление научного журнала
    /// или сборника трудов конференции в справочник
    /// </summary>
    public class ScienceJournalAddingClaim
    {
        public int ScienceJournalAddingClaimId { get; set; }

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
        /// Наименование научного журнала
        /// </summary>
        [Display(Name = "Наименование научного журнала")]
        public string ScienceJournalName { get; set; }

        /// <summary>
        /// Флаг принадлежности журнала перечню ВАК
        /// </summary>
        [Display(Name = "ВАК")]
        public bool IsVak { get; set; }

        /// <summary>
        /// Признак зарубежного журнала
        /// </summary>
        [Display(Name = "Зарубежный журнал")]
        public bool IsZarubejn { get; set; }

        /// <summary>
        /// Ссылка на сведения о журнале в eLibrary
        /// </summary>
        [Display(Name = "Ссылка на сведения о журнале в eLibrary")]
        public string ELibraryLink { get; set; }

        /// <summary>
        /// Базы цитирования
        /// </summary>
        [Display(Name = "Базы цитирования")]
        public string CitationBasesList { get; set; }

        /// <summary>
        /// Статус заявки
        /// </summary>
        [Display(Name = "Статус заявки")]
        public RowStatus RowStatus { get; set; }
        /// <summary>
        /// Статус заявки
        /// </summary>
        [Display(Name = "Статус заявки")]
        public int? RowStatusId { get; set; }
    }
}
