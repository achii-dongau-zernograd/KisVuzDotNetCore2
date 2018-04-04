using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Численность обучающихся"
    /// </summary>
    public class EduChislen
    {
        public int EduChislenId { get; set; }

        public int NumberBFpriem { get; set; }
        public int NumberBRpriem { get; set; }
        public int NumberBMpriem { get; set; }
        public int NumberPpriem { get; set; }

        public EduForm EduForm { get; set; }
        public int EduFormId { get; set; }

        public EduProfile EduProfile {get;set;}
        public int EduProfileId { get; set; }
    }
}
