using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    /// <summary>
    /// Модель представления списка учебных пособий с фильтром
    /// </summary>
    public class UchPosobiyaViewModel
    {
        /// <summary>
        /// Перечисление уч. пособий
        /// </summary>
        public IEnumerable<UchPosobie> UchPosobiya { get; set; }

        /// <summary>
        /// Фильтр
        /// </summary>
        public UchPosobieFilterModel UchPosobieFilterModel { get; set; }
    }
}
