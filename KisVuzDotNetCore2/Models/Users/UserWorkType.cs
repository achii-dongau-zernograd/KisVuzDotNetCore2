using System.Collections.Generic;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Тип работы пользователя"
    /// </summary>
    public class UserWorkType
    {
        /// <summary>
        /// УИД типа работы пользователя
        /// </summary>
        public int UserWorkTypeId { get; set; }

        /// <summary>
        /// Наименование типа работы пользователя
        /// </summary>
        public string UserWorkTypeName { get; set; }

        /// <summary>
        /// Работы пользователей, входящие в данную группу
        /// </summary>
        public List<UserWork> UserWorks { get; set; }
    }
}