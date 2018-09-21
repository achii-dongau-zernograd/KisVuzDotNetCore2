using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Методкомиссия - Профиль подготовки
    /// </summary>
    public class MetodKomissiyaEduProfile
    {
        public int MetodKomissiyaEduProfileId { get; set; }

        [Display(Name = "Методкомиссия")]
        public MetodKomissiya MetodKomissiya { get; set; }
        public int MetodKomissiyaId { get; set; }

        [Display(Name = "Профиль подготовки")]
        public EduProfile EduProfile { get; set; }
        public int EduProfileId { get; set; }
    }
}