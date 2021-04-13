using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sign
{
    public interface ISignRepository
    {
        /// <summary>
        /// Создаёт ЭЦП для всех неподписанных документов
        /// </summary>
        /// <returns></returns>
        Task<int> CreateSignForUnsignedDocuments();
    }
}
