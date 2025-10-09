using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Nir;
using KisVuzDotNetCore2.Models.Students;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly AppIdentityDBContext _context;
        private readonly IArticlesRepository _articlesRepository;
        private readonly IPatentRepository _patentRepository;
        private readonly IFileModelRepository _fileModelRepository;
        private readonly IUserWorkRepository _userWorkRepository;
        private readonly IAbiturientRepository _abiturientRepository;
        private readonly IMessagesFromAppUserToStudentGroupsRepository _messagesFromAppUserToStudentGroupsRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public DatabaseClearingController(AppIdentityDBContext context,
            IArticlesRepository articlesRepository,
            IPatentRepository patentRepository,
            IFileModelRepository fileModelRepository,
            IUserWorkRepository userWorkRepository,
            IAbiturientRepository abiturientRepository,
            IMessagesFromAppUserToStudentGroupsRepository messagesFromAppUserToStudentGroupsRepository,
            IUserProfileRepository userProfileRepository)
        {
            _context = context;
            _articlesRepository = articlesRepository;
            _patentRepository = patentRepository;
            _fileModelRepository = fileModelRepository;
            _userWorkRepository = userWorkRepository;
            _abiturientRepository = abiturientRepository;
            _messagesFromAppUserToStudentGroupsRepository = messagesFromAppUserToStudentGroupsRepository;
            _userProfileRepository = userProfileRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Удаление абитуриентов, у которых отсутствует дата регистрации
        public IActionResult RemoveAbiturientsWithNullRegDate()
        {
            var dataToRemove = _abiturientRepository.GetAbiturients()
                .Where(a=>a.RegisterDateTime == null)
                .ToList();
            return View(dataToRemove);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAbiturientsWithNullRegDateConfirmed()
        {
            var dataToRemove = _abiturientRepository.GetAbiturients()
                .Where(a => a.RegisterDateTime == null)
                .ToList();

            foreach (var abiturient in dataToRemove)
            {
                try
                {
                    await _abiturientRepository.RemoveAbiturientAsync(abiturient.AppUser.UserName);
                }
                catch (Exception)
                {
                    Console.WriteLine("RemoveAbiturientAsync Exception");
                }
                
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Удаление абитуриентов, у которых нет ссылки на аккаунт
        public IActionResult RemoveAbiturientsWithNullAppUser()
        {
            var dataToRemove = _abiturientRepository.GetAbiturients()
                .Where(a => a.AppUser == null)
                .ToList();
            return View(dataToRemove);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAbiturientsWithNullAppUserConfirmed()
        {
            var dataToRemove = _abiturientRepository.GetAbiturients()
                .Where(a => a.AppUser == null)
                .ToList();

            foreach (var abiturient in dataToRemove)
            {
                try
                {
                    await _abiturientRepository.RemoveAbiturientAsync(abiturient.AbiturientId);
                }
                catch (Exception)
                {
                    Console.WriteLine("RemoveAbiturientAsync Exception");
                }

            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Удаление абитуриентов, у которых отсутствует дата регистрации
        public IActionResult RemoveAbiturients(int year=2021)
        {
            var dataToRemove = _abiturientRepository.GetAbiturients()
                /*.Where(a => a.RegisterDateTime.Value.Year==year && a.AbiturientStatusId != (int)AbiturientStatusEnum.AddedToStudGroup)*/
                .Take(50)
                .ToList();
            return View(dataToRemove);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAbiturientsConfirmed(int year = 2021)
        {
            var dataToRemove = _abiturientRepository.GetAbiturients()
                /*.Where(a => a.RegisterDateTime.Value.Year == year && a.AbiturientStatusId != (int)AbiturientStatusEnum.AddedToStudGroup)*/
                .Take(50)
                .ToList();

            foreach (var abiturient in dataToRemove)
            {
                try
                {
                    await _abiturientRepository.RemoveAbiturientAsync(abiturient.AppUser.UserName);
                }
                catch (Exception)
                {
                    Console.WriteLine("RemoveAbiturientAsync Exception");
                }

            }

            return RedirectToAction(nameof(Index));
        }
        #endregion


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

            DateTime date = new DateTime(2020, 08, 30);
            int NumUserWorksUploadedToDate = await _userWorkRepository.GetNumUserWorksUploadedToDate(date);



            ViewBag.NumFilesInFileSystem = NumFilesInFileSystem;
            ViewBag.NumFilesInDatabase = NumFilesInDatabase;
            ViewBag.NumUserWorksUploadedToDate = NumUserWorksUploadedToDate;
            ViewBag.Date = date;

            return View();
        }
        #endregion

        /// <summary>
        /// Удаляет файлы работ пользователей, загруженные до указанной даты
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveUserWorksUploadedFilesToDate(string date)
        {
            DateTime _date = DateTime.Parse(date);
            await _userWorkRepository.RemoveUserWorksToDateAsync(_date);

            return RedirectToAction(nameof(FindLostFiles));
        }

        /// <summary>
        /// Удаляет все сообщения пользователей и сообщения учебным группам до 31.08.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RemoveOldMessages()
        {
            await _messagesFromAppUserToStudentGroupsRepository.RemoveMessagesToDate(new DateTime(2024, 08, 31));

            var query = _context.UserMessages.Where(m => m.UserMessageDate <= new DateTime(2024, 08, 31));
            _context.UserMessages.RemoveRange(query);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Удаление пользователей, которым не назначены роли и которые не являются ни студентами, ни преподавателями
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RemoveUsers(int numUsersToDelete = 1, bool remove = false)
        {            
            List<string> usersNotStudentsAndNotTeachersAndNoRoles = _userProfileRepository.GetUsersNotStudentsAndNotTeachersAndNoRoles();

            if (!remove)
            {
                return View("ListOfUsersWithoutRoles", usersNotStudentsAndNotTeachersAndNoRoles);
                //return RedirectToAction(nameof(ListOfUsersWithoutRoles), new { usersNotStudentsAndNotTeachersAndNoRoles });
            }
                

            foreach (var userName in usersNotStudentsAndNotTeachersAndNoRoles)
            {
                await _userWorkRepository.RemoveUserWorksAsync(userName);
                await _userProfileRepository.RemoveAppUserAsync(userName);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ListOfUsersWithoutRoles(List<string> usersNotStudentsAndNotTeachersAndNoRoles)
        {

            return View(usersNotStudentsAndNotTeachersAndNoRoles);
        }


    }
}
