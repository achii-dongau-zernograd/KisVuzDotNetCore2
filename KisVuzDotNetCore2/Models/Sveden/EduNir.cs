﻿using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Модель записи Таблицы 13. Образовательная программа (результаты НИР)
    /// </summary>
    public class EduNir
    {
        public int EduNirId { get; set; }

        [Display(Name ="Наименование специальности, направления подготовки")]
        public EduNapravl EduNapravl { get; set; }
        [Display(Name = "Наименование специальности, направления подготовки")]
        public int EduNapravlId { get; set; }

        /// <summary>
        /// Образовательная программа, направленность, профиль, шифр и наименование научной специальности
        /// </summary>
        [Display(Name = "Образовательная программа, направленность")]
        public EduProfile EduProfile { get; set; }
        [Display(Name = "Образовательная программа, направленность")]
        public int? EduProfileId { get; set; }

        /// <summary>
        /// Перечень научных направлений, в рамках которых ведётся научная (научно-исследовательская) деятельность
        /// </summary>
        [Display(Name = "Перечень научных направлений, в рамках которых ведётся научная (научно-исследовательская) деятельность")]
        public string NirNapravls { get; set; }

        /// <summary>
        /// Название научного направления/научной школы 
        /// </summary>
        [Display(Name = "Название научного направления/научной школы ")]
        public string NirNapravlSchool { get; set; }

        /// <summary>
        /// Количество НПР, принимающих участие в научной (научно-исследовательской) деятельности
        /// </summary>
        [Display(Name = "Количество НПР, принимающих участие в научной (научно-исследовательской) деятельности")]
        public string NumNpr { get; set; }

        /// <summary>
        /// Количество студентов, принимающих участие в научной (научно-исследовательской) деятельности
        /// </summary>
        [Display(Name = "Количество студентов, принимающих участие в научной (научно-исследовательской) деятельности")]
        public string NumStud { get; set; }

        /// <summary>
        /// Количество изданных монографий научно-педагогических работников образовательной организации по всем научным направлениям за последний год
        /// </summary>
        [Display(Name = "Количество изданных монографий научно-педагогических работников образовательной организации по всем научным направлениям за последний год")]
        public string NumMonograf { get; set; }

        /// <summary>
        /// Количество изданных и принятых к публикации статей в изданиях, рекомендованных ВАК для публикации научных работ за последний год
        /// </summary>
        [Display(Name = "Количество изданных и принятых к публикации статей в изданиях, рекомендованных ВАК для публикации научных работ за последний год")]
        public string NumArticles { get; set; }

        /// <summary>
        /// Количество изданных и принятых к публикации статей в зарубежных изданиях
        /// </summary>
        [Display(Name = "Количество изданных и принятых к публикации статей в зарубежных изданиях")]
        public string NumArticlesZ { get; set; }

        /// <summary>
        /// Количество патентов, полученных на разработки за последний год: российских
        /// </summary>
        [Display(Name = "Количество патентов, полученных на разработки за последний год: российских")]
        public string NumPatents { get; set; }

        /// <summary>
        /// Количество патентов, полученных на разработки за последний год: зарубежных
        /// </summary>
        [Display(Name = "Количество патентов, полученных на разработки за последний год: зарубежных")]
        public string NumPatentsZ { get; set; }

        /// <summary>
        /// Количество свидетельств о регистрации объекта интеллектуальной собственности, выданных на разработки за последний год: российских
        /// </summary>
        [Display(Name = "Количество свидетельств о регистрации объекта интеллектуальной собственности, выданных на разработки за последний год: российских")]
        public string NumSvidetelstv { get; set; }

        /// <summary>
        /// Количество свидетельств о регистрации объекта интеллектуальной собственности, выданных на разработки за последний год: зарубежных
        /// </summary>
        [Display(Name = "Количество свидетельств о регистрации объекта интеллектуальной собственности, выданных на разработки за последний год: зарубежных")]
        public string NumSvidetelstvZ { get; set; }

        /// <summary>
        /// Среднегодовой объём финансирования научных исследований на одного научно-педагогического работника организации (в приведённых к целочисленным значениям ставок)
        /// </summary>
        [Display(Name = "Среднегодовой объём финансирования научных исследований на одного научно-педагогического работника организации (в приведённых к целочисленным значениям ставок)")]
        public string NumFinance { get; set; }

        /// <summary>
        /// Сведения о научно-исследовательской базе для осуществления научной (научно-исследовательской) деятельности
        /// </summary>
        [Display(Name = "Сведения о научно-исследовательской базе для осуществления научной (научно-исследовательской) деятельности")]
        public string BaseNir { get; set; }
    }
}
