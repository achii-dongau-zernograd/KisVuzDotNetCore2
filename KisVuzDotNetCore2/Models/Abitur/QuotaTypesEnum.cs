using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Типы квот приёма
    /// </summary>
    public enum QuotaTypesEnum
    {
        /// <summary>
        /// Места в пределах квоты приёма лиц, имеющих особое право
        /// </summary>
        OsoboePravo = 1,
        /// <summary>
        /// Места в пределах квоты целевого приёма
        /// </summary>
        CelevoyPriem = 2,
        /// <summary>
        /// Основные места в рамках контрольных цифр
        /// </summary>
        KontrolnieCifri = 3,
        /// <summary>
        /// Места по договорам об оказании платных образовательных услуг
        /// </summary>
        DogovorObOkazaniiPlatnihObrazovatUslug = 4
    }
}
