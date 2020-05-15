using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Репозиторий сведений об образовании пользователя
    /// </summary>
    public class UserEducationRepository : IUserEducationRepository
    {
        private readonly AppIdentityDBContext _context;

        public UserEducationRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет сведения об образовании пользователя
        /// </summary>
        /// <param name="userEducation"></param>
        /// <returns></returns>
        public async Task AddUserEducationAsync(UserEducation userEducation)
        {
            _context.UserEducations.Add(userEducation);
            await _context.SaveChangesAsync();
        }
    }
}
