﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Модель "Научная статья"
    /// </summary>
    public class Article
    {
        public int ArticleId { get; set; }

        /// <summary>
        /// Наименование статьи
        /// </summary>
        [Display(Name = "Наименование статьи")]
        public string ArticleName { get; set; }

        /// <summary>
        /// Авторы - статьи - авторские доли
        /// </summary>
        [Display(Name = "Авторы")]
        public List<ArticleAuthor> ArticleAuthors { get; set; }

        /// <summary>
        /// Научный журнал
        /// </summary>
        [Display(Name = "Научный журнал")]
        public ScienceJournal ScienceJournal { get; set; }
        /// <summary>
        /// Научный журнал
        /// </summary>
        [Display(Name = "Научный журнал")]
        public int ScienceJournalId { get; set; }

        /// <summary>
        /// Год публикации статьи
        /// </summary>
        [Display(Name = "Год публикации статьи")]
        public Year Year { get; set; }
        /// <summary>
        /// Год публикации статьи
        /// </summary>
        [Display(Name = "Год публикации статьи")]
        public int? YearId { get; set; }

        /// <summary>
        /// Номер выпуска
        /// </summary>
        [Display(Name = "Номер выпуска")]
        public string VipuskNumber { get; set; }

        /// <summary>
        /// Страницы
        /// </summary>
        [Display(Name = "Страницы")]
        public string Pages { get; set; }

        /// <summary>
        /// Список тем НИР, которым соответствует статья
        /// </summary>
        [Display(Name = "Темы НИР")]
        public List<ArticleNirTema> ArticleNirTemas { get; set; } = new List<ArticleNirTema>();

        /// <summary>
        /// Список специальностей научных работников, которым соответствует статья
        /// </summary>
        [Display(Name = "Научные специальности")]
        public List<ArticleNirSpecial> ArticleNirSpecials { get; set; } = new List<ArticleNirSpecial>();

        /// <summary>
        /// .pdf файл статьи
        /// </summary>
        [Display(Name = ".pdf файл статьи")]
        public FileModel FileModel { get; set; }
        [Display(Name = ".pdf файл статьи")]
        public int? FileModelId { get; set; }

        /// <summary>
        /// Статус записи
        /// </summary>
        [Display(Name = "Статус записи")]
        public RowStatus RowStatus { get; set; }
        /// <summary>
        /// Статус записи
        /// </summary>
        [Display(Name = "Статус записи")]
        public int? RowStatusId { get; set; }
    }
}
