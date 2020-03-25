using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Struct;
using KisVuzDotNetCore2.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Students
{
    /// <summary>
    /// Модель "Студенческая группа"
    /// </summary>
    public class StudentGroup
    {
        /// <summary>
        /// УИД студенческой группы
        /// </summary>
        public int StudentGroupId { get; set; }

        #region Наименование группы (префикс, номер + правило именования)
        /// <summary>
        /// Префикс наименования группы (напр. ЭЛ, АБз, АГм)
        /// </summary>
        [Display(Name = "Префикс наименования группы (напр. АЭ, АБз, АГм)")]
        public string StudentGroupNamePrefix { get; set; }

        #region Порядковый номер группы в потоке
        /// <summary>
        /// Порядковый номер группы в потоке (в аббревиатуре АЭ21 1 - порядковый номер группы в потоке)
        /// </summary>
        [Display(Name = "Порядковый номер группы в потоке")]
        public int StudentGroupNumber { get; set; }
        #endregion

        /// <summary>
        /// Наименование группы
        /// </summary>
        [Display(Name = "Группа")]
        public string StudentGroupName
        {
            get
            {
                string studentGroupName = StudentGroupNamePrefix + "-" + EduKurs?.EduKursNumber + StudentGroupNumber.ToString();
                return studentGroupName;
            }
        }
        #endregion
        
        #region Профиль подготовки EduProfile
        /// <summary>
        /// Профиль подготовки
        /// </summary>
        [Display(Name = "Профиль подготовки")]
        public EduProfile EduProfile { get; set; }
        /// <summary>
        /// Профиль подготовки
        /// </summary>
        [Display(Name = "Профиль подготовки")]
        public int EduProfileId { get; set; }
        #endregion

        #region Форма обучения EduForm
        /// <summary>
        /// Форма обучения
        /// </summary>
        [Display(Name ="Форма обучения")]
        public EduForm EduForm { get; set; }
        /// <summary>
        /// Форма обучения
        /// </summary>
        [Display(Name = "Форма обучения")]
        public int EduFormId { get; set; }
        #endregion

        #region Курс EduKurs
        /// <summary>
        /// Курс
        /// </summary>
        [Display(Name ="Курс")]
        public EduKurs EduKurs { get; set; }
        /// <summary>
        /// Курс
        /// </summary>
        [Display(Name = "Курс")]
        public int EduKursId { get; set; }
        #endregion        

        #region Факультет
        /// <summary>
        /// Факультет
        /// </summary>
        [Display(Name ="Факультет")]
        public StructFacultet StructFacultet { get; set; }
        /// <summary>
        /// Факультет
        /// </summary>
        [Display(Name = "Факультет")]
        public int StructFacultetId { get; set; }
        #endregion               

        #region Куратор
        /// <summary>
        /// Куратор
        /// </summary>
        [Display(Name = "Куратор")]
        public Teacher Kurator { get; set; }
        /// <summary>
        /// Куратор
        /// </summary>
        [Display(Name ="Куратор")]
        public int KuratorId { get; set; }
        #endregion

        /// <summary>
        /// Староста
        /// </summary>
        //public Student Starosta { get; set; }
        //public int StarostaId { get; set; }
        ///////////
        /// <summary>
        /// Список студентов данной группы
        /// </summary>
        [Display(Name ="Список студентов")]
        public List<Student> Students { get; set; }

        /// <summary>
        /// Ведомости данной группы
        /// </summary>
        public List<Vedomost> Vedomosti { get; set; }

        /// <summary>
        /// Сообщения от пользователя всей студенческой группы
        /// </summary>
        public List<MessageFromAppUserToStudentGroup> MessagesFromAppUserToStudentGroup { get; set; }
    }
}
