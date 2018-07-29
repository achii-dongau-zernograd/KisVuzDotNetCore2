using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Students
{
    /// <summary>
    /// Ведомость промежуточной аттестации
    /// </summary>
    public class Vedomost
    {
        /// <summary>
        /// УИД ведомости
        /// </summary>
        public int VedomostId { get; set; }

        /// <summary>
        /// Учебный год
        /// </summary>
        [Display(Name ="Учебный год")]
        public EduYear EduYear { get; set; }
        /// <summary>
        /// Учебный год
        /// </summary>
        [Display(Name = "Учебный год")]
        public int EduYearId { get; set; }

        /// <summary>
        /// Группа
        /// </summary>
        [Display(Name = "Группа")]
        public StudentGroup StudentGroup { get; set; }
        /// <summary>
        /// Группа
        /// </summary>
        [Display(Name = "Группа")]
        public int StudentGroupId { get; set; }

        /// <summary>
        /// Семестр
        /// </summary>
        [Display(Name ="Семестр")]
        public SemestrName SemestrName { get; set; }
        /// <summary>
        /// Семестр
        /// </summary>
        [Display(Name = "Семестр")]
        public int SemestrNameId { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        [Display(Name = "Курс")]
        public int Kurs
        {
            get
            {
                if (SemestrName == null) return 0;
                switch(SemestrName.SemestrNameNumber)
                {
                    case 1:
                    case 2:
                        return 1;
                    case 3:
                    case 4:
                        return 2;
                    case 5:
                    case 6:
                        return 3;
                    case 7:
                    case 8:
                        return 4;
                    case 9:
                    case 10:
                        return 5;
                    case 11:
                    case 12:
                        return 6;
                    default:
                        return SemestrName.SemestrNameNumber/2 + SemestrName.SemestrNameNumber % 2;
                }                
            }
        }

        /// <summary>
        /// Дисциплина
        /// </summary>
        [Display(Name ="Дисциплина")]
        public string DisciplineName { get; set; }

        /// <summary>
        /// Оценки студентов по данной ведомости
        /// </summary>
        public List<VedomostStudentMark> VedomostStudentMarks { get; set; }
    }
}
