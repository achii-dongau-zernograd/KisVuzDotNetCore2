using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.AspNetCore.Authorization;
using KisVuzDotNetCore2.Infrastructure;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    [Authorize(Roles = "Администраторы, НИЧ")]
    public class ScienceJournalsController : Controller
    {
        private readonly IScienceJournalRepository _scienceJournalRepository;
        private readonly ISelectListRepository _selectListRepository;

        public ScienceJournalsController(IScienceJournalRepository scienceJournalRepository, ISelectListRepository selectListRepository)
        {
            _scienceJournalRepository = scienceJournalRepository;
            _selectListRepository = selectListRepository;
        }

        // GET: ScienceJournals
        public async Task<IActionResult> Index()
        {
            var scienceJournal = _scienceJournalRepository.GetScienceJournals();
            return View(scienceJournal);
        }

        // GET: ScienceJournals/CreateOrEdit
        public IActionResult CreateOrEdit(int? id)
        {
            var scienceJournal = _scienceJournalRepository.GetScienceJournal(id);
            if (scienceJournal == null) return NotFound();
            ViewBag.CitationBases = _selectListRepository.GetSelectListCitatonBases();
            return View(scienceJournal);
        }

        // POST: ScienceJournals/CreateOrEdit
        [HttpPost]
        public IActionResult CreateOrEdit(ScienceJournal scienceJournal,
            int CitationBaseIdAdd, int CitationBaseIdRemove,
            CreateOrEditNirDataModeEnum mode)
        {
            ScienceJournal scienceJournalEntry = _scienceJournalRepository.GetScienceJournal(scienceJournal.ScienceJournalId);

            if (scienceJournalEntry == null) {
                _scienceJournalRepository.CreateScienceJournal(scienceJournal);
                scienceJournalEntry = scienceJournal;
            }

            else {
                scienceJournal.ScienceJournalCitationBases = scienceJournalEntry.ScienceJournalCitationBases;
                _scienceJournalRepository.UpdateScienceJournal(scienceJournalEntry, scienceJournal);
            }                     

            switch (mode) {
                case CreateOrEditNirDataModeEnum.Saving:
                    _scienceJournalRepository.UpdateScienceJournal(scienceJournalEntry, scienceJournal);
                    return RedirectToAction("Index");

                case CreateOrEditNirDataModeEnum.Canceling:
                    return RedirectToAction("Index");

                case CreateOrEditNirDataModeEnum.AddingCitationBase:
                    if (CitationBaseIdAdd != 0) {
                        var isExists = scienceJournal.ScienceJournalCitationBases.FirstOrDefault(s => s.CitationBaseId == CitationBaseIdAdd) != null;
                        if (!isExists)
                        {
                            scienceJournal.ScienceJournalCitationBases.Add(new ScienceJournalCitationBase {
                                CitationBaseId = CitationBaseIdAdd });
                            _scienceJournalRepository.UpdateScienceJournal(scienceJournalEntry, scienceJournal);
                        }
                    }
                    break;

                case CreateOrEditNirDataModeEnum.EditingCitationBase:
                    break;

                case CreateOrEditNirDataModeEnum.RemovingCitationBase:
                    if (CitationBaseIdRemove != 0) {
                        var citationBaseToRemove = scienceJournal.ScienceJournalCitationBases.FirstOrDefault(s => s.CitationBaseId == CitationBaseIdRemove);
                        if (citationBaseToRemove != null) {
                            scienceJournal.ScienceJournalCitationBases.Remove(citationBaseToRemove);
                            _scienceJournalRepository.UpdateScienceJournal(scienceJournalEntry, scienceJournal);
                        }
                    }
                    break;

                default:
                    break;
            }

            ViewBag.ScienseJournal = _scienceJournalRepository.GetScienceJournals();
            ViewBag.CitationBase = _scienceJournalRepository.GetCitationBases();

            return View(scienceJournalEntry);
        }
        
        // GET: ScienceJournals/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var scienceJournalEntry = _scienceJournalRepository.GetScienceJournal(id);
            if (scienceJournalEntry == null) return NotFound();
            return View(scienceJournalEntry);
        }

        // POST: ScienceJournals/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(ScienceJournal scienceJournal)
        {
            if (scienceJournal == null) return NotFound();
            var scienceJournalEntry = _scienceJournalRepository.RemoveScienceJournal(scienceJournal.ScienceJournalId);

            return RedirectToAction("Index");
        }
    }
}
