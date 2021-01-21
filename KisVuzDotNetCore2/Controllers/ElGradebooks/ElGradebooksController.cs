using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Gradebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.ElGradebooks
{
    /// <summary>
    /// Контроллер для работы с зарегистрированными абитуриентами
    /// </summary>
    [Authorize(Roles = "Администраторы, Преподаватели")]
    public class ElGradebooksController : Controller
    {
        IElGradebookRepository _elGradebookRepository;
        ISelectListRepository _selectListRepository;

        public ElGradebooksController(IElGradebookRepository elGradebookRepository,
            ISelectListRepository selectListRepository)
        {
            _elGradebookRepository = elGradebookRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index(ElGradebooksFilterAndSortModel filterAndSortModel)
        {
            ViewBag.ElGradebooksFilterAndSortModel = filterAndSortModel;
            ViewBag.EduYears = new SelectList(new List<string> { "2020-2021" }, filterAndSortModel.FilterEduYear);
                        
            
            if (filterAndSortModel.IsRequestDataImmediately)
            {
                var data = _elGradebookRepository
                                .GetElGradebooks(filterAndSortModel, User.Identity.Name);

                data = data.OrderByDescending(g => g.GroupName);
                return View(await data.ToListAsync());
            }
            else
            {
                return View();
            }
        }
    }
}
