using KisVuzDotNetCore2.Models.Abitur;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Priem
{
    /// <summary>
    /// Репозиторий бланков регистрации на вступительные испытания
    /// </summary>
    public class EntranceTestRegistrationFormRepository : IEntranceTestRegistrationFormRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public EntranceTestRegistrationFormRepository(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        /// <summary>
        /// Создаёт новый бланк регистрации абитуриента на вступительное испытание
        /// </summary>
        /// <param name="entranceTestRegistrationForm"></param>
        /// <returns></returns>
        public async Task CreateEntranceTestRegistrationFormAsync(EntranceTestRegistrationForm entranceTestRegistrationForm)
        {
            _context.EntranceTestRegistrationForms.Add(entranceTestRegistrationForm);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает бланк регистрации абитуриента на вступительное испытание
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EntranceTestRegistrationForm> GetEntranceTestRegistrationFormAsync(int id)
        {
            var entry = await GetEntranceTestRegistrationForms()
                .SingleOrDefaultAsync(m => m.EntranceTestRegistrationFormId == id);

            return entry;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех бланков регистрации на вступительные испытания
        /// </summary>
        /// <returns></returns>
        public IQueryable<EntranceTestRegistrationForm> GetEntranceTestRegistrationForms()
        {
            var query = _context.EntranceTestRegistrationForms
                .Include(rf => rf.Abiturient.AppUser)
                .Include(rf => rf.LmsEvent);

            return query;
        }

        public bool IsEntranceTestRegistrationFormExists(int id)
        {
            return _context.EntranceTestRegistrationForms.Any(e => e.EntranceTestRegistrationFormId == id);
        }

        /// <summary>
        /// Удаляет pdf-файл (при наличии)
        /// </summary>
        /// <param name="entranceTestRegistrationFormId"></param>
        public async Task RemovePdfFileAsync(int entranceTestRegistrationFormId)
        {
            var entry = await GetEntranceTestRegistrationFormAsync(entranceTestRegistrationFormId);

            if (entry == null)
                return;

            if (string.IsNullOrWhiteSpace(entry.FileName))
                return;

            string[] paths = { _appEnvironment.WebRootPath, entry.FileName };

            string pathToFile = Path.Combine(paths[0], paths[1]);
            if (File.Exists(pathToFile))
                File.Delete(pathToFile);

            entry.FileName = "";
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Удаляет pdf-файл бланка ответов (при наличии)
        /// </summary>
        /// <param name="entranceTestRegistrationFormId"></param>
        /// <returns></returns>
        public async Task RemovePdfFileBlankOtvetovAsync(int entranceTestRegistrationFormId)
        {
            var entry = await GetEntranceTestRegistrationFormAsync(entranceTestRegistrationFormId);

            if (entry == null)
                return;

            if (string.IsNullOrWhiteSpace(entry.FileNameBlankOtvetov))
                return;

            string[] paths = { _appEnvironment.WebRootPath, entry.FileNameBlankOtvetov };

            string pathToFile = Path.Combine(paths[0], paths[1]);
            if (File.Exists(pathToFile))
                File.Delete(pathToFile);

            entry.FileNameBlankOtvetov = "";
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Устанавливает путь к файлу pdf для бланка регистрации с указанным УИД
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createdFileName"></param>
        /// <returns></returns>
        public async Task SetPathToPdfFile(int id, string createdFileName)
        {
            var entry = await GetEntranceTestRegistrationFormAsync(id);
            if (entry == null)
                return;

            if (entry.FileName != createdFileName)
            {
                entry.FileName = createdFileName;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Устанавливает путь к pdf-файлу бланка ответов на экзаменационное задание
        /// </summary>
        /// <param name="entranceTestRegistrationFormId"></param>
        /// <param name="createdFileNameBlankOtvetov"></param>
        /// <returns></returns>
        public async Task SetPathToPdfFileBlankOtvetov(int entranceTestRegistrationFormId, string createdFileNameBlankOtvetov)
        {
            var entry = await GetEntranceTestRegistrationFormAsync(entranceTestRegistrationFormId);
            if (entry == null)
                return;

            if (entry.FileNameBlankOtvetov != createdFileNameBlankOtvetov)
            {
                entry.FileNameBlankOtvetov = createdFileNameBlankOtvetov;
                await _context.SaveChangesAsync();
            }
        }
    }
}
