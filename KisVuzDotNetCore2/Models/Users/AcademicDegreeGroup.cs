using System.Collections.Generic;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель "Группа ученых степеней"
    /// </summary>
    public class AcademicDegreeGroup
    {
        public int AcademicDegreeGroupId { get; set; }

        public string AcademicDegreeGroupName { get; set; }

        public List<AcademicDegree> AcademicDegrees { get; set; }
    }
}