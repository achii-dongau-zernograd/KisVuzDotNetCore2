using KisVuzDotNetCore2.Models.Struct;
using KisVuzDotNetCore2.Models.Students;
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
                    + EduNapravl?.EduNapravlName + " – "
                    + EduProfileName;
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

        /// <summary>
        /// Темы НИР
        /// </summary>
        public List<NirTemaEduProfile> NirTemaEduProfileList { get; set; }

        /// <summary>
        /// Студенческие группы, обучающиеся по данному профилю подготовки
        /// </summary>
        public List<StudentGroup> StudentGroups { get; set; }

        /// <summary>
        /// Образовательные программы по данному профилю подготовки
        /// </summary>
        public List<EduProgram> EduPrograms { get; set; }

        /// <summary>
        /// Список привязок "Методкомиссия - Профиль подготовки"
        /// </summary>
        public List<MetodKomissiyaEduProfile> MetodKomissiyaEduProfiles { get; set; }
    }
}
