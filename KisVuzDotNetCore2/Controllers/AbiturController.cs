using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Priem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KisVuzDotNetCore2.Controllers
{
    public class AbiturController : Controller
    {
        string searchTemplate = "";

        List<priemKolMest> priemKolMest = new List<priemKolMest>();
        List<PriemExam> PriemExam = new List<PriemExam>();
        List<priemKolTarget> priemKolTarget = new List<priemKolTarget>();

        private readonly AppIdentityDBContext _context;

        public AbiturController(AppIdentityDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по СПО
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SrProf()
        {
            searchTemplate = "среднее";            
            
            GetDataForTables(ref priemKolMest, ref PriemExam, ref priemKolTarget, searchTemplate);

            ViewData["priemKolMest"] = priemKolMest;
            ViewData["PriemExam"] = PriemExam;
            ViewData["priemKolTarget"] = priemKolTarget;

            return View();
        }

        private void GetDataForTables(ref List<priemKolMest> priemKolMest, ref List<PriemExam> priemExam, ref List<priemKolTarget> priemKolTarget, string searchTemplate)
        {
            priemKolMest =  _context.priemKolMest
                .Include(p => p.EduNapravl)
                    .ThenInclude(n => n.EduUgs)
                        .ThenInclude(ugs => ugs.EduLevel)
                .Where(p => p.EduNapravl.EduUgs.EduLevel.EduLevelName.Contains(searchTemplate))
                .ToList();

            priemExam = _context.PriemExam
                .Include(p => p.EduNapravl)
                    .ThenInclude(n => n.EduUgs)
                        .ThenInclude(ugs => ugs.EduLevel)
                .Where(p => p.EduNapravl.EduUgs.EduLevel.EduLevelName.Contains(searchTemplate))
                .ToList();

            priemKolTarget = _context.priemKolTarget
                .Include(p => p.EduNapravl)
                    .ThenInclude(n => n.EduUgs)
                        .ThenInclude(ugs => ugs.EduLevel)
                .Where(p => p.EduNapravl.EduUgs.EduLevel.EduLevelName.Contains(searchTemplate))
                .ToList();
        }

        /// <summary>
        /// Подраздел для представления информации по бакалавриату
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Bachelor()
        {
            searchTemplate = "бакалавриат";

            GetDataForTables(ref priemKolMest, ref PriemExam, ref priemKolTarget, searchTemplate);

            ViewData["priemKolMest"] = priemKolMest;
            ViewData["PriemExam"] = PriemExam;
            ViewData["priemKolTarget"] = priemKolTarget;

            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по специалитету
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Special()
        {
            searchTemplate = "специалитет";

            GetDataForTables(ref priemKolMest, ref PriemExam, ref priemKolTarget, searchTemplate);

            ViewData["priemKolMest"] = priemKolMest;
            ViewData["PriemExam"] = PriemExam;
            ViewData["priemKolTarget"] = priemKolTarget;

            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по магистратуре
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Magistr()
        {
            searchTemplate = "магистратура";

            GetDataForTables(ref priemKolMest, ref PriemExam, ref priemKolTarget, searchTemplate);

            ViewData["priemKolMest"] = priemKolMest;
            ViewData["PriemExam"] = PriemExam;
            ViewData["priemKolTarget"] = priemKolTarget;

            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по аспирантуре
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Postgraduate()
        {
            searchTemplate = "аспирантура";

            GetDataForTables(ref priemKolMest, ref PriemExam, ref priemKolTarget, searchTemplate);

            ViewData["priemKolMest"] = priemKolMest;
            ViewData["PriemExam"] = PriemExam;
            ViewData["priemKolTarget"] = priemKolTarget;

            return View();
        }
    }
}