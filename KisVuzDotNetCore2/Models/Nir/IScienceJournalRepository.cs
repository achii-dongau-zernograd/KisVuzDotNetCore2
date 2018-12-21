using System.Collections.Generic;

namespace KisVuzDotNetCore2.Models.Nir
{
    public interface IScienceJournalRepository
    {
        /// <summary>
        /// Возвращает список баз цитирования
        /// </summary>
        /// <returns></returns>
        List<CitationBase> GetCitationBases();

        /// <summary>
        /// Возвращает список научных журналов
        /// </summary>
        /// <returns></returns>
        List<ScienceJournal> GetScienceJournals();

        /// <summary>
        /// Возвращает научный журнал
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ScienceJournal GetScienceJournal(int? id);

        /// <summary>
        /// Добавляет новый журнал
        /// </summary>
        /// <param name="scienceJournal"></param>
        void CreateScienceJournal(ScienceJournal scienceJournal);

        /// <summary>
        /// Обновляет научный журнал
        /// </summary>
        /// <param name="scienceJournalEntry"></param>
        /// <param name="scienceJournal"></param>
        void UpdateScienceJournal(ScienceJournal scienceJournalEntry, ScienceJournal scienceJournal);

        /// <summary>
        /// Удаляет научный журнал
        /// </summary>
        /// <param name="scienceJournalId"></param>
        ScienceJournal RemoveScienceJournal(int scienceJournalId);
    }
}
