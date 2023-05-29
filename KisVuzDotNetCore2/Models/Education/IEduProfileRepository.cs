using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Интерфейс репозитория профилей подготовки
    /// </summary>
    public interface IEduProfileRepository
    {        
        /// <summary>
        /// Возвращает профиль подготовки по переданному УИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EduProfile> GetEduProfileAsync(int? id);
    }
}
