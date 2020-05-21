using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Интерфейс льготы абитуриента при поступлении
    /// </summary>
    public interface IAdmissionPrivilegeRepository
    {
        /// <summary>
        /// Создание льготы абитуриента при приёме
        /// </summary>
        /// <param name="admissionPrivilege"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task CreateAdmissionPrivilegeAsync(AdmissionPrivilege admissionPrivilege, IFormFile uploadedFile);

        /// <summary>
        /// Возвращает объект льготы абитуриента при поступлении
        /// </summary>
        /// <param name="admissionPrivilegeId"></param>
        /// <returns></returns>
        Task<AdmissionPrivilege> GetAdmissionPrivilegeAsync(int admissionPrivilegeId);

        /// <summary>
        /// Возвращает запрос на выборку всех объектов льгот абитуриентов при поступлении
        /// </summary>
        /// <returns></returns>
        IQueryable<AdmissionPrivilege> GetAdmissionPrivileges();
        
        /// <summary>
        /// Удаляет льготу абитуриента при поступлении
        /// </summary>
        /// <param name="admissionPrivilege"></param>
        /// <returns></returns>
        Task RemoveAdmissionPrivilegeAsync(AdmissionPrivilege admissionPrivilege);

        /// <summary>
        /// Обновляет льготу абитуриента при поступлении
        /// </summary>
        /// <param name="admissionPrivilege"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateAdmissionPrivilegeAsync(AdmissionPrivilege admissionPrivilege, IFormFile uploadedFile);
    }
}
