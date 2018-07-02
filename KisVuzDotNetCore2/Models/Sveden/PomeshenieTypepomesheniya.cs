using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Таблица для связи PomeshenieType с Pomeshenie
    /// </summary>
    public class PomeshenieTypepomesheniya
    {
        public int PomeshenieTypepomesheniyaId { get; set; }

        public int PomeshenieId { get; set; }
        public Pomeshenie Pomeshenie { get; set; }
        
        public int PomeshenieTypeId { get; set; }
        public PomeshenieType PomeshenieType { get; set; }
    }
}
