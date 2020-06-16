using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Перечисление статусов абитуриентов
    /// </summary>
    public enum AbiturientStatusEnum
    {
        /// <summary>
        /// Новый абитуриент
        /// </summary>
        NewAbiturient = 1,
        /// <summary>
        /// Подтверждённый абитуриент
        /// </summary>
        ConfirmedAbiturient = 2,
        /// <summary>
        /// К зачислению
        /// </summary>
        Zachislenie = 3,
        /// <summary>
        /// В обработке
        /// </summary>
        VObrabotke = 4,
        /// <summary>
        /// К экзаменам
        /// </summary>
        KEkzamenam = 5
    }
}
