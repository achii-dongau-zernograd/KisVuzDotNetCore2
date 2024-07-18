using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Sveden;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Common
{
    [Authorize(Roles = "Администраторы, Учебная часть")]
    public class AddressPlacesController : Controller
    {
        private readonly IAddressPlacesRepository _addressPlacesRepository;
        private readonly ISelectListRepository _selectListRepository;        

        public AddressPlacesController(IAddressPlacesRepository addressPlacesRepository,
            ISelectListRepository selectListRepository)
        {
            _addressPlacesRepository = addressPlacesRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<AddressPlace> addressPlaces = _addressPlacesRepository.GetAddressPlaces();

            return View(await addressPlaces.ToListAsync());
        }

        #region Создание
        public IActionResult Create()
        {           

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddressPlace addressPlace)
        {
            await _addressPlacesRepository.CreateAddressPlaceAsync(addressPlace);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Редактирование
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();
            var contract = await _addressPlacesRepository.GetAddressPlaceAsync(id);
            
            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddressPlace addressPlace)
        {
            await _addressPlacesRepository.UpdateAddressPlaceAsync(addressPlace);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Удаление
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();

            var contract = await _addressPlacesRepository.GetAddressPlaceAsync(id);
                        
            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AddressPlace addressPlace)
        {
            if (addressPlace == null) return NotFound();

            var entry = await _addressPlacesRepository.GetAddressPlaceAsync(addressPlace.AddressPlaceId);
            await _addressPlacesRepository.RemoveAddressPlaceAsync(entry);

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
