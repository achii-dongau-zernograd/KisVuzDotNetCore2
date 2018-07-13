﻿using System;
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
    }
}