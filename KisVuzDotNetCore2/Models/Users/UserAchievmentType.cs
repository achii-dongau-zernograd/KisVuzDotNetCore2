using System.Collections.Generic;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Тип достижения пользователя"
    /// </summary>
    public class UserAchievmentType
    {
        /// <summary>
        /// УИД типа работы пользователя
        /// </summary>
        public int UserAchievmentTypeId { get; set; }

        /// <summary>
        /// Наименование типа работы пользователя
        /// </summary>
        public string UserAchievmentTypeName { get; set; }

        /// <summary>
        /// Достижения пользователей, входящие в данную группу
        /// </summary>
        public List<UserAchievment> UserAchievments { get; set; }
    }
}