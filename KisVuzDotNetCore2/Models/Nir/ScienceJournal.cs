using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Научный журнал
    /// </summary>
    public class ScienceJournal
    {
        public int ScienceJournalId { get; set; }

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
        /// Список сопоставлений "Научный журнал - База цитирования"
        /// </summary>
        [Display(Name = "Базы цитирования")]
        public List<ScienceJournalCitationBase> ScienceJournalCitationBases { get; set; } = new List<ScienceJournalCitationBase>();
    }
}