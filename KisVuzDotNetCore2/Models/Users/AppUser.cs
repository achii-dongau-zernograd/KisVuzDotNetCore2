using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.LMS;
using KisVuzDotNetCore2.Models.Nir;
using KisVuzDotNetCore2.Models.Struct;
using KisVuzDotNetCore2.Models.Students;
using KisVuzDotNetCore2.Models.UchPosobiya;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель пользователя приложения
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Пароль (только для абитуриентов и студентов)
        /// </summary>
        [Display(Name = "Пароль (только для абитуриентов и студентов)")]
        public string Password { get; set; }

        /// <summary>
        /// Дата и время регистрации пользователя в системе
        /// </summary>
        [Display(Name = "Дата и время регистрации пользователя в системе")]
        public DateTime? RegisterDateTime { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        /// <summary>
        /// ФИО полностью
        /// </summary>
        [Display(Name = "ФИО")]
        public string GetFullName
        {
            get
            {
                return LastName + " " + FirstName + " " + Patronymic;
            }
        }


        /// <summary>
        /// Дата рождения
        /// </summary>
        [DataType(DataType.Date)]        
        [Display(Name = "Дата рождения")]
        public DateTime? Birthdate { get; set; }

        /// <summary>
        /// Фотография пользователя
        /// </summary>
        public byte[] AppUserPhoto { get; set; }

        //////////////////        

        /// <summary>
        /// Уровень образования
        /// </summary>
        public EduLevelGroup EduLevelGroup { get; set; }
        public int? EduLevelGroupId { get; set; }               

        /// <summary> 
        /// Ученая степень
        /// </summary>
        public AcademicDegree AcademicDegree { get; set; }
        public int? AcademicDegreeId { get; set; }

        /// <summary>
        /// Ученое звание
        /// </summary>
        public AcademicStat AcademicStat { get; set; }
        public int? AcademicStatId { get; set; }

        /// <summary>
        /// Дата отсчета общего стажа работы
        /// (дата начала трудовой деятельности)
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата начала трудовой деятельности")]
        public DateTime? DateStartWorking { get; set; }

        /// <summary>
        /// Дата отсчета стажа работы по специальности
        /// (дата начала трудовой деятельности по специальности)
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата начала трудовой деятельности по специальности")]
        public DateTime? DateStartWorkingSpec { get; set; }

        /// <summary>
        /// Статус пользователя
        /// </summary>
        [Display(Name = "Статус пользователя")]
        public AppUserStatus AppUserStatus { get; set; }
        public int? AppUserStatusId { get; set; }

        /// <summary>
        /// Квалификации
        /// </summary>
        public List<Qualification> Qualifications { get; set; }

        /// <summary>
        /// Повышение квалификации
        /// </summary>
        public List<RefresherCourse> RefresherCourses { get; set; }

        /// <summary>
        /// Профессиональная переподготовка
        /// </summary>
        public List<ProfessionalRetraining> ProfessionalRetrainings { get; set; }

        /// <summary>
        /// Работы пользователя
        /// </summary>
        public List<UserWork> UserWorks { get; set; }

        /// <summary>
        /// Достижения пользователя
        /// </summary>
        public List<UserAchievment> UserAchievments { get; set; }

        /// <summary>
        /// Отображение пользователя как студента образовательной организации
        /// </summary>
        public List<Student> Students { get; set; }

        /// <summary>
        /// Отображение пользователя как преподавателя образовательной организации
        /// </summary>
        public List<Teacher> Teachers { get; set; }

        /// <summary>
        /// Отображение пользователя как автора учебных пособий
        /// </summary>
        public List<Author> Author { get; set; }

        /// <summary>
        /// Заявки пользователя на добавление
        /// научного журнала (сборника) в справочник
        /// </summary>
        public List<ScienceJournalAddingClaim> ScienceJournalAddingClaims { get; set; }

        /// <summary>
        /// Структурные подразделения, должности и формы занятости пользователя
        /// </summary>
        public List<AppUserStructSubvision> AppUserStructSubvisions { get; set; }

        /// <summary>
        /// Структурные подразделения, которыми руководит пользователь
        /// </summary>
        public List<StructSubvision> StructSubvisions { get; set; }

        /// <summary>
        /// Сообщения от пользователя для студенческих групп
        /// </summary>
        public List<MessageFromAppUserToStudentGroup> MessagesFromAppUserToStudentGroups { get; set; }

        /// <summary>
        /// Документы пользователя
        /// </summary>
        public List<UserDocument> UserDocuments { get; set; }

        /// <summary>
        /// Паспортные данные пользователя
        /// </summary>
        public PassportData PassportData { get; set; }

        /// <summary>
        /// Личное дело абитуриента
        /// </summary>
        public Abiturient Abiturient { get; set; }

        /// <summary>
        /// Адрес текущего проживания
        /// </summary>
        [Display(Name = "Адрес текущего проживания")]
        public int? AddressCurrentId { get; set; }
        public Address AddressCurrent { get; set; }

        /// <summary>
        /// Отношение к военной службе
        /// </summary>
        [Display(Name = "Отношение к военной службе")]
        public int? MilitaryServiceStatusId { get; set; }
        public MilitaryServiceStatus MilitaryServiceStatus { get; set; }

        /// <summary>
        /// Гражданство
        /// </summary>
        [Display(Name = "Гражданство")]
        public string Citizenship { get; set; }

        /// <summary>
        /// Национальность
        /// </summary>
        [Display(Name = "Национальность")]
        public string Nationality { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        [Display(Name = "Пол")]
        public int? GenderId { get; set; }
        public Gender Gender { get; set; }

        /// <summary>
        /// Контактные данные членов семьи
        /// </summary>
        [Display(Name = "Контактные данные членов семьи")]
        public List<FamilyMemberContact> FamilyMemberContacts { get; set; }

        /// <summary>
        /// Иностранные языки, которыми владеет пользователь
        /// </summary>
        [Display(Name = "Иностранные языки")]
        public List<AppUserForeignLanguage> AppUserForeignLanguages { get; set; }

        /// <summary>
        /// Сведения об образовании пользователя
        /// </summary>
        public List<UserEducation> UserEducations { get; set; }

        /// <summary>
        /// Список привязок пользователя к мероприятиям СДО
        /// </summary>
        public List<AppUserLmsEvent> AppUserLmsEvents { get; set; }

        /// <summary>
        /// Список договоров, заключенных между пользователем и образовательной организацией
        /// </summary>
        public List<Contract> Contracts { get; set; }
    }
}
