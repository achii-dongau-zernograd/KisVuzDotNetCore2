using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Обобщенный уровень образования
    /// </summary>
    public class EduLevelGroup
    {
        public int EduLevelGroupId { get; set; }

        /// <summary>
        /// Наименование группы уровней образования
        /// </summary>
        [Display(Name = "Наименование группы уровней образования")]
        public string EduLevelGroupName { get; set; }
    }
}