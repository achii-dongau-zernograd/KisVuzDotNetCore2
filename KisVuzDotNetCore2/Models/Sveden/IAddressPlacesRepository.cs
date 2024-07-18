using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Интерфейс репозитория мест осуществления образовательной деятельности
    /// </summary>
    public interface IAddressPlacesRepository
    {
        /// <summary>
        /// Создаёт место осуществления образовательной деятельности
        /// </summary>
        /// <param name="addressPlace"></param>
        Task CreateAddressPlaceAsync(AddressPlace addressPlace);

        /// <summary>
        /// Возвращает запрос на выборку всех мест осуществления образовательной деятельности
        /// </summary>
        IQueryable<AddressPlace> GetAddressPlaces();

        /// <summary>
        /// Возвращает место осуществления образовательной деятельности
        /// </summary>
        /// <param name="id"></param>
        Task<AddressPlace> GetAddressPlaceAsync(int id);

        /// <summary>
        /// Обновляет место осуществления образовательной деятельности
        /// </summary>
        /// <param name="addressPlace"></param>
        Task UpdateAddressPlaceAsync(AddressPlace addressPlace);

        /// <summary>
        /// Удаляет место осуществления образовательной деятельности
        /// </summary>
        /// <param name="addressPlace"></param>
        Task RemoveAddressPlaceAsync(AddressPlace addressPlace);
    }
}
