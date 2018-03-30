using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Укрупнённая группа специальностей" (УГС)
    /// </summary>
    public class EduUgs
    {
        /// <summary>
        /// Идентификатор УГС
        /// </summary>
        [Display(Name = "Идентификатор УГС")]
        public int EduUgsId { get; set; }

        /// <summary>
        /// Код УГС
        /// </summary>
        [Display(Name = "Код УГС")]
        public string EduUgsCode { get; set; }

        /// <summary>
        /// Наименование УГС
        /// </summary>
        [Display(Name = "Наименование УГС")]
        public string EduUgsName { get; set; }

        /// <summary>
        /// Идентификатор уровня образования
        /// </summary>        
        public int EduLevelId { get; set; }

        /// <summary>
        /// Уровень образования
        /// </summary>
        public EduLevel EduLevel { get; set; }

        /// <summary>
        /// Направления подготовки / специальности
        /// </summary>
        public List<EduNapravl> EduNapravls = new List<EduNapravl>();

        public string EduUgsFullName
        {
            get
            {
                return EduLevel?.EduLevelName + " - " + EduUgsCode + " " + EduUgsName;
            }
        }

    }
}
