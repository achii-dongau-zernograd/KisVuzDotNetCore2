using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Перечисление типов договоров
    /// </summary>
    public enum ContractTypesEnum
    {
        /// <summary>
        /// Договор о целевом обучении
        /// </summary>
        ContractTargetTraining = 1,
        /// <summary>
        /// Договор об оказании платных образовательных услуг
        /// </summary>
        ContractPaidTraining = 2
    }
}
