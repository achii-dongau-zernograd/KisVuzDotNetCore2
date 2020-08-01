using KisVuzDotNetCore2.Models.Abitur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Priem
{
    /// <summary>
    /// Интерфейс репозитория бланков регистрации на вступительные испытания
    /// </summary>
    public interface IEntranceTestRegistrationFormRepository
    {
        /// <summary>
        /// Создаёт новый бланк регистрации абитуриента на вступительное испытание
        /// </summary>
        /// <param name="entranceTestRegistrationForm"></param>
        /// <returns></returns>
        Task CreateEntranceTestRegistrationFormAsync(EntranceTestRegistrationForm entranceTestRegistrationForm);

        /// <summary>
        /// Возвращает запрос на выборку всех бланков регистрации на вступительные испытания
        /// </summary>
        /// <returns></returns>
        IQueryable<EntranceTestRegistrationForm> GetEntranceTestRegistrationForms();

        /// <summary>
        /// Возвращает бланк регистрации абитуриента на вступительное испытание
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EntranceTestRegistrationForm> GetEntranceTestRegistrationFormAsync(int id);
        
        bool IsEntranceTestRegistrationFormExists(int id);

        /// <summary>
        /// Устанавливает путь к файлу pdf для бланка регистрации с указанным УИД
        /// </summary>
        /// <param name="entranceTestRegistrationFormId"></param>
        /// <param name="createdFileName"></param>
        /// <returns></returns>
        Task SetPathToPdfFile(int entranceTestRegistrationFormId, string createdFileName);
        
        /// <summary>
        /// Удаляет pdf-файл (при наличии)
        /// </summary>
        /// <param name="entranceTestRegistrationFormId"></param>
        Task RemovePdfFileAsync(int entranceTestRegistrationFormId);
    }
}
