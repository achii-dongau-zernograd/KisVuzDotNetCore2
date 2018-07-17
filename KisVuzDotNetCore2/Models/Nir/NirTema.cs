using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Тема НИР
    /// </summary>
    public class NirTema
    {
        /// <summary>
        /// Идентификатор темы НИР
        /// </summary>
        public int NirTemaId { get; set; }

        /// <summary>
        /// Тема НИР
        /// </summary>
        [Display(Name = "Тема НИР")]
        public string NirTemaName { get; set; }

        /// <summary>
        /// Профили подготовки, соответствующие теме НИР
        /// </summary>
        public List<NirTemaEduProfile> NirTemaEduProfileList { get; set; }        
    }
}
