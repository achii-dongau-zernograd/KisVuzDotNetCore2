using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Интерфейс хранилища заявлений о приёме
    /// </summary>
    public interface IApplicationForAdmissionRepository
    {
        /// <summary>
        /// Проверяет наличие заявлений о приёме для указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsApplicationForAdmissionExists(string userName);

        /// <summary>
        /// Возвращает запрос на выборку всех заявлений о приёме
        /// </summary>
        /// <returns></returns>
        IQueryable<ApplicationForAdmission> GetApplicationForAdmissions();

        /// <summary>
        /// Возвращает запрос на выборку всех заявлений о приёме указанного пользователя
        /// </summary>
        /// <returns></returns>
        IQueryable<ApplicationForAdmission> GetApplicationForAdmissions(string userName);

        /// <summary>
        /// Возвращает количество заявлений о приёме, созданных указанным пользователем
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        int GetNumberOfApplicationForAdmissions(string userName);
    }
}
