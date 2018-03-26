using KisVuzDotNetCore2.Models.Education;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Модель "Кафедра"
    /// </summary>
    public class StructKaf
    {
        /// <summary>
        /// УИД кафедры
        /// </summary>
        public int StructKafId { get; set; }

        /// <summary>
        /// Наименование кафедры
        /// </summary>
        public int StructKafName { get; set; }

        /// <summary>
        /// Наименование кафедры (сокращённое)
        /// </summary>
        public int StructKafNameSokr { get; set; }

        /// <summary>
        /// Учебные планы
        /// </summary>
        public List<EduPlan> EduPlans = new List<EduPlan>();

        ////////// Навигационные свойства ///////////
        /// <summary>
        /// УИД факультета, которому принадлежит кафедра
        /// </summary>
        public int StructFacultetId { get; set; }

        /// <summary>
        /// Факультет, которому принадлежит кафедра
        /// </summary>
        public StructFacultet StructFacultet { get; set; }
    }
}