using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Репозиторий мест осуществления образовательной деятельности
    /// </summary>
    public class AddressPlacesRepository : IAddressPlacesRepository
    {
        private readonly AppIdentityDBContext _context;

        public AddressPlacesRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех мест осуществления образовательной деятельности
        /// </summary>
        /// <returns></returns>
        public IQueryable<AddressPlace> GetAddressPlaces()
        {
            var addressPlaces = _context.AddressPlaces;

            return addressPlaces;
        }

        /// <summary>
        /// Создаёт место осуществления образовательной деятельности
        /// </summary>        
        public async Task CreateAddressPlaceAsync(AddressPlace addressPlace)
        {
            _context.Add(addressPlace);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает место осуществления образовательной деятельности
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AddressPlace> GetAddressPlaceAsync(int id)
        {
            var addressPlace = await GetAddressPlaces().SingleOrDefaultAsync(c => c.AddressPlaceId == id);

            return addressPlace;
        }

        /// <summary>
        /// Обновляет место осуществления образовательной деятельности
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateAddressPlaceAsync(AddressPlace addressPlace)
        {
            if (addressPlace == null) return;                        

            _context.Update(addressPlace);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет место осуществления образовательной деятельности
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public async Task RemoveAddressPlaceAsync(AddressPlace addressPlace)
        {
            if (addressPlace == null) return;
                        
            _context.Remove(addressPlace);
            await _context.SaveChangesAsync();
        }
    }
}
