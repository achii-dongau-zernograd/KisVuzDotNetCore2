using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Модель "Институты"
    /// </summary>
    public class StructInstitute
    {
        /// <summary>
        /// УИД института
        /// </summary>
        public int StructInstituteId { get; set; }

        /// <summary>
        /// Наименование института
        /// </summary>
        [Display(Name = "Наименование института")]
        public string StructInstituteName { get; set; }

        ////////// Навигационные свойства и поля ///////////
        /// <summary>
        /// Факультеты в составе института
        /// </summary>
        public List<StructFacultet> StructFacultets = new List<StructFacultet>();
    }
}