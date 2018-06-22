using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    public class ElectronCatalog
    {
        public int ElectronCatalogId { get; set; }
        [Display (Name = "Название БД электронного каталога")]
        public string NameEc { get; set; }
        [Display (Name = "Описание БД электронного каталога")]
        public string DescriptionEc { get; set; }
    }
}
