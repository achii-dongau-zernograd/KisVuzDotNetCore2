using KisVuzDotNetCore2.Models.Nir;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Admin
{
    /// <summary>
    /// Контроллер очистки базы данных
    /// </summary>
    public class DatabaseClearingController : Controller
    {
        private readonly IArticlesRepository _articlesRepository;

        public DatabaseClearingController(IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Удаление научных статей из базы данных, опубликованных до указанного года включительно
        /// </summary>
        /// <returns></returns>
        public IActionResult RemoveArticles(int year)
        {
            var articlesToDelete = new List<Article>();
            if (year != 0)
            {
                articlesToDelete = _articlesRepository.GetArticles(year).ToList();
            }

            ViewBag.Year = year;

            return View(articlesToDelete);
        }


        public IActionResult RemoveArticlesConfirmation(int year)
        {
            var articlesToDelete = new List<Article>();
            if (year != 0)
            {
                articlesToDelete = _articlesRepository.GetArticles(year).ToList();
            }

            ViewBag.Year = year;
            ViewBag.ArticlesNum = articlesToDelete.Count();

            return View(articlesToDelete);
        }

        public async Task<IActionResult> RemoveArticlesConfirmed(int year)
        {
            var articlesToDelete = new List<Article>();
            if (year != 0)
            {
                articlesToDelete = _articlesRepository.GetArticles(year).ToList();
                await _articlesRepository.RemoveArticlesAsync(articlesToDelete);
            }                       

            return RedirectToAction(nameof(RemoveArticles),new { year });
        }
    }
}
