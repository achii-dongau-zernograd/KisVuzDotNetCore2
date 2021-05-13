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
        /// Способ подачи документов
        /// </summary>
        public int? FilterSubmittingDocumentsType { get; set; }

        /// <summary>
        /// Номер группы абитуриентов к вступительным испытаниям
        /// </summary>
        public int? FilterEntranceTestGroupId { get; set; }

        /// <summary>
        /// Флаг, указывающий предоставил ли абитуриент оригинал документа об образовании
        /// </summary>
        public bool? FilterIsEduDocumentOriginal { get; set; }

        /// <summary>
        /// Дата регистрации (отображаем, только если абитуриент зарегистрировался позднее этой даты)
        /// </summary>
        public DateTime? FilterRegisteredFromDate { get; set; } = new DateTime(2021, 02, 01);

        /// <summary>
        /// Флаг, указывающий на необходимость немедленной загрузки данных
        /// (из запроса GET)
        /// </summary>
        public bool IsRequestDataImmediately { get; set; }
        
    }
}
