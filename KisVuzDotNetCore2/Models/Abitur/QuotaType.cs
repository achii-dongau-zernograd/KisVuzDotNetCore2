using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Тип квоты на выделение мест для обучения
    /// (в пределах квоты приема лиц, имеющих особое право,
    /// в пределах квоты целевого приема, основные в рамках контрольных цифр,
    /// по договорам об оказании платных образовательных услуг)
    /// </summary>
    public class QuotaType
    {
        public int QuotaTypeId { get; set; }

        public string QuotaTypeName { get; set; }
    }
}
