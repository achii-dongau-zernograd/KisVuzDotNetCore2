using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Nir;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Интерфейс профиля пользователя
    /// </summary>
    public interface IUserProfileRepository
    {
        /// <summary>
        /// Возвращает список статей пользователя userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<Article> GetArticles(string userName);
    }
}
