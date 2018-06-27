using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Наименование части блока дисциплин Учебного плана
    /// </summary>
    public class BlokDisciplChastName
    {
        public int BlokDisciplChastNameId { get; set; }
        [Display(Name = "Наименование части Блока дисциплин")]
        public string BlokDisciplChastNameName { get; set; }
    }
}