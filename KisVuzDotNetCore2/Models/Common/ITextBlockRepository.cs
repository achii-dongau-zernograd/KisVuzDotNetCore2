using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Интерфейс репозитория текстовых блоков
    /// </summary>
    public interface ITextBlockRepository
    {
        /// <summary>
        /// Создаёт текстовый блок
        /// </summary>        
        Task CreateTextBlockAsync(TextBlock textBlock);

        /// <summary>
        /// Возвращает запрос на выборку всех текстовых блоков
        /// </summary>
        /// <returns></returns>
        IQueryable<TextBlock> GetTextBlocks();

        /// <summary>
        /// Возвращает запрос на выборку всех текстовых блоков с заданным тегом
        /// </summary>
        /// <returns></returns>
        IQueryable<TextBlock> GetTextBlocks(string textBlockTag);

        /// <summary>
        /// Возвращает текстовый блок
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TextBlock> GetTextBlockAsync(int id);

        /// <summary>
        /// Обновляет текстовый блок
        /// </summary>
        /// <param name="textBlock"></param>
        /// <returns></returns>
        Task UpdateTextBlockAsync(TextBlock textBlock);

        /// <summary>
        /// Удаляет текстовый блок
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        Task RemoveTextBlockAsync(TextBlock entry);
    }
}
