using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Интерфейс методкомиссий
    /// </summary>
    public interface IMetodKomissiyaRepository
    {
        /// <summary>
        /// Определяет, является ли аутентифицированный
        /// пользователь членом методкомиссии
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsMetodKomissiyaMember(string userName);

        /// <summary>
        /// Возвращает набор образовательных программ,
        /// к которым имеет доступ пользователь
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IEnumerable<EduProgram> GetEduProgramsByUserName(string userName);
    }
}
