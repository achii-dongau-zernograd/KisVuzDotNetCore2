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
    public class ScienceJournalClaimAdd
    {
        public int ScienceJournalClaimAddId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        AppUser AppUser { get; set; }
        string AppUserId { get; set; }

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
    }
}
