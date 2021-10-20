using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Students
{
    public class MessagesFromAppUserToStudentGroupsRepository : IMessagesFromAppUserToStudentGroupsRepository
    {
        AppIdentityDBContext _context;
        public MessagesFromAppUserToStudentGroupsRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех записей, содержащих сообщения пользователей учебным группам
        /// </summary>
        /// <returns></returns>
        public IQueryable<MessageFromAppUserToStudentGroup> GetMessagesFromAppUserToStudentGroups()
        {
            var messages = _context.MessagesFromAppUsersToStudentGroups
                .Include(m => m.DisciplineName)
                .Include(m => m.StudentGroup.EduKurs)
                .Include(m => m.UserMessageType)
                .Include(m => m.AppUser)
                .OrderByDescending(m => m.DateTime);

            return messages;
        }

        /// <summary>
        /// Возвращает сообщение студенческой группе по указанному УИД
        /// </summary>        
        public async Task<MessageFromAppUserToStudentGroup> GetMessageFromAppUserToStudentGroupAsync(int? id)
        {
            var entry = await GetMessagesFromAppUserToStudentGroups().FirstOrDefaultAsync(m => m.Id == id);
            return entry;
        }

        /// <summary>
        /// Удаляет все сообщения учебным группам до указанной даты
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public async Task RemoveMessagesToDate(DateTime dateTime)
        {
            var query = _context.MessagesFromAppUsersToStudentGroups.Where(m => m.DateTime <= dateTime);
            _context.MessagesFromAppUsersToStudentGroups.RemoveRange(query);
            await _context.SaveChangesAsync();
        }
    }
}
