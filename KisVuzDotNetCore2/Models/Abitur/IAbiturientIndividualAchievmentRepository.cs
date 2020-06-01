using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Интерфейс репозитория индивидуальных достижений абитуриента
    /// </summary>
    public interface IAbiturientIndividualAchievmentRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку всех индивидуальных достижений всех абитуриентов
        /// </summary>
        /// <returns></returns>
        IQueryable<AbiturientIndividualAchievment> GetAbiturientIndividualAchievments();
        
        /// <summary>
        /// Возвращает индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AbiturientIndividualAchievment> GetAbiturientIndividualAchievmentAsync(int id);

        /// <summary>
        /// Обновляет индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievment"></param>
        /// <returns></returns>
        Task Update(AbiturientIndividualAchievment abiturientIndividualAchievment, IFormFile uploadedFile);

        /// <summary>
        /// Удаляет индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievmentId"></param>
        /// <returns></returns>
        Task Remove(int abiturientIndividualAchievmentId);

        /// <summary>
        /// Создаёт индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task Create(AbiturientIndividualAchievment abiturientIndividualAchievment, IFormFile uploadedFile);
    }
}
