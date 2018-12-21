using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Репозиторий Научный журнал, реализующий интерфейс
    /// </summary>
    public class ScienceJournalRepository: IScienceJournalRepository
    {
        AppIdentityDBContext _context;

        /// <summary>
        /// Внедрение зависимостей
        /// </summary>
        public ScienceJournalRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает список баз цитирования
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<CitationBase> GetCitationBases()
        {
            List<CitationBase> citationBases = _context.CitationBases.ToList();
            return citationBases;
        }

        /// <summary>
        /// Возвращает научный журнал
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ScienceJournal GetScienceJournal(int? id)
        {
            ScienceJournal scienceJournal = new ScienceJournal();

            if (id == null) return scienceJournal;

            List<ScienceJournal> NirScienceJournal = GetScienceJournals();
            scienceJournal = NirScienceJournal.SingleOrDefault(s=>s.ScienceJournalId==id);
            return scienceJournal;
        }

        /// <summary>
        /// Обновляет научный журнал
        /// </summary>
        /// <param name="scienceJournalEntry"></param>
        /// <param name="scienceJournal"></param>        
        public void UpdateScienceJournal(ScienceJournal scienceJournalEntry, ScienceJournal scienceJournal)
        {
            scienceJournalEntry.ScienceJournalName = scienceJournal.ScienceJournalName;

            if (scienceJournal.ScienceJournalCitationBases != null && scienceJournal.ScienceJournalCitationBases.Count > 0)
            {
                foreach (var scienceJournalCitationBases in scienceJournal.ScienceJournalCitationBases)
                {
                    bool isExists = false;
                    foreach (var scienceJournalCitationBasesEntry in scienceJournalEntry.ScienceJournalCitationBases)
                    {
                        if (scienceJournalCitationBasesEntry.CitationBaseId == scienceJournalCitationBases.CitationBaseId)
                        {
                            isExists = true;
                        }
                    }
                    if (!isExists)
                    {
                        scienceJournalEntry.ScienceJournalCitationBases.Add(scienceJournalCitationBases);
                    }
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Добавляет научный журнал
        /// </summary>
        /// <param name="scienceJournal"></param>
        public void CreateScienceJournal(ScienceJournal scienceJournal)
        {
            if (scienceJournal.ScienceJournalId != 0) return;

            _context.ScienceJournals.Add(scienceJournal);

            _context.SaveChanges();            
        }

        /// <summary>
        /// Удаляет научный журнал
        /// </summary>
        /// <param name="scienceJournalId"></param>
        /// <returns></returns>
        public ScienceJournal RemoveScienceJournal(int scienceJournalId)
        {
            var scienceJournal = GetScienceJournal(scienceJournalId);
            if (scienceJournal == null) return null;

            _context.ScienceJournals.Remove(scienceJournal);

            _context.SaveChanges();

            return scienceJournal;
        }

        /// <summary>
        /// Возвращает список научных журналов
        /// </summary>
        /// <returns></returns>
        public List<ScienceJournal> GetScienceJournals()
        {
            var scienceJournals = _context.ScienceJournals
                .Include(m => m.ScienceJournalCitationBases)
                    .ThenInclude(a=>a.CitationBase)
                .ToList();

            return scienceJournals;
        }
        
    }
}
