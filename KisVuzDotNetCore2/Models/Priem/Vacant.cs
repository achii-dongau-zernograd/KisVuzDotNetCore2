using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Priem
{
    /// <summary>
    /// Модель "Количество вакантных мест"
    /// </summary>
    public class Vacant
    {
        public int VacantId { get; set; }
        public int NumberBFVacant { get; set; }
        public int NumberBRVacant { get; set; }
        public int NumberBMVacant { get; set; }
        public int NumberPVacant { get; set; }

        public EduForm EduForm { get; set; }
        public int EduFormId { get; set; }

        public EduKurs EduKurs { get; set; }
        public int EduKursId { get; set; }

        public EduNapravl EduNapravl {get;set;}
        public int EduNapravlId { get; set; }
    }
}
