using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Способ подачи документов (лично, дистанционно)
    /// </summary>
    public class SubmittingDocumentsType
    {
        public int SubmittingDocumentsTypeId { get; set; }

        /// <summary>
        /// Наименование способа подачи документов
        /// </summary>
        [Display(Name = "Наименование способа подачи документов")]
        public string SubmittingDocumentsTypeName { get; set; }
    }
}