using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Интерфейс репозитория сведений об образовании пользователя
    /// </summary>
    public interface IUserEducationRepository
    {
        /// <summary>
        /// Добавляет сведения об образовании пользователя
        /// </summary>
        /// <param name="userEducation"></param>
        /// <returns></returns>
        Task AddUserEducationAsync(UserEducation userEducation);
    }
}
