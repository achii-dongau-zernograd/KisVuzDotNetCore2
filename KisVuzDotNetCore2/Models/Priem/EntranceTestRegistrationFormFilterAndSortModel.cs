using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Priem
{
    /// <summary>
    /// Модель фильтрации и сортировки бланков регистрации абитуриентов на вступительные испытания 
    /// </summary>
    public class EntranceTestRegistrationFormFilterAndSortModel
    {
        /// <summary>
        /// Фрагмент фамилии абитуриента
        /// </summary>
        public string FilterLastNameFragment { get; set; }

        
        /// <summary>
        /// Номер группы абитуриентов к вступительным испытаниям
        /// </summary>
        public int? FilterEntranceTestGroupId { get; set; }

        /// <summary>
        /// Наименование дисциплины
        /// </summary>
        public string FilterDisciplineNameFragment { get; set; }

        /// <summary>
        /// Дата проведения
        /// </summary>
        public DateTime? FilterDate { get; set; }

        /// <summary>
        /// Флаг, указывающий на необходимость немедленной загрузки данных
        /// (из запроса GET)
        /// </summary>
        public bool IsRequestDataImmediately { get; set; }
        
    }
}
