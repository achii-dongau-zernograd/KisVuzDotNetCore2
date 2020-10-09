using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Внешний аккаунт пользователя
    /// </summary>
    public class UserAccountExternal
    {
        public int UserAccountExternalId { get; set; }

        /// <summary>
        /// Идентификатор аккаунта пользователя
        /// </summary>
        public string AppUserId { get; set; }
        /// <summary>
        /// Навигационное свойство Аккаунт пользователя
        /// </summary>
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Идентификатор Внешнего ресурса
        /// </summary>
        [Display(Name = "Внешний ресурс")]
        public int ExternalResourceId { get; set; }
        /// <summary>
        /// Навигационное свойство Внешний ресурс
        /// </summary>
        public ExternalResource ExternalResource { get; set; }

        /// <summary>
        /// Ссылка на внешний аккаунт пользователя
        /// </summary>
        [Display(Name = "Ссылка на внешний ресурс")]
        public string Link { get; set; }

    }
}
