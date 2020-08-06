using KisVuzDotNetCore2.Models.Priem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Абитуриент
    /// </summary>
    public class Abiturient
    {
        public int AbiturientId { get; set; }

        [Display(Name ="Пользователь")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
        /// <summary>
        /// Необходимость общежития
        /// </summary>
        [Display(Name = "Необходимость общежития")]
        public bool IsHostelRequired { get; set; } = false;

        /// <summary>
        /// Статус абитуриента
        /// </summary>
        [Display(Name ="Статус абитуриента")]
        public int AbiturientStatusId { get; set; }
        public AbiturientStatus AbiturientStatus { get; set; }

        /// <summary>
        /// Номер группы для прохождения вступительных испытаний
        /// </summary>
        [Display(Name = "Номер группы для прохождения вступительных испытаний")]
        public int? EntranceTestGroupId { get; set; }

        /// <summary>
        /// УИД способа подачи документов (лично, дистанционно)
        /// </summary>
        [Display(Name = "УИД способа подачи документов")]
        public int? SubmittingDocumentsTypeId { get; set; }

        /// <summary>
        /// Способ подачи документов (лично, дистанционно)
        /// </summary>
        [Display(Name = "Способ подачи документов")]
        public SubmittingDocumentsType SubmittingDocumentsType { get; set; }

        /// <summary>
        /// Предоставлен оригинал документа об образовании
        /// </summary>
        [Display(Name = "Предоставлен оригинал документа об образовании")]
        public bool? IsEduDocumentOriginal { get; set; }

        /// <summary>
        /// Заявления о приёме
        /// </summary>
        public List<ApplicationForAdmission> ApplicationForAdmissions { get; set; }

        /// <summary>
        /// Индивидуальные достижения абитуриента
        /// </summary>
        public List<AbiturientIndividualAchievment> AbiturientIndividualAchievments { get; set; }
        
        /// <summary>
        /// Консультанты абитуриента
        /// </summary>
        public List<AppUserAbiturientConsultant> AppUserAbiturientConsultants { get; set; }

        /// <summary>
        /// Бланки регистрации абитуриентов на вступительные испытания
        /// </summary>
        public List<EntranceTestRegistrationForm> EntranceTestRegistrationForms { get; set; }

        /// <summary>
        /// Протоколы вступительных испытаний
        /// </summary>
        public List<EntranceTestsProtocol> EntranceTestsProtocols { get; set; }        
        

        /// <summary>
        /// Возвращает строку описания абитуриента, включающую
        /// ФИО, дату рождения и email
        /// </summary>
        public string AbiturientFioBirthdayEmail
        {
            get
            {
                return $"{AppUser?.GetFullName ?? " - "}, {(AppUser?.Birthdate != null ? ((DateTime)AppUser?.Birthdate).ToString("d") : " - ")}, {AppUser?.Email ?? " - "}, { AppUser?.PhoneNumber ?? " - "}";
            }
        }
    }
}
