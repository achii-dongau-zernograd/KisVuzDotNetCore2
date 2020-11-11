using KisVuzDotNetCore2.Models.Files;
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
        private readonly IPatentRepository _patentRepository;
        private readonly IFileModelRepository _fileModelRepository;

        public DatabaseClearingController(IArticlesRepository articlesRepository,
            IPatentRepository patentRepository,
            IFileModelRepository fileModelRepository)
        {
            _articlesRepository = articlesRepository;
            _patentRepository = patentRepository;
            _fileModelRepository = fileModelRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Удаление научных статей
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
        #endregion

        #region Удаление патентов и свидетельств
        /// <summary>
        /// Удаление патентов и свидетельств из базы данных, опубликованных до указанного года включительно
        /// </summary>
        /// <returns></returns>
        public IActionResult RemovePatents(int year)
        {
            var patentsToDelete = new List<Patent>();
            if (year != 0)
            {
                patentsToDelete = _patentRepository.GetPatents(year).ToList();
            }

            ViewBag.Year = year;

            return View(patentsToDelete);
        }


        public IActionResult RemovePatentsConfirmation(int year)
        {
            var patentsToDelete = new List<Patent>();
            if (year != 0)
            {
                patentsToDelete = _patentRepository.GetPatents(year).ToList();
            }

            ViewBag.Year = year;
            ViewBag.PatentsNum = patentsToDelete.Count();

            return View(patentsToDelete);
        }

        public async Task<IActionResult> RemovePatentsConfirmed(int year)
        {
            var patentsToDelete = new List<Patent>();
            if (year != 0)
            {
                patentsToDelete = _patentRepository.GetPatents(year).ToList();
                await _patentRepository.RemovePatentsAsync(patentsToDelete);
            }

            return RedirectToAction(nameof(RemovePatents), new { year });
        }
        #endregion

        #region Поиск и удаление файлов из папки files, не имеющих записей в таблице files
        public async Task<IActionResult> FindLostFiles(int numFilesToShow)
        {
            // Количество файлов в папке files
            int NumFilesInFileSystem = await _fileModelRepository.GetNumFilesInFileSystemAsync();
            // Количество записей в таблице files базы данных
            int NumFilesInDatabase = await _fileModelRepository.GetNumFilesInDatabase();

            
            ViewBag.NumFilesInFileSystem = NumFilesInFileSystem;
            ViewBag.NumFilesInDatabase = NumFilesInDatabase;

            return View();
        }
        #endregion
    }
}
