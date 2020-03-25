using System.Collections.Generic;
using System.Linq;

namespace KisVuzDotNetCore2.Models.Nir
{
    public interface IMonografRepository
    {
        /// <summary>
        /// Возвращает все монографии
        /// </summary>        
        /// <returns></returns>
        IQueryable<Monograf> GetMonografs();

        /// <summary>
        /// Возвращает монографии, ожидающие подтверждения
        /// </summary>
        /// <returns></returns>
        IQueryable<Monograf> GetMonografsNotConfirmed();

        /// <summary>
        /// Возвращает монографию по УИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Monograf GetMonograf(int? id);

        /// <summary>
        /// Добавляет монографию пользователя userName
        /// </summary>
        /// <param name="monograf"></param>
        void AddMonograf(Monograf monograf);

        /// <summary>
        /// Обновляет монографию пользователя userName
        /// </summary>
        /// <param name="monografEntry"></param>
        /// <param name="monograf"></param>
        void UpdateMonograf(Monograf monografEntry, Monograf monograf);

        /// <summary>
        /// Удаляет монографию пользователя userName
        /// </summary>
        /// <param name="monografId"></param>
        void RemoveMonografAsync(int monografId);

        /// <summary>
        /// Подтверждение монографии
        /// </summary>
        /// <param name="monografId"></param>
        void ConfirmMonograf(int monografId);
    }
}