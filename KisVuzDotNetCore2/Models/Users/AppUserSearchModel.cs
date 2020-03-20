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
        public string LastNameSearchFragment { get; internal set; }
    }
}
