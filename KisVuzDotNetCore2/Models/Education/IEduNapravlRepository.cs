using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Интерфейс репозитория направлений подготовки
    /// </summary>
    public interface IEduNapravlRepository
    {
        /// <summary>
        /// Возвращает набор записей, связывающих направления подготовки, формы обучения и нормативный срок обучения 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EduNapravlEduFormEduSrok>> GetEduNapravlEduFormEduSroksAsync();

        /// <summary>
        /// Возвращает запись, связывающую направление подготовки, форму обучения и нормативный срок обучения, по переданному УИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EduNapravlEduFormEduSrok> GetEduNapravlEduFormEduSrokAsync(int? id);

        /// <summary>
        /// Удаляет запись, связывающую направление подготовки, форму обучения и нормативный срок обучения
        /// </summary>
        /// <param name="eduNapravlEduFormEduSrok"></param>
        /// <returns></returns>
        Task RemoveEduNapravlEduFormEduSrokAsync(EduNapravlEduFormEduSrok eduNapravlEduFormEduSrok);

        /// <summary>
        /// Добавляет запись, связывающую направление подготовки, форму обучения и нормативный срок обучения
        /// </summary>
        /// <param name="eduNapravlEduFormEduSrok"></param>
        /// <returns></returns>
        Task AddEduNapravlEduFormEduSrokAsync(EduNapravlEduFormEduSrok eduNapravlEduFormEduSrok);

        /// <summary>
        /// Обновляет запись, связывающую направление подготовки, форму обучения и нормативный срок обучения
        /// </summary>
        /// <param name="eduNapravlEduFormEduSrok"></param>
        /// <returns></returns>
        Task UpdateEduNapravlEduFormEduSrokAsync(EduNapravlEduFormEduSrok eduNapravlEduFormEduSrok);

        /// <summary>
        /// Определяет, существует ли запись с указанным УИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsEduNapravlEduFormEduSrokExists(int id);
    }
}
