using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель параметров поиска пользователей
    /// </summary>
    public class AppUserSearchModel
    {
        /// <summary>
        /// Фрагмент фамилии пользователя
        /// </summary>
        public string LastNameSearchFragment { get; set; }

        /// <summary>
        /// Фрагмент электронной почты
        /// </summary>
        public string EmailSearchFragment { get; set; }

        /// <summary>
        /// Статус пользователя
        /// </summary>
        public int? AppUserStatusId { get; set; }

        /// <summary>
        /// Флаг, указывающий на необходимость немедленной загрузки данных
        /// (из запроса GET)
        /// </summary>
        public bool IsRequestDataImmediately { get; set; }
    }
}
