using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Priem
{
    public class EduPerevod
    {
        /// <summary>
        /// Модель шаблона заполнения таблицы "Образование" (Информация о результатах перевода, восстановления и отчисления)
        /// </summary>
        public int EduPerevodId { get; set; }

        [Display(Name = "Наименование специальности, направления подготовки")]
        public EduNapravl EduNapravl { get; set; }
        public int EduNapravlId { get; set; }

        [Display(Name = "Форма обучения")]
        public EduForm EduForm { get; set; }
        public int EduFormId { get; set; }

        [Display(Name = "Численность обучающихся, переведенных в другие образовательные организации")]
        public string NumberOutPerevod { get; set; }

        [Display(Name = "Численность обучающихся, переведенных из других образовательных организаций")]
        public string NumberToPerevod { get; set; }

        [Display(Name = "Численность восстановленных обучающихся")]
        public string NumberResPerevod { get; set; }

        [Display(Name = "Численность отчисленных обучающихся")]
        public string NumberExpPerevod { get; set; }
    }
}
