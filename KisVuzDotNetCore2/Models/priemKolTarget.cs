using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{


    /// <summary>
    /// Шаблон представления информации о колличестве мест  для приема на обучения по различным условиям поступления
    /// </summary>
    public class priemKolTarget
    {
        public int priemKolTargetId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование ")]
        public EduNapravl EduNapravl { get; set; }
        [Display(Name = "Наименование ")]
        public int EduNapravlId { get; set; }


       


       


        /// <summary>
        /// Места в приделах целевой квоты очное
        /// </summary>
        [Display(Name = " Особая квота очное ")]
        public int Mesta_v_ramkahtQuota_och { get; set; }

        /// <summary>
        /// Места в рамках контрольных цифр приема(по общему конкурсу) заочное
        /// </summary>
        [Display(Name = "Особая квота заочное ")]
        public int Mesta_v_ramkah_zaoch { get; set; }

        /// <summary>
        /// Места в рамках контрольных цифр приема(по общему конкурсу) очно-заочное
        /// </summary>
        [Display(Name = "Особая квота очно-заочное")]
        public int Mesta_v_ramkah_och_zaoch { get; set; }





        /// <summary>
        ///  Места в приделах особой квоты очное
        /// </summary>
        [Display(Name = "Общие условия очное")]
        public int Mesta_v_pridelah_osoboi_och { get; set; }


        /// <summary>
        ///  Места в приделах особой квоты заочное
        /// </summary>
        [Display(Name = "Общие условия заочное ")]
        public int Mesta_v_pridelah_osoboi_zaoch { get; set; }

        /// <summary>
        ///  Места в приделах особой квоты  очно-заочное
        /// </summary>
        [Display(Name = "Общие условия  очно-заочное")]
        public int Mesta_v_pridelah_osoboi_och_zaoch { get; set; }




        /// <summary>
        /// Места в приделах целевой квоты очное
        /// </summary>
        [Display(Name = "По договорам об оказании платных образовательных услуг очное ")]
        public int Mesta_v_pridelah_celevoi_och { get; set; }

        /// <summary>
        /// Места в приделах целевой квоты заочное
        /// </summary>
        [Display(Name = " По договорам об оказании платных образовательных услуг заочное ")]
        public int Mesta_v_pridelah_celevoi_zaoch { get; set; }


        /// <summary>
        /// Места в приделах целевой квоты очно-заочное
        /// </summary>
        [Display(Name = "По договорам об оказании платных образовательных услуг очно-заочное ")]
        public int Mesta_v_pridelah_celevoi_och_zaoch { get; set; }










        /// <summary>
        /// Всего 
        /// </summary>
        [Display(Name = "Всего ")]
        public int Vsego
        {
            get
            {
                return (Mesta_v_ramkahtQuota_och+ Mesta_v_ramkah_zaoch+ Mesta_v_ramkah_och_zaoch + Mesta_v_pridelah_osoboi_och+ Mesta_v_pridelah_osoboi_zaoch+ Mesta_v_pridelah_osoboi_och_zaoch+Mesta_v_pridelah_celevoi_och+ Mesta_v_pridelah_celevoi_zaoch+ Mesta_v_pridelah_celevoi_och_zaoch);
            }
        }
    }


}
