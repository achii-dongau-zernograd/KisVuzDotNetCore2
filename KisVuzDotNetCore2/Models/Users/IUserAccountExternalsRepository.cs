using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Интерфейс "Внешние ресурсы пользователей"
    /// </summary>
    public interface IUserAccountExternalsRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку всех внешних ресурсов
        /// </summary>        
        /// <returns></returns>
        IQueryable<UserAccountExternal> GetUserAccountExternals();

        /// <summary>
        /// Возвращает внешний ресурс пользователя по переданному УИД ресурса
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserAccountExternal GetUserAccountExternal(int id);

        /// <summary>
        /// Возвращает запрос на выборку всех внешних ресурсов указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<UserAccountExternal> GetUserAccountExternals(string userName);
        
        /// <summary>
        /// Создаёт внешний ресурс пользователя
        /// </summary>
        /// <param name="userAccountExternal"></param>
        /// <returns></returns>
        Task AddAsync(UserAccountExternal userAccountExternal);

        /// <summary>
        /// Обновляет внешний ресурс пользователя
        /// </summary>
        /// <param name="userAccountExternal"></param>
        /// <returns></returns>
        Task UpdateAsync(UserAccountExternal userAccountExternal);

        /// <summary>
        /// Удаляет внешний ресурс пользователя
        /// </summary>
        /// <param name="userAccountExternalId"></param>
        /// <returns></returns>
        Task RemoveAsync(int userAccountExternalId);
    }
}
