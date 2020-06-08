using KisVuzDotNetCore2.Models.Abitur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Событие (мероприятие), организовываемое
    /// с помощью системы дистанционного образования
    /// </summary>
    public class LmsEvent
    {
        public int LmsEventId { get; set; }

        /// <summary>
        /// Краткое описание мероприятия
        /// </summary>
        [Display(Name = "Краткое описание мероприятия")]
        public string Description { get; set; }

        /// <summary>
        /// Ссылка на мероприятие (при наличии).
        /// Указывается в случае вебинаров и
        /// использовании стронних ресурсов
        /// </summary>
        [Display(Name = "Ссылка на мероприятие (при наличии)")]
        public string WebLink { get; set; }

        /// <summary>
        /// Дата/время начала мероприятия
        /// </summary>
        [Display(Name = "Дата/время начала мероприятия")]
        public DateTime DateTimeStart { get; set; }

        /// <summary>
        /// Длительность мероприятия (по умолчанию 2 часа)
        /// </summary>
        [Display(Name = "Длительность мероприятия")]
        public TimeSpan Duration { get; set; } = new TimeSpan(2, 0, 0);

        /// <summary>
        /// Дата и время окончания мероприятия
        /// </summary>
        public DateTime DateTimeEnd
        {
            get
            {
                return DateTimeStart + Duration;
            }
        }

        /// <summary>
        /// Ручной запуск мероприятия
        /// </summary>
        [Display(Name = "Ручной запуск мероприятия")]
        public bool IsLmsEventStartedManual { get; set; }

        /// <summary>
        /// Определяет, проходит ли мероприятие в настоящий момент
        /// </summary>
        public bool IsLmsEventStarted
        {
            get
            {
                bool isLmsEventStarted = false;
                if (IsLmsEventStartedManual)
                {
                    isLmsEventStarted = true;
                }
                    

                var currentDateTime = DateTime.Now;
                if(currentDateTime > DateTimeStart && currentDateTime < DateTimeEnd)
                {
                    isLmsEventStarted = true;
                }

                return isLmsEventStarted;
            }
        }

        /// <summary>
        /// Тип мероприятия системы дистанционного образования
        /// </summary>
        [Display(Name = "Тип мероприятия")]
        public int LmsEventTypeId { get; set; }
        public LmsEventType LmsEventType { get; set; }

        /// <summary>
        /// Аккаунты пользователей, ассоциированные с мероприятием
        /// </summary>
        public List<AppUserLmsEvent> AppUserLmsEvents {get;set;}
                
        /// <summary>
        /// Дополнительные материалы к мероприятию
        /// </summary>
        public List<LmsEventAdditionalMaterial> LmsEventAdditionalMaterials { get; set; }

        /// <summary>
        /// Чат мероприятия
        /// </summary>
        public List<LmsEventChatMessage> LmsEventChatMessages { get; set; }

        /// <summary>
        /// Наборы заданий к мероприятию
        /// </summary>
        public List<LmsEventLmsTaskSet> LmsEventLmsTaskSets { get; set; }

        /// <summary>
        /// Полное описание мероприятия (время - описание)
        /// </summary>
        public string GetFullDescription
        {
            get
            {
                return $"{DateTimeStart} - {Description}";
            }
        }
    }
}
