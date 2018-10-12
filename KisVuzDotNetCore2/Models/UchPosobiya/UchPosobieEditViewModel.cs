using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    /// <summary>
    /// Модель представления "Создание / Редактирование
    /// учебного пособия
    /// </summary>
    public class UchPosobieEditViewModel
    {
        public UchPosobie UchPosobie { get; set; }        
        public IFormFile FormFile { get; set; }
    }
}
