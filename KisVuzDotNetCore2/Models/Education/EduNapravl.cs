﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Направление подготовки / специальность"
    /// </summary>
    public class EduNapravl
    {
        /// <summary>
        /// Идентификатор направления подготовки
        /// </summary>
        public int EduNapravlId { get; set; }

        /// <summary>
        /// Код направления подготовки
        /// </summary>
        [Display (Name= "Код направления подготовки")]
        public string EduNapravCode { get; set; }

        /// <summary>
        /// Наименование направления подготовки
        /// </summary>
        [Display (Name= "Наименование направления подготовки")]
        public string EduNapravName { get; set; }

        /// <summary>
        /// Идентификатор записи УГС
        /// </summary>
        public int EduUgsId { get; set; }

        /// <summary>
        /// УГС
        /// </summary>
        [Display(Name = "УГС")]
        public EduUgs EduUgs { get; set; }

    }
}