using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Интерфейс "Информация о профессионально-общественной и/или общественной аккредитации образовательной программы"
    /// </summary>
    public interface IEduPOAccredRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку информации о профессионально-общественной и/или общественной аккредитации образовательной программы
        /// </summary>
        IQueryable<EduPOAccred> GetEduPOAccreds();

        /// <summary>
        /// Возвращает информацию о профессионально-общественной и/или общественной аккредитации образовательной программы
        /// </summary>
        Task<EduPOAccred> GetAsync(int id);

        /// <summary>
        /// Добавляет информацию о профессионально-общественной и/или общественной аккредитации образовательной программы
        /// </summary>
        Task AddAsync(EduPOAccred eduPOAccred, IFormFile uploadedFile);

        /// <summary>
        /// Обновляет информацию о профессионально-общественной и/или общественной аккредитации образовательной программы
        /// </summary>
        Task UpdateAsync(EduPOAccred eduPOAccred, IFormFile uploadedFile);

        /// <summary>
        /// Удаляет информацию о профессионально-общественной и/или общественной аккредитации образовательной программы
        /// </summary>
        Task RemoveAsync(int id);
    }
}