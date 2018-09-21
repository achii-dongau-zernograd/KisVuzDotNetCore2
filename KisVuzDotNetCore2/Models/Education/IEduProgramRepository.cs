using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Интерфейс репозитория образовательных программ
    /// </summary>
    public interface IEduProgramRepository
    {
        /// <summary>
        /// Определяет, является ли аутентифицированный
        /// пользователь лицом, ответственным за работу
        /// с образовательными программами
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsEduProgramUser(string userName);
    }
}
