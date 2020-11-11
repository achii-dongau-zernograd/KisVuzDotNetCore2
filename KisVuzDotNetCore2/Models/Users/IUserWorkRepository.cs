using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Интерфейс репозитория работ пользователей
    /// </summary>
    public interface IUserWorkRepository
    {
        /// <summary>
        /// Возвращает количество работ пользователей, размещённых до указанной даты
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        Task<int> GetNumUserWorksUploadedToDate(DateTime dateTime);

        /// <summary>
        /// Удаляет файлы работ пользователей, загруженные до указанной даты
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task RemoveUserWorksToDateAsync(DateTime date);
    }
}
