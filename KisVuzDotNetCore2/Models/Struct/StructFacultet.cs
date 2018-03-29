using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Модель "Факультет"
    /// </summary>
    public class StructFacultet
    {
        /// <summary>
        /// УИД факультета
        /// </summary>
        public int StructFacultetId { get; set; }
                        
        /// <summary>
        /// Кафедры в составе факультета
        /// </summary>
        public List<StructKaf> StructKafs = new List<StructKaf>();

        ////////// Навигационные свойства ////////////////
        public int StructInstituteId { get; set; }
        public StructInstitute StructInstitute { get; set; }

        public int StructSubvisionId { get; set; }
        public StructSubvision StructSubvision { get; set; }
    }
}
