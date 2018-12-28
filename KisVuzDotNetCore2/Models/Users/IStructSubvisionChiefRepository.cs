using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Интерфейс руководителя структурного подразделения
    /// </summary>
    public interface IStructSubvisionChiefRepository
    {
        /// <summary>
        /// Определяет, является ли пользователь руководителем структурного подразделения
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsStructSubvisionChief(string userName);
    }
}
