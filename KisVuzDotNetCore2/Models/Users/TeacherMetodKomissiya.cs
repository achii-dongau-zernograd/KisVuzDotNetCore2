using KisVuzDotNetCore2.Models.Struct;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Преподаватели - Методкомиссии
    /// </summary>
    public class TeacherMetodKomissiya
    {
        public int TeacherMetodKomissiyaId { get; set; }

        /// <summary>
        /// Преподаватель
        /// </summary>
        [Display(Name ="Преподаватель")]
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }

        /// <summary>
        /// Методкомиссия
        /// </summary>
        [Display(Name = "Методкомиссия")]
        public MetodKomissiya MetodKomissiya { get; set; }
        public int MetodKomissiyaId { get; set; }
    }
}