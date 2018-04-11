using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель "Группа ученых степеней"
    /// </summary>
    public class AcademicDegreeGroup
    {
        public int AcademicDegreeGroupId { get; set; }

        /// <summary>
        /// Наименование группы учёных степеней
        /// </summary>
        [Display(Name = "Наименование группы учёных степеней")]
        public string AcademicDegreeGroupName { get; set; }

        public List<AcademicDegree> AcademicDegrees { get; set; }
    }
}