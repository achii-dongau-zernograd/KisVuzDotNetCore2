using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Education;
using System;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Students
{
    /// <summary>
    /// Сообщение от пользователя для всей студенческой группы
    /// </summary>
    public class MessageFromAppUserToStudentGroup
    {
        public int Id { get; set; }

        /// <summary>
        /// Дата и время отправки сообщения
        /// </summary>
        [Display(Name = "Дата и время отправки сообщения")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// УИД пользователя-отправителя сообщения
        /// </summary>
        [Display(Name = "УИД пользователя-отправителя сообщения")]
        public string AppUserId { get; set; }

        /// <summary>
        /// УИД пользователя-отправителя сообщения
        /// </summary>
        [Display(Name = "Пользователь-отправитель сообщения")]
        public AppUser AppUser { get; set; }

        /// <summary>
        /// УИД группы-получателя сообщения
        /// </summary>
        [Display(Name = "УИД группы-получателя сообщения")]
        public int StudentGroupId { get; set; }

        /// <summary>
        /// Группа-получатель сообщения
        /// </summary>
        [Display(Name = "Группа-получатель сообщения")]
        public StudentGroup StudentGroup { get; set; }

        /// <summary>
        /// УИД дисциплины (если сообщение предполагает передачу информации по какой-либо учебной дисциплине)
        /// </summary>
        [Display(Name = "УИД дисциплины")]
        public int? DisciplineNameId { get; set; }

        /// <summary>
        /// Дисциплина (если сообщение предполагает передачу информации по какой-либо учебной дисциплине)
        /// </summary>
        [Display(Name = "Дисциплина")]
        public DisciplineName DisciplineName { get; set; }

        /// <summary>
        /// УИД типа сообщения
        /// </summary>
        [Display(Name = "УИД типа сообщения")]
        public int UserMessageTypeId { get; set; }

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [Display(Name = "Тип сообщения")]
        public UserMessageType UserMessageType { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [Display(Name = "Текст сообщения")]
        public string MessageText { get; set; }

        /// <summary>
        /// Ссылка
        /// </summary>
        [Display(Name = "Ссылка")]
        public string Link { get; set; }
    }
}