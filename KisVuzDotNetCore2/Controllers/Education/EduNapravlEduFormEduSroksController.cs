using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Infrastructure;

namespace KisVuzDotNetCore2.Controllers.Education
{
    public class EduNapravlEduFormEduSroksController : Controller
    {        
        private readonly IEduNapravlRepository _eduNapravlRepository;
        private readonly ISelectListRepository _selectListRepository;

        public EduNapravlEduFormEduSroksController(IEduNapravlRepository eduNapravlRepository,
            ISelectListRepository selectListRepository)
        {            
            _eduNapravlRepository = eduNapravlRepository;
            _selectListRepository = selectListRepository;
        }

        // GET: EduNapravlEduFormEduSroks
        public async Task<IActionResult> Index()
        {
            var eduNapravlEduFormEduSroks = await _eduNapravlRepository.GetEduNapravlEduFormEduSroksAsync();            
            return View(eduNapravlEduFormEduSroks);
        }

        
        // GET: EduNapravlEduFormEduSroks/Create
        public IActionResult Create()
        {
            ViewData["EduForms"] = _selectListRepository.GetSelectListEduForms();
            ViewData["EduNapravls"] = _selectListRepository.GetSelectListEduNapravlFullNames();
            ViewData["EduSroks"] = _selectListRepository.GetSelectListEduSrok();
            return View();
        }

        // POST: EduNapravlEduFormEduSroks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduNapravlEduFormEduSrokId,EduNapravlId,EduFormId,EduSrokId")] EduNapravlEduFormEduSrok eduNapravlEduFormEduSrok)
        {
            if (ModelState.IsValid)
            {
                await _eduNapravlRepository.AddEduNapravlEduFormEduSrokAsync(eduNapravlEduFormEduSrok);
                return RedirectToAction(nameof(Index));
            }

            ViewData["EduForms"] = _selectListRepository.GetSelectListEduForms(eduNapravlEduFormEduSrok.EduFormId);
            ViewData["EduNapravls"] = _selectListRepository.GetSelectListEduNapravlFullNames(eduNapravlEduFormEduSrok.EduNapravlId);
            ViewData["EduSroks"] = _selectListRepository.GetSelectListEduSrok(eduNapravlEduFormEduSrok.EduSrokId);
            
            return View(eduNapravlEduFormEduSrok);
        }

        // GET: EduNapravlEduFormEduSroks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNapravlEduFormEduSrok = await _eduNapravlRepository.GetEduNapravlEduFormEduSrokAsync(id);
            if (eduNapravlEduFormEduSrok == null)
            {
                return NotFound();
            }
            ViewData["EduForms"] = _selectListRepository.GetSelectListEduForms(eduNapravlEduFormEduSrok.EduFormId);
            ViewData["EduNapravls"] = _selectListRepository.GetSelectListEduNapravlFullNames(eduNapravlEduFormEduSrok.EduNapravlId);
            ViewData["EduSroks"] = _selectListRepository.GetSelectListEduSrok(eduNapravlEduFormEduSrok.EduSrokId);
            return View(eduNapravlEduFormEduSrok);
        }

        // POST: EduNapravlEduFormEduSroks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduNapravlEduFormEduSrokId,EduNapravlId,EduFormId,EduSrokId")] EduNapravlEduFormEduSrok eduNapravlEduFormEduSrok)
        {
            if (id != eduNapravlEduFormEduSrok.EduNapravlEduFormEduSrokId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _eduNapravlRepository.UpdateEduNapravlEduFormEduSrokAsync(eduNapravlEduFormEduSrok);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduNapravlEduFormEduSrokExists(eduNapravlEduFormEduSrok.EduNapravlEduFormEduSrokId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduForms"] = _selectListRepository.GetSelectListEduForms(eduNapravlEduFormEduSrok.EduFormId);
            ViewData["EduNapravls"] = _selectListRepository.GetSelectListEduNapravlFullNames(eduNapravlEduFormEduSrok.EduNapravlId);
            ViewData["EduSroks"] = _selectListRepository.GetSelectListEduSrok(eduNapravlEduFormEduSrok.EduSrokId);
            return View(eduNapravlEduFormEduSrok);
        }

        // GET: EduNapravlEduFormEduSroks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNapravlEduFormEduSrok = await _eduNapravlRepository.GetEduNapravlEduFormEduSrokAsync(id);
            if (eduNapravlEduFormEduSrok == null)
            {
                return NotFound();
            }

            return View(eduNapravlEduFormEduSrok);
        }

        // POST: EduNapravlEduFormEduSroks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduNapravlEduFormEduSrok = await _eduNapravlRepository.GetEduNapravlEduFormEduSrokAsync(id);
            await _eduNapravlRepository.RemoveEduNapravlEduFormEduSrokAsync(eduNapravlEduFormEduSrok);
            
            return RedirectToAction(nameof(Index));
        }

        private bool EduNapravlEduFormEduSrokExists(int id)
        {
            return _eduNapravlRepository.IsEduNapravlEduFormEduSrokExists(id);
        }
    }
}
