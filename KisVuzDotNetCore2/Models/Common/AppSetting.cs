using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Модель "Параметр (настройка) приложения"
    /// </summary>
    public class AppSetting
    {
        public int AppSettingId { get; set; }

        /// <summary>
        /// Наименование параметра
        /// </summary>
        [Display(Name = "Наименование параметра")]
        public string AppSettingName { get; set; }

        // <summary>
        /// Значение параметра
        /// </summary>
        [Display(Name = "Значение параметра")]
        public int AppSettingValue { get; set; }
    }
}
