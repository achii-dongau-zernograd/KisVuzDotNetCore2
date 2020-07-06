using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Priem
{
    /// <summary>
    /// Модель фильтрации и сортировки заявлений о приёме
    /// </summary>
    public class ApplicationForAdmissionsFilterAndSortModel
    {        
        #region Фильтрация
        /// <summary>
        /// Фрагмент фамилии абитуриента
        /// </summary>
        public string FilterLastNameFragment { get; set; }

        /// <summary>
        /// Тип документа об образовании
        /// </summary>
        public int EducationDocumentFileDataTypeId { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        public int EduFormId { get; set; }

        /// <summary>
        /// Профиль подготовки
        /// </summary>
        public int EduProfileId { get; set; }

        /// <summary>
        /// Тип квоты на выделение мест для обучения
        /// </summary>
        public int QuotaTypeId { get; set; }

        /// <summary>
        /// Приоритет
        /// </summary>
        public int PriorityId { get; set; }

        /// <summary>
        /// Статус записи
        /// </summary>
        public int RowStatusId { get; set; }

        /// <summary>
        /// Флаг, указывающий на необходимость немедленной загрузки данных
        /// (из запроса GET)
        /// </summary>
        public bool IsRequestDataImmediately { get; set; }
        #endregion                
    }
}
