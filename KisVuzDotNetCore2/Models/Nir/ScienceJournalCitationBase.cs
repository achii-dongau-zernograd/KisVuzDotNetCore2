using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Научный журнал - База цитирования
    /// </summary>
    public class ScienceJournalCitationBase
    {
        public int ScienceJournalCitationBaseId { get; set; }

        /// <summary>
        /// Научный журнал
        /// </summary>
        [Display(Name = "Научный журнал")]
        public ScienceJournal ScienceJournal { get; set; }
        /// <summary>
        /// Научный журнал
        /// </summary>
        [Display(Name = "Научный журнал")]
        public int ScienceJournalId { get; set; }

        /// <summary>
        /// База цитирования
        /// </summary>
        [Display(Name = "База цитирования")]
        public CitationBase CitationBase { get; set; }
        /// <summary>
        /// База цитирования
        /// </summary>
        [Display(Name = "База цитирования")]
        public int CitationBaseId { get; set; }
    }
}