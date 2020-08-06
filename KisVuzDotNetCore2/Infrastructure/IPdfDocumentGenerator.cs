using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.LMS;
using KisVuzDotNetCore2.Models.Priem;
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
        /// и возвращает путь к созданному файлу
        /// </summary>
        /// <param name="entranceTestRegistrationForm"></param>
        /// <returns></returns>
        string GenerateEntranceTestRegistrationForm(EntranceTestRegistrationForm entranceTestRegistrationForm);

        /// <summary>
        /// Создаёт pdf-файл бланка ответов на эк
        /// и возвращает путь к созданному файлу
        /// </summary>
        /// <param name="entranceTestRegistrationForm"></param>
        /// <returns></returns>
        string GenerateEntranceTestBlankOtvetov(EntranceTestRegistrationForm entranceTestRegistrationForm, List<LmsTask> lmsEventTasks);

        /// <summary>
        /// Создаёт pdf-файл протокола вступительных испытаний
        /// и возвращает путь к созданному файлу
        /// </summary>
        /// <param name="entranceTestsProtocol"></param>
        /// <returns></returns>
        string GenerateEntranceTestsProtocol(EntranceTestsProtocol entranceTestsProtocol, string path);
    }
}
