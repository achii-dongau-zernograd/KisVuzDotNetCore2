using System;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Students
{
    /// <summary>
    /// Интерфейс сообщений пользователя группе студентов
    /// </summary>
    public interface IMessagesFromAppUserToStudentGroupsRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку всех записей, содержащих сообщения пользователей учебным группам
        /// </summary>        
        IQueryable<MessageFromAppUserToStudentGroup> GetMessagesFromAppUserToStudentGroups();

        /// <summary>
        /// Возвращает сообщение студенческой группе по указанному УИД 
        /// </summary>        
        Task<MessageFromAppUserToStudentGroup> GetMessageFromAppUserToStudentGroupAsync(int? id);

        /// <summary>
        /// Удаляет все сообщения учебным группам до указанной даты
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        Task RemoveMessagesToDate(DateTime dateTime);
    }
}