using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Модель фильтрации и сортировки электронных журналов успеваемости студентов
    /// </summary>
    public class ElGradebooksFilterAndSortModel
    {
        /// <summary>
        /// Учебный год
        /// </summary>
        public string FilterEduYear { get; set; }

        /// <summary>
        /// Фрагмент наименования учебной дисциплины
        /// </summary>
        public string FilterDisciplineName { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public int FilterCourse { get; set; }

        /// <summary>
        /// Кафедра
        /// </summary>
        public string FilterDepartment { get; set; }

        /// <summary>
        /// Факультет
        /// </summary>
        public string FilterFaculty { get; set; }

        /// <summary>
        /// Наименование группы
        /// </summary>
        public string FilterGroupName { get; set; }

        /// <summary>
        /// Семестр
        /// </summary>
        public int FilterSemesterNumber { get; set; }

        /// <summary>
        /// Флаг, указывающий на необходимость немедленной загрузки данных
        /// (из запроса GET)
        /// </summary>
        public bool IsRequestDataImmediately { get; set; }
        
    }
}
