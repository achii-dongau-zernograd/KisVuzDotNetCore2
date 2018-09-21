using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    public class MetodKomissiya
    {
        public int MetodKomissiyaId { get; set; }

        /// <summary>
        /// Наименование методкомиссии
        /// </summary>
        [Display(Name = "Наименование методкомиссии")]
        public string MetodKomissiyaName { get; set; }

        /// <summary>
        /// Профили подготовки
        /// </summary>
        public List<MetodKomissiyaEduProfile> MetodKomissiyaEduProfiles { get; set; }

        /// <summary>
        /// Члены методкомиссии
        /// </summary>
        public List<TeacherMetodKomissiya> TeacherMetodKomissii { get; set; }
    }
}