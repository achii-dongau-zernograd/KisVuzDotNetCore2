using KisVuzDotNetCore2.Models.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Бланк регистрации абитуриента на вступительное испытание
    /// </summary>
    public class EntranceTestRegistrationForm
    {
        public int EntranceTestRegistrationFormId { get; set; }

        /// <summary>
        /// Шифр
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Наименование дисциплины
        /// </summary>
        [Display(Name = "Наименование дисциплины")]
        public string DisciplineName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        /// <summary>
        /// Документ: серия
        /// </summary>
        [Display(Name = "Документ: серия")]
        public string DocumentSeriya { get; set; }

        /// <summary>
        /// Документ: номер
        /// </summary>
        [Display(Name = "Документ: номер")]
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// Дата проведения
        /// </summary>
        [Display(Name = "Дата проведения")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Согласие пользователя
        /// </summary>
        [Display(Name = "Согласие пользователя")]
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Результат вступительного испытания (количество баллов)
        /// </summary>
        [Display(Name = "Результат вступительного испытания (количество баллов)")]
        public string EntranceTestResult { get; set; }



        /// <summary>
        /// Абитуриент
        /// </summary>
        public Abiturient Abiturient { get; set; }
        public int AbiturientId { get; set; }

        /// <summary>
        /// Событие СДО
        /// </summary>
        public LmsEvent LmsEvent { get; set; }
        public int LmsEventId { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        [Display(Name = "Имя файла")]
        public string FileName { get; set; }
    }
}
