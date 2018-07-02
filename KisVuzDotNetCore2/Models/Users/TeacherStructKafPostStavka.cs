using KisVuzDotNetCore2.Models.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Преподаватель - Кафедра - Должность - Ставка - Дата установления ставки"
    /// </summary>
    public class TeacherStructKafPostStavka
    {
        public int TeacherStructKafPostStavkaId { get; set; }

        /// <summary>
        /// Преподаватель
        /// </summary>
        [Display(Name ="Преподаватель")]
        public Teacher Teacher { get; set; }
        [Display(Name = "Преподаватель")]
        public int TeacherId { get; set; }

        /// <summary>
        /// Кафедра
        /// </summary>
        [Display(Name ="Кафедра")]
        public StructKaf StructKaf { get; set; }
        [Display(Name = "Кафедра")]
        public int StructKafId { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [Display(Name ="Должность")]
        public Post Post { get; set; }
        [Display(Name = "Должность")]
        public int PostId { get; set; }

        /// <summary>
        /// Дата установки ставки
        /// </summary>
        [Display(Name ="Дата установки ставки")]
        [DataType(DataType.Date)]
        public DateTime StavkaDate { get; set; }

        /// <summary>
        /// Количество ставок
        /// </summary>
        [Display(Name = "Количество ставок")]
        public double Stavka { get; set; }
    }
}
