using KisVuzDotNetCore2.Models.Priem;
using Microsoft.AspNetCore.Http;
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
        /// Возвращает отфильтрованный запрос на выборку всех заявлений о приёме
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <returns></returns>
        IQueryable<ApplicationForAdmission> GetApplicationForAdmissions(ApplicationForAdmissionsFilterAndSortModel filterAndSortModel);

        /// <summary>
        /// Возвращает количество заявлений о приёме, созданных указанным пользователем
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        int GetNumberOfApplicationForAdmissions(string userName);
        
        /// <summary>
        /// Возвращает заявление о зачислении абитуриента
        /// </summary>
        /// <param name="applicationForAdmissionId"></param>
        /// <returns></returns>
        Task<ApplicationForAdmission> GetApplicationForAdmissionAsync(int applicationForAdmissionId);

        /// <summary>
        /// Обновляет заявление о зачислении
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <returns></returns>
        Task UpdateApplicationForAdmissionAsync(ApplicationForAdmission applicationForAdmission);

        /// <summary>
        /// Загрузка скан-копии заявления о приёме
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="applicationForAdmissionId"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task ApplicationForAdmissionFileLoadAsync(string userName, int applicationForAdmissionId, IFormFile uploadedFile);
        
        /// <summary>
        /// Создаёт заявление о приёме
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task CreateApplicationForAdmissionAsync(ApplicationForAdmission applicationForAdmission, IFormFile uploadedFile);

        /// <summary>
        /// Обновляет заявление о приёме
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateApplicationForAdmissionAsync(ApplicationForAdmission applicationForAdmission,
                                                IFormFile uploadedFile);

        /// <summary>
        /// Удаляет заявления о приёме
        /// </summary>
        /// <param name="applicationForAdmissions"></param>
        /// <returns></returns>
        Task RemoveApplicationForAdmissionsAsync(List<ApplicationForAdmission> applicationForAdmissions);
        
        /// <summary>
        /// Удаляет заявление о приёме
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        Task RemoveApplicationForAdmissionAsync(ApplicationForAdmission entry);
    }
}
