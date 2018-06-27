using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Блок дисциплин Учебного плана
    /// </summary>
    public class BlokDisciplName
    {
        public int BlokDisciplNameId { get; set; }
        [Display(Name = "Наименование блока дисциплин")]
        public string BlokDisciplNameName { get; set; }
    }
}