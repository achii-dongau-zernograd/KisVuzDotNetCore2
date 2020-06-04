using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Priem
{
    /// <summary>
    /// Модель фильтрации и сортировки абитуриентов
    /// </summary>
    public class AbiturientsFilterAndSortModel
    {
        /// <summary>
        /// Фрагмент фамилии абитуриента
        /// </summary>
        public string FilterLastNameFragment { get; set; }

        /// <summary>
        /// Статус абитуриента
        /// </summary>
        public int? FilterAbiturientStatus { get; set; }

        /// <summary>
        /// Флаг, указывающий на необходимость немедленной загрузки данных
        /// (из запроса GET)
        /// </summary>
        public bool IsRequestDataImmediately { get; set; }
    }
}
