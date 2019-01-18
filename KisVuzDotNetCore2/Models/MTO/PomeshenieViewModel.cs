using KisVuzDotNetCore2.Models.Sveden;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.MTO
{
    /// <summary>
    /// Модель представления помещения
    /// </summary>
    public class PomeshenieViewModel
    {
        /// <summary>
        /// УИД помещения
        /// </summary>
        public int PomeshenieId { get; set; }

        /// <summary>
        /// Список помещений, доступных для руководителя структурного подразделения
        /// </summary>
        public SelectList PomeshenieSelectList { get; set; }

        /// <summary>
        /// Помещение
        /// </summary>
        public Pomeshenie Pomeshenie { get; set; }

        /// <summary>
        /// Редактируемый объект оборудования
        /// </summary>
        public Oborudovanie EditingOborudovanie { get; set; }
    }
}
