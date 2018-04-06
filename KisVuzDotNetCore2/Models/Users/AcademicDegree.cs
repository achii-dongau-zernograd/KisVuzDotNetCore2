namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель "Ученая степень"
    /// </summary>
    public class AcademicDegree
    {
        public int AcademicDegreeId { get; set; }

        public string AcademicDegreeName { get; set; }

        /// <summary>
        /// Группа ученых степеней
        /// </summary>
        public AcademicDegreeGroup AcademicDegreeGroup{get;set;}
        public int AcademicDegreeGroupId { get; set; }
    }
}