using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.AspNetCore.Authorization;
using KisVuzDotNetCore2.Models.Users;
using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models.Common;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    [Authorize(Roles = "Администраторы, НИЧ")]
    public class PatentsController : Controller
    {       
        private ISelectListRepository _selectListRepository;
        private IFileModelRepository _fileModelRepository;
        private IPatentRepository _patentRepository;


        public PatentsController(            
            ISelectListRepository selectListRepository,
            IFileModelRepository fileModelRepository,
            IPatentRepository patentRepository)
        {            
            _selectListRepository = selectListRepository;
            _fileModelRepository = fileModelRepository;
            _patentRepository = patentRepository;
        }

        /// <summary>
        /// Подтверждение патента (свидетельства)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmPatent(int patentId)
        {
            _patentRepository.ConfirmPatent(patentId);
            return RedirectToAction("Index");
        }

        // GET: Patents
        public IActionResult Index()
        {
            List<Patent> userPatents = _patentRepository.GetPatents();
            return View(userPatents);
        }               

        // GET: Patents/CreateOrEditPatent
        public IActionResult CreateOrEdit(int? id)
        {
            Patent patent = _patentRepository.GetPatent(id);
            if (patent == null) return NotFound();

            ViewBag.Authors = _selectListRepository.GetSelectListAuthors();
            ViewBag.Years = _selectListRepository.GetSelectListYears();
            ViewBag.PatentVids = _selectListRepository.GetSelectListPatentVids();
            ViewBag.NirSpecials = _selectListRepository.GetSelectListNirSpecials();
            ViewBag.NirTemas = _selectListRepository.GetSelectListNirTemas();

            return View(patent);
        }

        // POST: Patents/CreateOrEditPatent        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(Patent patent,
            string AuthorFilter,
            int AuthorIdAdd, int AuthorIdRemove, decimal AuthorPart,
            int NirSpecialIdAdd, int NirSpecialIdRemove,
            int NirTemaIdAdd, int NirTemaIdRemove,
            CreateOrEditNirDataModeEnum mode, IFormFile uploadFile)
        {
            Patent patentEntry = _patentRepository.GetPatent(patent.PatentId);

            if (patentEntry == null)
            {
                if (uploadFile != null)
                {
                    FileModel f = await _fileModelRepository.UploadPatentAsync(patent, uploadFile);
                }
                _patentRepository.AddPatent(patent);
                patentEntry = patent;
            }
            else
            {
                if (uploadFile != null)
                {
                    FileModel f = await _fileModelRepository.UploadPatentAsync(patentEntry, uploadFile);
                    patent.FileModelId = patentEntry.FileModelId;
                }

                patent.PatentAuthors = patentEntry.PatentAuthors;
                patent.PatentVid = patentEntry.PatentVid;
                patent.PatentNirSpecials = patentEntry.PatentNirSpecials;
                patent.PatentNirTemas = patentEntry.PatentNirTemas;
                _patentRepository.UpdatePatent(patentEntry, patent);
            }

            switch (mode)
            {
                case CreateOrEditNirDataModeEnum.Saving:
                    patent.RowStatusId = (int)RowStatusEnum.NotConfirmed;
                    _patentRepository.UpdatePatent(patentEntry, patent);
                    return RedirectToAction("Index");

                case CreateOrEditNirDataModeEnum.Canceling:
                    if (patent.RowStatusId == null)
                    {
                        _patentRepository.RemovePatent(patentEntry.PatentId);
                    }
                    return RedirectToAction("Index");

                case CreateOrEditNirDataModeEnum.AddingAuthor:
                    if (AuthorIdAdd != 0)
                    {
                        var isExists = patent.PatentAuthors.FirstOrDefault(p => p.AuthorId == AuthorIdAdd) != null;
                        if (!isExists)
                        {
                            patent.PatentAuthors.Add(new PatentAuthor {
                                AuthorId = AuthorIdAdd,
                                AuthorPart = AuthorPart});
                            _patentRepository.UpdatePatent(patentEntry, patent);
                        }
                    }
                    break;

                case CreateOrEditNirDataModeEnum.EditingAuthor:
                    break;

                case CreateOrEditNirDataModeEnum.RemovingAuthor:
                    if (AuthorIdRemove!= 0)
                    {
                        var patentAuthorsToRemove = patent.PatentAuthors.FirstOrDefault(pp => pp.AuthorId == AuthorIdRemove);
                        if (patentAuthorsToRemove != null)
                        {
                            patent.PatentAuthors.Remove(patentAuthorsToRemove);
                            _patentRepository.UpdatePatent(patentEntry, patent);
                        }
                    }
                    break;

                case CreateOrEditNirDataModeEnum.ApplyAuthorFilter:
                    break;

                case CreateOrEditNirDataModeEnum.AddingNirSpecial:
                    if (NirSpecialIdAdd != 0)
                    {
                        var isExists = patent.PatentNirSpecials.FirstOrDefault(s => s.NirSpecialId == NirSpecialIdAdd) != null;
                        if (!isExists)
                        {
                            patent.PatentNirSpecials.Add(new PatentNirSpecial { NirSpecialId = NirSpecialIdAdd});
                            _patentRepository.UpdatePatent(patentEntry, patent);
                        }
                    }
                    break;

                case CreateOrEditNirDataModeEnum.EditingNirSpecial:
                    break;

                case CreateOrEditNirDataModeEnum.RemovingNirSpecial:
                    if (NirSpecialIdRemove != 0)
                    {
                        var patentToRemove = patent.PatentNirSpecials.FirstOrDefault(s => s.NirSpecialId == NirSpecialIdRemove);
                        if (patentToRemove != null)
                        {
                            patent.PatentNirSpecials.Remove(patentToRemove);
                            _patentRepository.UpdatePatent(patentEntry, patent);
                        }
                    }
                    break;

                case CreateOrEditNirDataModeEnum.AddingNirTema:
                    if(NirTemaIdAdd!=0)
                    {
                        var isExists = patent.PatentNirTemas.FirstOrDefault(s => s.NirTemaId == NirTemaIdAdd) != null;
                        if (!isExists)
                        {
                            patent.PatentNirTemas.Add(new PatentNirTema {NirTemaId = NirTemaIdAdd});
                            _patentRepository.UpdatePatent(patentEntry, patent);
                        }
                    }
                    break;

                case CreateOrEditNirDataModeEnum.EditingNirTema:
                    break;

                case CreateOrEditNirDataModeEnum.RemovingNirTema:
                    if(NirTemaIdRemove!=0)
                    {
                        var patentToRemove = patent.PatentNirTemas.FirstOrDefault(s => s.NirTemaId == NirTemaIdRemove);
                        if (patentToRemove != null)
                        {
                            patent.PatentNirTemas.Remove(patentToRemove);
                            _patentRepository.UpdatePatent(patentEntry, patent);
                        }
                    }
                    break;
                 default:
                    break;
            }

            ViewBag.AuthorFilter = AuthorFilter;
            ViewBag.Authors = _selectListRepository.GetSelectListAuthors(AuthorFilter);
            ViewBag.Years = _selectListRepository.GetSelectListYears(patent.YearId);
            ViewBag.NirSpecials = _selectListRepository.GetSelectListNirSpecials();
            ViewBag.NirTemas = _selectListRepository.GetSelectListNirTemas();

            return View(patentEntry);
        }        

        // GET: Patents/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var patentEntry = _patentRepository.GetPatent(id);            
            if (patentEntry == null) return NotFound();    
            return View(patentEntry);
        }

        // POST: Patents/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Patent patent)
        {
            if (patent == null) return NotFound();
            var patentEntry = _patentRepository.RemovePatent(patent.PatentId);

            return RedirectToAction("Index");
        }

        
    }
}
