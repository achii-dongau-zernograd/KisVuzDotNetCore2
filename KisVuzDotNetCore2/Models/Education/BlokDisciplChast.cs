using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Часть Блока дисциплины Учебного плана
    /// </summary>
    public class BlokDisciplChast
    {
        public int BlokDisciplChastId { get; set; }

        [Display(Name = "Блок дисциплин учебного плана")]
        public BlokDiscipl BlokDiscipl { get; set; }
        public int BlokDisciplId { get; set; }

        [Display(Name = "Наименование части Блока дисциплин")]
        public BlokDisciplChastName BlokDisciplChastName { get; set; }
        public int BlokDisciplChastNameId { get; set; }

        [Display(Name = "Наименование дисциплины")]
        public List<Discipline> Disciplines { get; set; }
    }
}