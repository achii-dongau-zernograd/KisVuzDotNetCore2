using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Вспомогательный класс для таблицы 11.
    /// Формулировки объёма программы "за 1 год обучения" и т.д.
    /// </summary>
    public class EduOPEduYearName
    {
        public int EduOPEduYearNameId { get; set; }
        [Display(Name="Наименование")]
        public string EduOPEduYearNameName { get; set; }
    }
}