using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Перечисление групп типов событий системы дистанционного образования
    /// </summary>
    public enum LmsEventTypeGroupsEnum
    {
        /// <summary>
        /// События, организовываемые в процессе проведения
        /// приёмной кампании
        /// </summary>
        Priem = 1,
        /// <summary>
        /// События, организовываемые в процессе обучения
        /// </summary>
        StudyingProccess = 2,
        /// <summary>
        /// События, организовываемые в процессе текущей аттестации
        /// </summary>
        CurrentCertification = 3,
        /// <summary>
        /// События, организовываемые в процессе промежуточной аттестации
        /// </summary>
        IntermediateCertification = 4,
        /// <summary>
        /// События, организовываемые в процессе государственной итоговой аттестации (ГИА)
        /// </summary>
        StateFinalCertification = 5
    }
}
