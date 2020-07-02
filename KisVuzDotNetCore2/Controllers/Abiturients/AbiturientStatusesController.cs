using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Abitur;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Abiturients
{
    [Authorize(Roles="Администраторы, Приёмная комиссия")]
    public class AbiturientStatusesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public AbiturientStatusesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.AbiturientStatuses.ToList();
            return View(data);
        }

        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AbiturientStatus abiturientStatus)
        {
            _context.AbiturientStatuses.Add(abiturientStatus);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int id)
        {
            var abiturientStatus = _context.AbiturientStatuses.FirstOrDefault(a=>a.AbiturientStatusId == id);
            return View(abiturientStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AbiturientStatus abiturientStatus)
        {
            _context.AbiturientStatuses.Update(abiturientStatus);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var abiturientStatus = _context.AbiturientStatuses.FirstOrDefault(a => a.AbiturientStatusId == id);
            return View(abiturientStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(AbiturientStatus abiturientStatus)
        {
            _context.AbiturientStatuses.Remove(abiturientStatus);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
