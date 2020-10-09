using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Интерфейс репозитория патентов (свидетельств)
    /// </summary>
    public interface IPatentRepository
    {
        /// <summary>
        /// Возвращает все патенты (свидетельства)
        /// </summary>        
        /// <returns></returns>
        IQueryable<Patent> GetPatents();

        /// <summary>
        /// Возвращает запрос на выборку всех патентов и свидетельств, опубликованных до указанного года включительно
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        IQueryable<Patent> GetPatents(int year);

        /// <summary>
        /// Патенты и свидетельства, ожидающие подтверждения
        /// </summary>
        /// <returns></returns>
        IQueryable<Patent> GetPatentsNotConfirmed();

        /// <summary>
        /// Возвращает патент (свидетельство) по УИД
        /// </summary>
        /// <param name="id"></param>        
        /// <returns></returns>
        Patent GetPatent(int? id);

        /// <summary>
        /// Обновляет патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="patentEntry"></param>
        /// <param name="patent"></param>
        void UpdatePatent(Patent patentEntry, Patent patent);

        /// <summary>
        /// Добавляет патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="patent"></param>        
        void AddPatent(Patent patent);

        /// <summary>
        /// Удаляет патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="patentId"></param>        
        Task<Patent> RemovePatentAsync(int patentId);

        /// <summary>
        /// Удаляет патенты (свидетельства)
        /// </summary>
        /// <param name="patentsToDelete"></param>
        /// <returns></returns>
        Task RemovePatentsAsync(List<Patent> patentsToDelete);

        /// <summary>
        /// Подтверждение патента (свидетельства)
        /// </summary>
        /// <param name="patentId"></param>
        void ConfirmPatent(int patentId);       
        
    }
}