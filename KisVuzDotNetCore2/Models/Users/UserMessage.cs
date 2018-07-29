using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Сообщение от пользователя пользователю"
    /// </summary>
    public class UserMessage
    {
        /// <summary>
        /// УИД сообщения
        /// </summary>
        public int UserMessageId { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        [Display(Name = "Отправитель")]
        public AppUser UserSender { get; set; }
        /// <summary>
        /// Отправитель
        /// </summary>
        [Display(Name ="Отправитель")]
        public string UserSenderId { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        [Display(Name = "Получатель")]
        public AppUser UserReceiver { get; set; }
        /// <summary>
        /// Получатель
        /// </summary>
        [Display(Name = "Получатель")]
        public string UserReceiverId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата отправки")]
        public DateTime UserMessageDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [Required]
        [Display(Name ="Текст сообщения")]
        public string UserMessageText { get; set; }

        /// <summary>
        /// Признак прочтения сообщения получателем
        /// </summary>        
        [Display(Name = "Признак прочтения сообщения получателем")]
        public bool? IsReadedByReceiver { get; set; }
    }
}
