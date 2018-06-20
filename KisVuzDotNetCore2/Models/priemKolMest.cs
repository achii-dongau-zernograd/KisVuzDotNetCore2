using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{     /// <summary>
      /// Информация о количестве мест для приема на обучение по различным условиям поступления
      /// </summary>
    public class priemKolMest
    {

        public int priemKolMestId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование ")]
        public EduNapravl EduNapravl { get; set; }
        [Display(Name = "Наименование ")]
        public int EduNapravlId { get; set; }
       

        /// <summary>
        /// Особая квота очное
        /// </summary>
        [Display(Name = " Особая квота очное ")]
        public int priemKolMestQuota_och { get; set; }

        /// <summary>
        /// Особая квота заочное
        /// </summary>
        [Display(Name = "Особая квота заочное ")]
        public int priemKolMestQuota_zaoch { get; set; }

        /// <summary>
        /// Особая квота очно-заочное
        /// </summary>
        [Display(Name = "Особая квота очно-заочное")]
        public int priemKolMestQuota_och_zaoch { get; set; }





        /// <summary>
        /// Общие условия очное
        /// </summary>
        [Display(Name = "Общие условия очное")]
        public int PriemKolMestCommon_och { get; set; }


        /// <summary>
        /// Общие условия заочное
        /// </summary>
        [Display(Name = "Общие условия заочное ")]
        public int PriemKolMestCommon_zaoch { get; set; }

        /// <summary>
        /// Общие условия  очно-заочное
        /// </summary>
        [Display(Name = "Общие условия  очно-заочное")]
        public int PriemKolMestCommon_och_zaoch { get; set; }




        /// <summary>
        /// По договорам об оказании платных образовательных услуг очное
        /// </summary>
        [Display(Name = "По договорам об оказании платных образовательных услуг очное ")]
        public int PriemKolMestPaid_och { get; set; }

        /// <summary>
        /// По договорам об оказании платных образовательных услуг заочное
        /// </summary>
        [Display(Name = " По договорам об оказании платных образовательных услуг заочное ")]
        public int PriemKolMestPaid_zaoch { get; set; }


        /// <summary>
        /// По договорам об оказании платных образовательных услуг очно-заочное
        /// </summary>
        [Display(Name = "По договорам об оказании платных образовательных услуг очно-заочное ")]
        public int PriemKolMestPaid_och_zaoch { get; set; }



        /// <summary>
        /// Всего 
        /// </summary>
        [Display(Name = "Всего ")]
        public int PriemKolMestAll { get {
                return (PriemKolMestCommon_och + PriemKolMestCommon_zaoch + PriemKolMestCommon_och_zaoch + PriemKolMestCommon_och+ PriemKolMestCommon_zaoch+ PriemKolMestCommon_och_zaoch+ PriemKolMestPaid_och+ PriemKolMestPaid_zaoch+ PriemKolMestPaid_och_zaoch);
            } }
        
        

    }
   
}

