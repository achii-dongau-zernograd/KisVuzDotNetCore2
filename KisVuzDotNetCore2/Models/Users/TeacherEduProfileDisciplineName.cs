using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель закрепления за преподавателем профиля подготовки и дисциплины
    /// </summary>
    public class TeacherEduProfileDisciplineName
    {
        public int TeacherEduProfileDisciplineNameId { get; set; }

        [Display(Name = "Преподаватель")]
        public Teacher Teacher { get; set; }
        [Display(Name = "Преподаватель")]
        public int TeacherId { get; set; }

        [Display(Name ="Профиль подготовки")]
        public EduProfile EduProfile { get; set; }
        [Display(Name = "Профиль подготовки")]
        public int EduProfileId { get; set; }

        [Display(Name = "Дисциплина")]
        public DisciplineName DisciplineName { get; set; }
        [Display(Name = "Дисциплина")]
        public int DisciplineNameId { get; set; }
    }
}
