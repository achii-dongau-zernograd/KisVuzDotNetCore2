using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Users;
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
        /// Учебные планы
        /// </summary>
        public List<EduPlan> EduPlans = new List<EduPlan>();

        /// <summary>
        /// Преподаватель - Кафедра - Должность - Ставка - Дата установления ставки
        /// </summary>
        public List<TeacherStructKafPostStavka> TeacherStructKafPostStavka { get; set; }

        ////////// Навигационные свойства ///////////
        /// <summary>
        /// УИД факультета, которому принадлежит кафедра
        /// </summary>
        public int StructFacultetId { get; set; }

        /// <summary>
        /// Факультет, которому принадлежит кафедра
        /// </summary>
        public StructFacultet StructFacultet { get; set; }

        public int StructSubvisionId { get; set; }
        public StructSubvision StructSubvision { get; set; }
    }
}