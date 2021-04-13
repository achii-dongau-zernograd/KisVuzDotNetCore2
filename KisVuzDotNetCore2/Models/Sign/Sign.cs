using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sign
{
    /// <summary>
    /// Электронная цифровая подпись (ЭЦП)
    /// </summary>
    public class Sign
    {
        /// <summary>
        /// УИД ЭЦП
        /// </summary>
        public int SignId { get; set; }

        /// <summary>
        /// Дата и время подписания
        /// </summary>
        public DateTime SignDateTime { get; set; } = new DateTime(2020, 12, 31, 17, 00, 00);
                

        /// <summary>
        /// Фамилия лица, подписавшего документ
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Фамилия лица, подписавшего документ
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия лица, подписавшего документ
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Должность лица, подписавшего документ
        /// </summary>
        [Display(Name = "Должность лица, подписавшего документ")]
        public string Post { get; set; } = "Директор";

        /// <summary>
        /// Сформированный уникальный программный ключ
        /// </summary>
        [Display(Name = "Сформированный уникальный программный ключ")]
        public string Key { get; set; }


        /// <summary>
        /// Файл на сервере
        /// </summary>
        [Display(Name = "Файл на сервере")]
        public int? FileModelId { get; set; }
        public FileModel FileModel { get; set; }
    }
}
