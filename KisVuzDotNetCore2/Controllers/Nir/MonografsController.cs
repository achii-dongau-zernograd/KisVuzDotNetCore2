using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Users;
using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Common;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    [Authorize(Roles = "Администраторы, НИЧ")]
    public class MonografsController : Controller
    {
        private IUserProfileRepository _userProfileRepository;
        private ISelectListRepository _selectListRepository;
        private IMonografRepository _monografRepository;
        private IFileModelRepository _fileModelRepository;
       
                
        public MonografsController(IUserProfileRepository userProfileRepository,
            ISelectListRepository selectListRepository,
            IMonografRepository monografRepository,
            IFileModelRepository fileModelRepository
           )
        {
            _userProfileRepository = userProfileRepository;
            _selectListRepository = selectListRepository;
            _fileModelRepository = fileModelRepository;
            _monografRepository = monografRepository;                      
        }

        // GET: Monografs
        public IActionResult Index()
        {
            List<Monograf> monografs = _monografRepository.GetMonografs();
            return View(monografs);
        }


        // GET: Monografs/CreateOrEdit
        public IActionResult CreateOrEdit(int? id)
        {
            Monograf monograf = _monografRepository.GetMonograf(id);
            if (monograf == null) return NotFound();

            ViewBag.Authors = _selectListRepository.GetSelectListAuthors();
            ViewBag.Years = _selectListRepository.GetSelectListYears(monograf.YearId);
            ViewBag.NirSpecials = _selectListRepository.GetSelectListNirSpecials();
            ViewBag.NirTemas = _selectListRepository.GetSelectListNirTemas();

                                 
            //ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Name");
            //ViewData["RowStatusId"] = new SelectList(_context.RowStatuses, "RowStatusId", "RowStatusName");
            //ViewData["YearId"] = new SelectList(_context.Years, "YearId", "YearName");

            return View(monograf);
        }

        // POST: Monografs/CreateOrEdit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit( Monograf monograf,
            string AuthorFilter,
            int AuthorIdAdd, int AuthorIdRemove, decimal AuthorPart,
            int NirSpecialIdAdd, int NirSpecialIdRemove,
            int NirTemaIdAdd, int NirTemaIdRemove,
            CreateOrEditNirDataModeEnum mode,
            IFormFile uploadedFile)
        {
            Monograf monografEntry = _monografRepository.GetMonograf(monograf.MonografId);

            if (monografEntry==null)
            {
                if (uploadedFile !=null)
                {
                    FileModel f = await _fileModelRepository.UploadMonografAsync(monograf, uploadedFile);
                }
                _monografRepository.AddMonograf(monograf);
                monografEntry = monograf;
            }
            else
            {
                if (uploadedFile !=null)
                {
                    FileModel f = await _fileModelRepository.UploadMonografAsync(monografEntry, uploadedFile);
                    monograf.FileModelId = monografEntry.FileModelId;
                }
                monograf.MonografNirSpecials = monografEntry.MonografNirSpecials;
                monograf.MonografAuthors = monografEntry.MonografAuthors;
                monograf.MonografNirTemas = monografEntry.MonografNirTemas;
               _monografRepository.UpdateMonograf(monografEntry, monograf);
                }
                                 
            switch(mode)
            {
                case CreateOrEditNirDataModeEnum.Saving:
                    monograf.RowStatusId = (int)RowStatusEnum.NotConfirmed;
                    _monografRepository.UpdateMonograf(monografEntry, monograf);
                    return RedirectToAction("Index");
                case CreateOrEditNirDataModeEnum.Canceling:
                    if(monograf.RowStatusId==null)
                    {
                        _monografRepository.RemoveMonografAsync(monografEntry.MonografId);
                    }
                    return RedirectToAction("Index");
                case CreateOrEditNirDataModeEnum.AddingAuthor:
                    if(AuthorIdAdd!=0)
                    {
                        var isExist = monograf.MonografAuthors.FirstOrDefault(a => a.AuthorId == AuthorIdAdd) != null;
                        if (!isExist)
                        {
                            monograf.MonografAuthors.Add(new MonografAuthor
                            {
                                AuthorId = AuthorIdAdd,
                                AuthorPart = AuthorPart
                            });
                            _monografRepository.UpdateMonograf(monografEntry, monograf);
                        }
                    }
                    break;
                case CreateOrEditNirDataModeEnum.EditingAuthor:
                    break;
                case CreateOrEditNirDataModeEnum.RemovingAuthor:
                    if (AuthorIdRemove!=0)
                    {
                        var monografAuthorsToRemove = monograf.MonografAuthors.FirstOrDefault(aa => aa.AuthorId == AuthorIdRemove);
                        if (monografAuthorsToRemove!=null)
                            {
                            monograf.MonografAuthors.Remove(monografAuthorsToRemove);
                            _monografRepository.UpdateMonograf(monografEntry, monograf);
                        }
                    }
                    break;
                case CreateOrEditNirDataModeEnum.ApplyAuthorFilter:
                    break;
                case CreateOrEditNirDataModeEnum.AddingNirSpecial:
                    if (NirSpecialIdAdd!=0)
                    {
                        var isExist = monograf.MonografNirSpecials.FirstOrDefault(s => s.NirSpecialId == NirSpecialIdAdd) != null;
                        if (!isExist)
                        {
                            monograf.MonografNirSpecials.Add(new MonografNirSpecial { NirSpecialId = NirSpecialIdAdd });
                            _monografRepository.UpdateMonograf(monografEntry, monograf);
                        }
                    }
                    break;
                case CreateOrEditNirDataModeEnum.EditingNirSpecial:
                    break;
                case CreateOrEditNirDataModeEnum.RemovingNirSpecial:
                    if (NirSpecialIdRemove != 0)
                    {
                        var monografToRemove = monograf.MonografNirSpecials.FirstOrDefault(s => s.NirSpecialId == NirSpecialIdRemove);
                        if (monografToRemove!=null)
                        {
                            monograf.MonografNirSpecials.Remove(monografToRemove);
                            _monografRepository.UpdateMonograf(monografEntry, monograf);
                        }
                    }
                    break;
                case CreateOrEditNirDataModeEnum.AddingNirTema:
                    if (NirTemaIdAdd !=0)
                    {
                        var isExists = monograf.MonografNirTemas.FirstOrDefault(s => s.NirTemaId == NirTemaIdAdd) != null;
                        if (!isExists)
                        {
                            monograf.MonografNirTemas.Add(new MonografNirTema { NirTemaId = NirTemaIdAdd });
                            _monografRepository.UpdateMonograf(monografEntry, monograf);
                        }
                    }
                    break;
                case CreateOrEditNirDataModeEnum.EditingNirTema:
                break;
                case CreateOrEditNirDataModeEnum.RemovingNirTema:
                    if (NirTemaIdRemove !=0)
                    {
                        var monografToRemove = monograf.MonografNirTemas.FirstOrDefault(s => s.NirTemaId == NirTemaIdRemove);
                        if (monografToRemove !=null)
                        {
                            monograf.MonografNirTemas.Remove(monografToRemove);
                            _monografRepository.UpdateMonograf(monografEntry, monograf);
                        }
                    }
                    break;
                default:
                    break;
            }
            ViewBag.AuthorFilter = AuthorFilter;
            ViewBag.Authors = _selectListRepository.GetSelectListAuthors(AuthorFilter);
            ViewBag.Years = _selectListRepository.GetSelectListYears(monograf.YearId);
            ViewBag.NirSpecials = _selectListRepository.GetSelectListNirSpecials();
            ViewBag.NirTemas = _selectListRepository.GetSelectListNirTemas();

            return View(monografEntry);
                                          

            //if (ModelState.IsValid && uploadedFile != null)
            //{

            //    FileModel f = await _fileModelRepository.UploadMonografAsync(monograf, uploadedFile);
            //}
            //ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Name", monograf.FileModelId);
            //ViewData["RowStatusId"] = new SelectList(_context.RowStatuses, "RowStatusId", "RowStatusName", monograf.RowStatusId);
            //ViewData["YearId"] = new SelectList(_context.Years, "YearId", "YearName", monograf.YearId);
            //return View(monograf);
        }

       

        // GET: Monografs/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monograf = _monografRepository.GetMonograf(id);
            if (monograf == null)
            {
                return NotFound();
            }

            return View(monograf);
        }

        // POST: Monografs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _monografRepository.RemoveMonografAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
