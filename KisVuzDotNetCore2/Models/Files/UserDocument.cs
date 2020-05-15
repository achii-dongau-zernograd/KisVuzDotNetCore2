using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Files
{
    /// <summary>
    /// Документ пользователя
    /// </summary>
    public class UserDocument
    {
        /// <summary>
        /// УИД документа пользователя
        /// </summary>
        public int UserDocumentId { get; set; }

        /// <summary>
        /// УИД пользователя
        /// </summary>
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Файл пользователя
        /// </summary>
        [Display(Name = "Файл")]
        public int FileModelId { get; set; }
        public FileModel FileModel { get; set; }

        /// <summary>
        /// Тип документа пользователя
        /// </summary>
        [Display(Name = "Тип документа")]
        public int FileDataTypeId { get; set; }
        public FileDataType FileDataType { get; set; }

        /// <summary>
        /// Статус записи
        /// </summary>
        [Display(Name = "Статус")]
        public int? RowStatusId { get; set; }
        public RowStatus RowStatus { get; set; }

        /// <summary>
        /// Замечание
        /// </summary>
        [Display(Name = "Замечание")]
        public string Remark { get; set; }

        ///////////////////////////////////////////////////////////
        
        /// <summary>
        /// Сведения об образовании
        /// </summary>
        [Display(Name = "Сведения об образовании")]
        public List<UserEducation> UserEducations { get; set; }

        /// <summary>
        /// Паспортные данные
        /// </summary>
        [Display(Name = "Паспортные данные")]
        public PassportData PassportData { get; set; }

        ////////////////////////////////////////////////////////////
        public RowStatusEnum GetRowStatusEnum
        {
            get
            {
                if (RowStatus == null)
                    return RowStatusEnum.NotConfirmed;

                if(RowStatus.RowStatusId == (int)RowStatusEnum.Confirmed)
                    return RowStatusEnum.Confirmed;

                if (RowStatus.RowStatusId == (int)RowStatusEnum.ReturnedForCorrection)
                    return RowStatusEnum.ReturnedForCorrection;

                return RowStatusEnum.NotConfirmed;
            }
        }
    }
}
