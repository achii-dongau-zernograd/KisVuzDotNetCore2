using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Перечисление типов внешних ресурсов
    /// </summary>
    public enum ExternalResourceTypeEnum
    {
        /// <summary>
        /// Социальные сети
        /// </summary>
        SocialNetworks = 1,
        /// <summary>
        /// Социальные сети
        /// </summary>
        ScienceNetworks = 2,
        /// <summary>
        /// Мессенджеры
        /// </summary>
        Messengers = 3,
        /// <summary>
        /// Базы цитирования
        /// </summary>
        CitationBases = 4
    }
}
