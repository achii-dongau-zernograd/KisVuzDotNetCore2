using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.MTO;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Интерфейс руководителя структурного подразделения
    /// </summary>
    public interface IStructSubvisionChiefRepository
    {
        /// <summary>
        /// Определяет, является ли пользователь руководителем структурного подразделения
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsStructSubvisionChief(string userName);

        /// <summary>
        /// Возвращает модель представления для работы с помещением с указанным УИД
        /// </summary>
        /// <param name="pomeshenieId">УИД помещения</param>
        /// <returns></returns>
        PomeshenieViewModel GetPomeshenieViewModel(int? pomeshenieId);
    }
}
