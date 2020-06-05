using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Задание в системе дистанционного образования
    /// </summary>
    public class LmsTask
    {
        /// <summary>
        /// УИД задания
        /// </summary>
        public int LmsTaskId { get; set; }

        /// <summary>
        /// Тип задания
        /// </summary>
        [Display(Name ="Тип задания")]
        public int LmsTaskTypeId { get; set; }
        public LmsTaskType LmsTaskType { get; set; }

        /// <summary>
        /// Текст задания
        /// </summary>
        [Display(Name = "Текст задания")]
        public string LmsTaskText { get; set; }
         
        /// <summary>
        /// jpg-файл с заданием
        /// </summary>
        [Display(Name = "jpg-файл с заданием")]
        public int? LmsTaskJpgId { get; set; }
        public FileModel LmsTaskJpg { get; set; }

        /// <summary>
        /// Количество баллов
        /// </summary>
        [Display(Name = "Количество баллов")]
        public int NumberOfPoints { get; set; } = 5;

        /// <summary>
        /// Автор
        /// </summary>
        [Display(Name = "Автор")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Дата и время создания задания
        /// </summary>
        [Display(Name = "Дата и время создания задания")]
        public DateTime DateTimeOfCreation { get; set; }

        /// <summary>
        /// Список сопоставлений задания с наименованиями учебных дисциплин
        /// </summary>
        public List<LmsTaskDisciplineName> LmsTaskDisciplineNames { get; set; }

        /// <summary>
        /// Варианты ответов
        /// </summary>
        public List<LmsTaskAnswer> LmsTaskAnswers { get; set; }
    }
}
