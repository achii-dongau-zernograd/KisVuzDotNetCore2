using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Электронный журнал учета успеваемости обучающихся
    /// </summary>
    public class ElGradebook
    {
        /// <summary>
        /// УИД электронного журнала
        /// </summary>
        [Display(Name = "УИД электронного журнала")]
        public int ElGradebookId { get; set; }

        /// <summary>
        /// Наименование дисциплины
        /// </summary>
        [Display(Name = "Наименование дисциплины")]
        public string DisciplineName { get; set; }

        /// <summary>
        /// Учебный год
        /// </summary>
        [Display(Name = "Учебный год")]
        public string EduYear { get; set; }

        /// <summary>
        /// Номер семестра
        /// </summary>
        [Display(Name = "Номер семестра")]
        [Range(1, 12)]
        public int SemesterNumber { get; set; }

        /// <summary>
        /// Факультет
        /// </summary>
        [Display(Name = "Факультет")]
        public string Faculty { get; set; }

        /// <summary>
        /// Кафедра
        /// </summary>
        [Display(Name = "Кафедра")]
        public string Department { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        [Display(Name = "Курс")]
        [Range(1,6)]
        public int Course { get; set; }

        /// <summary>
        /// Наименование учебной группы
        /// </summary>
        [Display(Name = "Наименование учебной группы")]
        public string GroupName { get; set; }

        /// <summary>
        /// УИД учебной группы
        /// </summary>
        [Display(Name = "УИД учебной группы")]
        public int GroupId { get; set; }

        /// <summary>
        /// Преподаватели
        /// </summary>
        [Display(Name = "Преподаватели")]
        public List<ElGradebookTeacher> ElGradebookTeachers { get; set; }

        /// <summary>
        /// Студенты
        /// </summary>
        [Display(Name = "Список студентов")]
        public List<ElGradebookGroupStudent> ElGradebookGroupStudents { get; set; }

        /// <summary>
        /// Занятия
        /// </summary>
        [Display(Name = "Занятия")]
        public List<ElGradebookLesson> ElGradebookLessons { get; set; }
    }
}
