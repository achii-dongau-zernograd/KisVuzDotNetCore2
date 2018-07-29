using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Students
{
    /// <summary>
    /// Перечисление наименований отметок студентов в ведомостях
    /// </summary>
    public enum VedomostStudentMarkNameEnum
    {
        /// <summary>
        /// Отлично
        /// </summary>
        Otl=1,
        /// <summary>
        /// Хорошо
        /// </summary>
        Hor=2,
        /// <summary>
        /// Удовлетворительно
        /// </summary>
        Udovl=3,
        /// <summary>
        /// Неудовлетворительно
        /// </summary>
        Neudovl=4,
        /// <summary>
        /// Не явился, не аттестован
        /// </summary>
        NeYavNeAtt=5,
        /// <summary>
        /// Зачтено
        /// </summary>
        Zachteno=6,
        /// <summary>
        /// Не зачтено
        /// </summary>
        NeZachteno=7
    }
}
