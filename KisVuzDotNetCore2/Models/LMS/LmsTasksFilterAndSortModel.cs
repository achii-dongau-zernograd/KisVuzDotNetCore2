using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Модель фильтрации и сортировки списка заданий СДО
    /// </summary>
    public class LmsTasksFilterAndSortModel
    {
        /// <summary>
        /// Строка, определяющая правило вильтрации по УИД задания
        /// 5 --> {5};
        /// 5,7,9 --> {5},{7},{9};
        /// 5-7 --> {5},{6},{7};
        /// </summary>
        public string FilterLmsTaskId { get; set; }

        /// <summary>
        /// УИД автора задания
        /// </summary>
        public string FilterAppUserId { get; set; }

        /// <summary>
        /// УИД дисциплины
        /// </summary>
        public int FilterDisciplineNameId { get; set; }

        /// <summary>
        /// УИД типа задания
        /// </summary>
        public int FilterLmsTaskTypeId { get; set; }

        /// <summary>
        /// Флаг, указывающий на необходимость немедленной загрузки данных
        /// (из запроса GET)
        /// </summary>
        public bool IsRequestDataImmediately { get; set; }
    }
}
