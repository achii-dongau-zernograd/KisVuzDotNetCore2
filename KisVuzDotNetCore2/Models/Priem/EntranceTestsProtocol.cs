using KisVuzDotNetCore2.Models.Abitur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Priem
{
    /// <summary>
    /// Протокол вступительных испытаний
    /// </summary>
    public class EntranceTestsProtocol
    {
        public int EntranceTestsProtocolId { get; set; }

        /// <summary>
        /// Абитуриент
        /// </summary>
        [Display(Name = "Абитуриент")]
        public Abiturient Abiturient { get; set; }
        public int AbiturientId { get; set; }


        /// <summary>
        /// Дата выдачи
        /// </summary>
        [Display(Name = "Дата выдачи")]
        [DataType(DataType.Date)]
        public DateTime DataVidachi { get; set; }

        /// <summary>
        /// pdf-файл протокола
        /// </summary>
        [Display(Name = "pdf-файл протокола")]
        public string FileName { get; set; }
    }
}
