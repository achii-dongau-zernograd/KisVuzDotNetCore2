using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Пользователь - консультант абитуриента
    /// </summary>
    public class AppUserAbiturientConsultant
    {
        public int AppUserAbiturientConsultantId { get; set; }

        /// <summary>
        /// Консультант
        /// </summary>
        [Display(Name = "Консультант")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Абитуриент
        /// </summary>
        [Display(Name = "Абитуриент")]
        public int AbiturientId { get; set; }
        public Abiturient Abiturient { get; set; }
    }
}
