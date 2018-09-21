using KisVuzDotNetCore2.Models.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Преподаватель"
    /// </summary>
    public class Teacher
    {
        public int TeacherId { get; set; }

        [Display(Name ="ФИО преподавателя")]
        public string TeacherFio { get; set; }

        [Display(Name = "Аккаунт преподавателя")]
        public AppUser AppUser { get; set; }
        [Display(Name = "Аккаунт преподавателя")]
        public string AppUserId { get; set; }

        /// <summary>
        /// Профили подготовки и дисциплины преподавателя
        /// </summary>
        public List<TeacherEduProfileDisciplineName> TeacherEduProfileDisciplineNames { get; set; }

        /// <summary>
        /// Кафедра - Должность - Ставка - Дата установления ставки преподавателя 
        /// </summary>
        public List<TeacherStructKafPostStavka> TeacherStructKafPostStavka { get; set; }

        /// <summary>
        /// Дисциплины, которые ведёт преподаватель (для заполнения таблицы 16 "Информация о составе педагогических работников")
        /// </summary>
        public List<TeacherDiscipline> TeacherDisciplines { get; set; }

        /// <summary>
        /// Курируемые группы
        /// </summary>
        public List<StudentGroup> StudentGroupsOfKurator { get; set; }

        /// <summary>
        /// Методкомиссии, в которые входит преподаватель
        /// </summary>
        public List<TeacherMetodKomissiya> TeacherMetodKomissii { get; set; }
    }
}
