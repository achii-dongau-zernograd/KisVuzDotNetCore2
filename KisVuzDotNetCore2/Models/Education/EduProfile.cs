using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Профиль подготовки / направленность / специализация
    /// </summary>
    public class EduProfile
    {
        /// <summary>
        /// УИД профиля подготовки
        /// </summary>
        public int EduProfileId { get; set; }

        /// <summary>
        /// Наименование профиля подготовки
        /// </summary>
        [Display(Name = "Наименование профиля подготовки (направленности, специальности)")]
        public string EduProfileName { get; set; }


        //public StructKaf


        public List<EduPlan> EduPlans = new List<EduPlan>();


        ////////// Навигационные свойства ///////////////
        /// <summary>
        /// Код направления подготовки,
        /// которому принадлежит данный профиль
        /// </summary>
        public int EduNapravlId { get; set; }

        /// <summary>
        /// Объект направления подготовки,
        /// которому принадлежит данный профиль
        /// </summary>
        public EduNapravl EduNapravl { get; set; }        
    }
}
