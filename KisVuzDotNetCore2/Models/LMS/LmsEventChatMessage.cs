using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Сообщение чата мероприятия
    /// </summary>
    public class LmsEventChatMessage
    {
        public int LmsEventChatMessageId { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [Display(Name = "Текст сообщения")]
        public string LmsEventChatMessageText { get; set; }

        /// <summary>
        /// Ссылка
        /// </summary>
        [Display(Name = "Ссылка")]
        public string LmsEventChatMessageLink { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        [Display(Name = "Отправитель")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }        
    }
}