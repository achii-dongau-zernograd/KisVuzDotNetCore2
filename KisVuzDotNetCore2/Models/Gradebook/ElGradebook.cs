using System;
using System.Collections.Generic;
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
        public int ElGradebookId { get; set; }

        /// <summary>
        /// Наименование дисциплины
        /// </summary>
        public string DisciplineName { get; set; }

        /// <summary>
        /// Учебный год
        /// </summary>
        public string EduYear { get; set; }

        /// <summary>
        /// Номер семестра
        /// </summary>
        public int SemesterNumber { get; set; }

        /// <summary>
        /// Факультет
        /// </summary>
        public string Faculty { get; set; }

        /// <summary>
        /// Кафедра
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Наименование учебной группы
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// УИД учебной группы
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Преподаватели
        /// </summary>
        public List<ElGradebookTeacher> ElGradebookTeachers { get; set; }

        /// <summary>
        /// Студенты
        /// </summary>
        public List<ElGradebookGroupStudent> ElGradebookGroupStudents { get; set; }

        /// <summary>
        /// Занятия
        /// </summary>
        public List<ElGradebookLesson> ElGradebookLessons { get; set; }
    }
}
