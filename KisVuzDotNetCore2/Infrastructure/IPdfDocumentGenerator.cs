using KisVuzDotNetCore2.Models.Abitur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Infrastructure
{
    /// <summary>
    /// Интерфейс генератора документов pdf
    /// </summary>
    public interface IPdfDocumentGenerator
    {
        /// <summary>
        /// Создаёт pdf-файл бланка регистрации абитуриента на вступительное испытание
        /// </summary>
        /// <param name="entranceTestRegistrationForm"></param>
        /// <returns></returns>
        Task GenerateEntranceTestRegistrationForm(EntranceTestRegistrationForm entranceTestRegistrationForm);
    }
}
