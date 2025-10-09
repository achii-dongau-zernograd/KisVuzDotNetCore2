using KisVuzDotNetCore2.Models.Files;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Репозиторий работ пользователей
    /// </summary>
    public class UserWorkRepository : IUserWorkRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;

        public UserWorkRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Возвращает количество работ пользователей, загруженное до указанной даты
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public async Task<int> GetNumUserWorksUploadedToDate(DateTime dateTime)
        {
            var num = await _context.UserWorks
                .Include(uw => uw.FileModel)
                .Where(uw => uw.FileModelId != null)
                .Where(uw => uw.FileModel.UploadDate <= dateTime)
                .CountAsync();

            return num;
        }

        /// <summary>
        /// Удаляет все работы пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveUserWorksAsync(string userName)
        {
            var userWorks = await _context.UserWorks
                .Include(uw => uw.FileModel)
                .Include(uw => uw.AppUser)
                .Include(uw => uw.UserWorkReviews)
                    .ThenInclude(uwr => uwr.FileModel)
                .Where(uw => uw.AppUser.UserName == userName)
                .ToListAsync();

            foreach (var userWork in userWorks)
            {
                foreach (var userWorkReview in userWork.UserWorkReviews)
                {
                    await _fileModelRepository.RemoveFileModelAsync(userWorkReview.FileModel);
                    _context.UserWorkReviews.Remove(userWorkReview);
                    await _context.SaveChangesAsync();
                }
                
                await _fileModelRepository.RemoveFileModelAsync(userWork.FileModel);
                _context.UserWorks.Remove(userWork);
                await _context.SaveChangesAsync();
            }

        }

        /// <summary>
        /// Удаляет файлы работ пользователей, загруженные до указанной даты
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task RemoveUserWorksToDateAsync(DateTime date)
        {
            var userWorksToDelete = await _context.UserWorks
                .Include(uw => uw.FileModel)
                .Where(uw => uw.FileModelId != null)
                .Where(uw => uw.FileModel.UploadDate <= date)
                .Take(1000)
                .ToListAsync();

            foreach (var userWork in userWorksToDelete)
            {
                await _fileModelRepository.RemoveFileModelAsync(userWork.FileModel);
            }
        }
    }
}
