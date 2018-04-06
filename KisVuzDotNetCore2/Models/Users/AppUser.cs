﻿using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель пользователя приложения
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [DataType(DataType.Date)]        
        [Display(Name = "Дата рождения")]
        public DateTime? Birthdate { get; set; }

        /// <summary>
        /// Фотография пользователя
        /// </summary>
        public byte[] AppUserPhoto { get; set; }

        //////////////////        

        /// <summary>
        /// Уровень образования
        /// </summary>
        public EduLevelGroup EduLevelGroup { get; set; }
        public int? EduLevelGroupId { get; set; }               

        /// <summary> 
        /// Ученая степень
        /// </summary>
        public AcademicDegree AcademicDegree { get; set; }
        public int? AcademicDegreeId { get; set; }

        /// <summary>
        /// Ученое звание
        /// </summary>
        public AcademicStat AcademicStat { get; set; }
        public int? AcademicStatId { get; set; }

        /// <summary>
        /// Дата отсчета общего стажа работы
        /// (дата начала трудовой деятельности)
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата начала трудовой деятельности")]
        public DateTime? DateStartWorking { get; set; }

        /// <summary>
        /// Дата отсчета стажа работы по специальности
        /// (дата начала трудовой деятельности по специальности)
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата начала трудовой деятельности по специальности")]
        public DateTime? DateStartWorkingSpec { get; set; }

        /// <summary>
        /// Квалификации
        /// </summary>
        public List<Qualification> Qualifications { get; set; }        

        /// <summary>
        /// Занимаемые должности
        /// </summary>
        //public List<Post> Posts { get; set; }
    }
}