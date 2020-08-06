using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Priem
{
    /// <summary>
    /// Интерфейс репозитория протоколов вступительных испытаний
    /// </summary>
    public interface IEntranceTestsProtocolsRepository
    {
        IQueryable<EntranceTestsProtocol> GetEntranceTestsProtocols();

        /// <summary>
        /// Возвращает протокол вступительных испытаний
        /// </summary>
        /// <param name="entranceTestsProtocolId"></param>
        /// <returns></returns>
        Task<EntranceTestsProtocol> GetEntranceTestsProtocol(int entranceTestsProtocolId);

        /// <summary>
        /// Создаёт pdf-файл протокола и возвращает объект протокола вступительных испытаний
        /// </summary>
        /// <param name="entranceTestsProtocolId"></param>
        /// <returns></returns>
        Task<EntranceTestsProtocol> GeneratePdf(int entranceTestsProtocolId);
    }
}
