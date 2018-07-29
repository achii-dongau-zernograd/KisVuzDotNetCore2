using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Users
{
    [Authorize]
    public class UserMessagesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UserMessagesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает объект активного пользователя
        /// </summary>
        /// <returns></returns>
        private async Task<AppUser> GetCurrentUser()
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == User.Identity.Name);
        }

        // GET: UserMessages
        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUser();
            if(currentUser==null)
            {
                return NotFound();
            }

            var userMessages = _context.UserMessages
                .Include(u => u.UserReceiver)
                .Include(u => u.UserSender)
                .Where(u => u.UserReceiverId==currentUser.Id || u.UserSenderId== currentUser.Id)
                .OrderByDescending(u => u.UserMessageDate);
            return View(await userMessages.ToListAsync());
        }        

        // GET: UserMessages/Create
        public async Task<IActionResult> Create(string UserReceiverId)
        {
            if(string.IsNullOrWhiteSpace(UserReceiverId))
            {
                return RedirectToAction("Index");
            }

            var newMessage = new UserMessage();
            newMessage.UserReceiverId = UserReceiverId;
            newMessage.UserSenderId = (await GetCurrentUser()).Id;
            
            return RedirectToAction(nameof(Edit), newMessage);
        }

        // POST: UserMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("UserMessageId,UserSenderId,UserReceiverId,UserMessageDate,UserMessageText,IsReadedByReceiver")] UserMessage userMessage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(userMessage);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "Id", userMessage.UserReceiverId);
        //    ViewData["UserSenderId"] = new SelectList(_context.Users, "Id", "Id", userMessage.UserSenderId);
        //    return View(userMessage);
        //}

        // GET: UserMessages/Edit/5
        public async Task<IActionResult> Edit(int? id, UserMessage newMessage)
        {
            if(id==null && newMessage==null)
            {
                return NotFound();
            }

            UserMessage userMessage = new UserMessage();
            // Если передан id, значит редактируем существующую запись
            if (id != null)
            {
                userMessage = await _context.UserMessages.Include(m => m.UserSender).Include(m=>m.UserReceiver).SingleOrDefaultAsync(m => m.UserMessageId == id);
                if (userMessage == null)
                {
                    return NotFound();
                }
            } // Если передан newMessage, значит редактируем новую запись
            else if (newMessage != null)
            {
                userMessage = newMessage;
            }

            if(userMessage.UserSender==null)
            {
                userMessage.UserSender = await _context.Users.SingleOrDefaultAsync(u=>u.Id == userMessage.UserSenderId);
            }

            if (userMessage.UserReceiver == null)
            {
                userMessage.UserReceiver = await _context.Users.SingleOrDefaultAsync(u => u.Id == userMessage.UserReceiverId);
            }

            return View(userMessage);
        }

        // POST: UserMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserMessageId,UserSenderId,UserReceiverId,UserMessageDate,UserMessageText,IsReadedByReceiver")] UserMessage userMessage)
        {
            if (id != userMessage.UserMessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMessageExists(userMessage.UserMessageId))
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
            
            return View(userMessage);
        }

        // GET: UserMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _context.UserMessages
                .Include(u => u.UserReceiver)
                .Include(u => u.UserSender)
                .SingleOrDefaultAsync(m => m.UserMessageId == id);
            if (userMessage == null)
            {
                return NotFound();
            }

            return View(userMessage);
        }

        // POST: UserMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userMessage = await _context.UserMessages.SingleOrDefaultAsync(m => m.UserMessageId == id);
            _context.UserMessages.Remove(userMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Отмечает сообщение как прочитанное
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ReadMessage(int id)
        {
            var userMessage = await _context.UserMessages.SingleOrDefaultAsync(m => m.UserMessageId == id);
            if(userMessage!=null)
            {
                userMessage.IsReadedByReceiver = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserMessageExists(int id)
        {
            return _context.UserMessages.Any(e => e.UserMessageId == id);
        }
    }
}
