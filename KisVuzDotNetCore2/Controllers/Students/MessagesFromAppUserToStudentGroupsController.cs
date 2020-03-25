using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Students;
using Microsoft.AspNetCore.Authorization;
using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Users;

namespace KisVuzDotNetCore2.Controllers.Students
{
    [Authorize]
    public class MessagesFromAppUserToStudentGroupsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IMessagesFromAppUserToStudentGroupsRepository _messagesFromAppUserToStudentGroupsRepository;
        private readonly ISelectListRepository _selectListRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public MessagesFromAppUserToStudentGroupsController(AppIdentityDBContext context,
            IMessagesFromAppUserToStudentGroupsRepository messagesFromAppUserToStudentGroupsRepository,
            ISelectListRepository selectListRepository,
            IUserProfileRepository userProfileRepository)
        {
            _context = context;
            _messagesFromAppUserToStudentGroupsRepository = messagesFromAppUserToStudentGroupsRepository;
            _selectListRepository = selectListRepository;
            _userProfileRepository = userProfileRepository;
        }

        // GET: MessagesFromAppUsersToStudentGroups
        public async Task<IActionResult> Index()
        {
            var appUser = _userProfileRepository.GetAppUser(User.Identity.Name);

            var groupMessages = _messagesFromAppUserToStudentGroupsRepository.GetMessagesFromAppUserToStudentGroups();

            // Сообщения, отправленные пользователем студенческим группам
            var sendedMessagesByUser = await groupMessages.Where(gm => gm.AppUserId == appUser.Id).ToListAsync();

            // Сообщения, полученные пользователем, являющимся студентом учебной группы
            var receivedGroupMessages = new List<MessageFromAppUserToStudentGroup>();
            foreach (var student in appUser.Students)
            {
                var items = await groupMessages
                    .Where(gm => gm.StudentGroupId == student.StudentGroupId)
                    .ToListAsync();
                receivedGroupMessages.AddRange(items);
            }

            var modelTuple = new Tuple<List<MessageFromAppUserToStudentGroup>, List<MessageFromAppUserToStudentGroup>, AppUser> (sendedMessagesByUser, receivedGroupMessages, appUser);

            return View(modelTuple);
        }
               

        // GET: MessagesFromAppUsersToStudentGroups/Create
        public IActionResult Create()
        {
            var newMessage = new MessageFromAppUserToStudentGroup
            {
                AppUserId = _userProfileRepository.GetAppUserId(User.Identity.Name),
                DateTime = DateTime.Now
            };

            ViewData["DisciplineNameId"]  = _selectListRepository.GetSelectListDisciplineNames();
            ViewData["StudentGroupId"]    = _selectListRepository.GetSelectListStudentGroups();
            ViewData["UserMessageTypeId"] = _selectListRepository.GetSelectListUserMessageTypes();
            return View(newMessage);
        }

        // POST: MessagesFromAppUsersToStudentGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,AppUserId,StudentGroupId,DisciplineNameId,UserMessageTypeId,MessageText,Link")] MessageFromAppUserToStudentGroup messageFromAppUserToStudentGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messageFromAppUserToStudentGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplineNameId"] = _selectListRepository.GetSelectListDisciplineNames(messageFromAppUserToStudentGroup.DisciplineNameId);
            ViewData["StudentGroupId"] = _selectListRepository.GetSelectListStudentGroups(messageFromAppUserToStudentGroup.StudentGroupId);
            ViewData["UserMessageTypeId"] = _selectListRepository.GetSelectListUserMessageTypes(messageFromAppUserToStudentGroup.UserMessageTypeId);
            return View(messageFromAppUserToStudentGroup);
        }

        // GET: MessagesFromAppUsersToStudentGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageFromAppUserToStudentGroup = await _messagesFromAppUserToStudentGroupsRepository.GetMessageFromAppUserToStudentGroupAsync(id);
            if (messageFromAppUserToStudentGroup == null)
            {
                return NotFound();
            }
            
            ViewData["DisciplineNameId"] = _selectListRepository.GetSelectListDisciplineNames(messageFromAppUserToStudentGroup.DisciplineNameId);
            ViewData["StudentGroupId"] = _selectListRepository.GetSelectListStudentGroups(messageFromAppUserToStudentGroup.StudentGroupId);
            ViewData["UserMessageTypeId"] = _selectListRepository.GetSelectListUserMessageTypes(messageFromAppUserToStudentGroup.UserMessageTypeId);

            return View(messageFromAppUserToStudentGroup);
        }

        // POST: MessagesFromAppUsersToStudentGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTime,AppUserId,StudentGroupId,DisciplineNameId,UserMessageTypeId,MessageText,Link")] MessageFromAppUserToStudentGroup messageFromAppUserToStudentGroup)
        {
            if (id != messageFromAppUserToStudentGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageFromAppUserToStudentGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageFromAppUserToStudentGroupExists(messageFromAppUserToStudentGroup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplineNameId"] = _selectListRepository.GetSelectListDisciplineNames(messageFromAppUserToStudentGroup.DisciplineNameId);
            ViewData["StudentGroupId"] = _selectListRepository.GetSelectListStudentGroups(messageFromAppUserToStudentGroup.StudentGroupId);
            ViewData["UserMessageTypeId"] = _selectListRepository.GetSelectListUserMessageTypes(messageFromAppUserToStudentGroup.UserMessageTypeId);
            return View(messageFromAppUserToStudentGroup);
        }

        // GET: MessagesFromAppUsersToStudentGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                        
            var messageFromAppUserToStudentGroup = await _messagesFromAppUserToStudentGroupsRepository.GetMessageFromAppUserToStudentGroupAsync(id);
                        
            if (messageFromAppUserToStudentGroup == null)
            {
                return NotFound();
            }

            return View(messageFromAppUserToStudentGroup);
        }

        // POST: MessagesFromAppUsersToStudentGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messageFromAppUserToStudentGroup = await _context.MessagesFromAppUsersToStudentGroups.SingleOrDefaultAsync(m => m.Id == id);
            _context.MessagesFromAppUsersToStudentGroups.Remove(messageFromAppUserToStudentGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageFromAppUserToStudentGroupExists(int id)
        {
            return _context.MessagesFromAppUsersToStudentGroups.Any(e => e.Id == id);
        }
    }
}
