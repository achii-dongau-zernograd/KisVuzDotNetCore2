using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Репозиторий внешних ресурсов пользователей
    /// </summary>
    public class UserAccountExternalsRepository : IUserAccountExternalsRepository
    {
        private readonly AppIdentityDBContext _context;

        public UserAccountExternalsRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создаёт внешний ресурс пользователя
        /// </summary>
        /// <param name="userAccountExternal"></param>
        /// <returns></returns>
        public async Task AddAsync(UserAccountExternal userAccountExternal)
        {
            _context.userAccountExternals.Add(userAccountExternal);
            await _context.SaveChangesAsync();
        }

        

        /// <summary>
        /// Возвращает запрос на выборку всех внешних ресурсов
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserAccountExternal> GetUserAccountExternals()
        {
            return _context.userAccountExternals
                .Include(uae => uae.AppUser)
                .Include(uae => uae.ExternalResource.ExternalResourceType);
        }

        /// <summary>
        /// Возвращает запрос на выборку всех внешних ресурсов указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<UserAccountExternal> GetUserAccountExternals(string userName)
        {
            return GetUserAccountExternals().Where(uae => uae.AppUser.UserName == userName);
        }

        /// <summary>
        /// Возвращает внешний ресурс пользователя по переданному УИД ресурса
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserAccountExternal GetUserAccountExternal(int id)
        {
            return GetUserAccountExternals().SingleOrDefault(a => a.UserAccountExternalId == id);
        }

        /// <summary>
        /// Обновляет внешний ресурс пользователя
        /// </summary>
        /// <param name="userAccountExternal"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UserAccountExternal userAccountExternal)
        {
            _context.userAccountExternals.Update(userAccountExternal);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет внешний ресурс пользователя
        /// </summary>
        /// <param name="userAccountExternalId"></param>
        /// <returns></returns>
        public async Task RemoveAsync(int userAccountExternalId)
        {
            _context.userAccountExternals.Remove(new UserAccountExternal { UserAccountExternalId = userAccountExternalId });
            await _context.SaveChangesAsync();
        }
    }
}
