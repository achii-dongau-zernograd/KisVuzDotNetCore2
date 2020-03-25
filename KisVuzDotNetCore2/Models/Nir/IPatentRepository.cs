using System.Collections.Generic;
using System.Linq;

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
        Patent RemovePatent(int patentId);

        /// <summary>
        /// Подтверждение патента (свидетельства)
        /// </summary>
        /// <param name="patentId"></param>
        void ConfirmPatent(int patentId);        
    }
}