using System;
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
        [Display(Name = "Код направления подготовки")]
        public string EduNapravlCode { get; set; }

        /// <summary>
        /// Наименование направления подготовки
        /// </summary>
        [Display(Name = "Наименование направления подготовки")]
        public string EduNapravlName { get; set; }

        /// <summary>
        /// Ссылка на файл образовательного стандарта ФГОС 3+
        /// </summary>
        [Display(Name = "Образовательный стандарт ФГОС 3+")]
        public string EduNapravlStandartDocLink { get; set; }

        /// <summary>
        /// Ссылка на файл образовательного стандарта ФГОС 3++
        /// </summary>
        [Display(Name = "Образовательный стандарт ФГОС 3++")]
        public string EduNapravlStandartDocLinkFgos3pp { get; set; }

        ////////// Навигационные свойства и поля ////////////

        /// <summary>
        /// Идентификатор записи УГС
        /// </summary>
        public int EduUgsId { get; set; }

        /// <summary>
        /// УГС
        /// </summary>
        [Display(Name = "УГС")]
        public EduUgs EduUgs { get; set; }

        /// <summary>
        /// Профили подготовки (направленности, специализации)
        /// </summary>
        public List<EduProfile> EduProfiles { get; set; }

        public string GetEduNapravlName
        {
            get
            {
                return EduNapravlCode + " - " + EduNapravlName;
            }
        }

        public string GetEduNapravlFullName
        {
            get
            {
                return EduUgs?.EduLevel?.EduLevelName + " - " + EduNapravlCode + " - " + EduNapravlName;
            }
        }

        /// <summary>
        /// Идентификатор записи квалификации
        /// </summary>
        public int EduQualificationId { get; set; }

        /// <summary>
        /// Квалификация по направлению
        /// </summary>
        [Display(Name = "Квалификация")]
        public EduQualification EduQualification { get; set; }

        /// <summary>
        /// Набор записей, свзывающих направление подготовки, форму обучения и нормативный срок обучения
        /// </summary>
        public List<EduNapravlEduFormEduSrok> EduNapravlEduFormEduSroks { get; set; }
    }
}