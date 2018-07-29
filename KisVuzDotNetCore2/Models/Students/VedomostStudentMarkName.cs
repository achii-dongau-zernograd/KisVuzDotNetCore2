using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Students
{
    /// <summary>
    /// Модель "Наименование отметки по ведомости (справочник)"
    /// </summary>
    public class VedomostStudentMarkName
    {
        /// <summary>
        /// УИД наименования отметки по ведомости
        /// </summary>
        public int VedomostStudentMarkNameId { get; set; }

        /// <summary>
        /// Значение наименования отметки по ведомости
        /// </summary>
        [Display(Name = "Наименование отметки")]
        public string VedomostStudentMarkNameName { get; set; }
    }
}