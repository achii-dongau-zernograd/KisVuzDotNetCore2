using KisVuzDotNetCore2.Models.Abitur;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public EntranceTestRegistrationFormRepository(AppIdentityDBContext context)
        {
            _context = context;
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
                .Include(rf => rf.Abiturient)
                .Include(rf => rf.LmsEvent);

            return query;
        }

        public bool IsEntranceTestRegistrationFormExists(int id)
        {
            return _context.EntranceTestRegistrationForms.Any(e => e.EntranceTestRegistrationFormId == id);
        }
    }
}
