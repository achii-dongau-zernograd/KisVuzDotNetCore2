using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Профиль подготовки / направленность / специализация"
    /// </summary>
    public class EduProfile
    {
        /// <summary>
        /// УИД профиля подготовки
        /// </summary>
        [Display(Name = "Наименование профиля подготовки (направленности, специальности)")]
        public int EduProfileId { get; set; }

        /// <summary>
        /// Наименование профиля подготовки
        /// </summary>
        [Display(Name = "Наименование профиля подготовки (направленности, специальности)")]
        public string EduProfileName { get; set; }

        /// <summary>
        /// Учебные планы по профилю подготовки
        /// </summary>
        [Display(Name = "Учебные планы")]
        public List<EduPlan> EduPlans { get; set; }

        [Display(Name = "Направление специальности, направление подготовки")]
        public string GetEduProfileFullName
        {
            get
            {
                return EduNapravl?.EduUgs?.EduLevel?.EduLevelName + " – "
                    + EduNapravl?.EduNapravlCode + " – "
                    + EduNapravl?.EduNapravlName + " – ";
            }
        }

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
