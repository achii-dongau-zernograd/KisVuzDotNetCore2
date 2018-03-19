using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Структура и органы управления образовательной организацией
    /// </summary>
    public class StructOrgUprav
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public int StructOrgUpravId { get; set; }

        /// <summary>
        /// Флаг органа управления
        /// </summary>
        public bool IsOrgUprav { get; set; }

        /// <summary>
        /// Наименование органа управления или структурного подразделения
        /// </summary>
        public string StructOrgUpravName {get;set;}

        /// <summary>
        /// ФИО руководителя структурного подразделения
        /// </summary>
        public string Fio { get; set; }

        /// <summary>
        /// Должность руководителя структурного подразделения
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// Адрес местонахождения структурного подразделения
        /// </summary>
        public string AddressStr { get; set; }

        /// <summary>
        /// Адрес официального сайта структурного подразделения
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Адреса электронной почты структурного подразделения
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Ссылка на копию положения о структурном подразделении
        /// </summary>
        public string DivisionClauseDocLink { get; set; }
    }
}
