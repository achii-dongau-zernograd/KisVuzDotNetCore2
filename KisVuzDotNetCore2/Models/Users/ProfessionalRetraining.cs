using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Профессиональная переподготовка"
    /// </summary>
    public class ProfessionalRetraining
    {
        public int ProfessionalRetrainingId { get; set; }

        /// <summary>
        /// Регистрационный номер диплома о профессиональной переподготовке
        /// </summary>
        [Display(Name = "Регистрационный номер диплома о профессиональной переподготовке")]
        public string ProfessionalRetrainingDiplomRegNumber { get; set; }

        /// <summary>
        /// Номер диплома о профессиональной переподготовке
        /// </summary>
        [Display(Name = "Номер диплома о профессиональной переподготовке")]
        public string ProfessionalRetrainingDiplomNumber { get; set; }

        /// <summary>
        /// Наименование программы профессиональной переподготовки
        /// </summary>
        [Display(Name = "Наименование программы профессиональной переподготовки")]
        public string ProfessionalRetrainingProgramName { get; set; }

        /// <summary>
        /// Количество часов
        /// </summary>
        [Display(Name = "Количество часов")]
        public int ProfessionalRetrainingHours { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [Display(Name = "Город")]
        public string ProfessionalRetrainingCity { get; set; }

        /// <summary>
        /// Учебное заведение
        /// </summary>
        [Display(Name = "Учебное заведение")]
        public string ProfessionalRetrainingInstitition { get; set; }

        /// <summary>
        /// Дата начала прохождения проф. преподготовки
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата начала прохождения профессиональной переподготовки")]
        public DateTime ProfessionalRetrainingDateStart { get; set; }

        /// <summary>
        /// Дата окончания прохождения проф. преподготовки
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата окончания прохождения профессиональной переподготовки")]
        public DateTime ProfessionalRetrainingDateFinish { get; set; }

        /// <summary>
        /// Дата выдачи диплома о профессиональной переподготовке
        /// </summary>
        [Display(Name = "Дата выдачи диплома о профессиональной переподготовке")]
        [DataType(DataType.Date)]
        public DateTime ProfessionalRetrainingDateIssue { get; set; }

        /// <summary>
        /// Скан диплома о профессиональной переподготовке
        /// </summary>
        [Display(Name = "Копия диплома о профессиональной переподготовке")]
        public FileModel ProfessionalRetrainingFile { get; set; }
        [Display(Name = "Скан диплома о профессиональной переподготовке")]
        public int ProfessionalRetrainingFileId { get; set; }

        /// <summary>
        /// Аккаунт пользователя
        /// </summary>
        [Display(Name = "Пользователь")]
        public AppUser AppUser { get; set; }
        [Display(Name = "Аккаунт пользователя")]
        public string AppUserId { get; set; }

        
        public string GetProfessionalRetrainingString
        {
            get
            {
                return $"Диплом о профессиональной переподготовке {ProfessionalRetrainingDiplomNumber} " +
                    $"от {ProfessionalRetrainingDateIssue} " +
                    $"по программе {ProfessionalRetrainingProgramName} ({ProfessionalRetrainingHours} ч)," +
                    $"{ProfessionalRetrainingCity}, {ProfessionalRetrainingDateIssue.Year} г.";
            }
        }
    }
}
