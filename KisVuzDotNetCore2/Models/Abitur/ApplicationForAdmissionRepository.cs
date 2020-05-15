using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Репозиторий заявлений о приёме
    /// </summary>
    public class ApplicationForAdmissionRepository : IApplicationForAdmissionRepository
    {
        private readonly AppIdentityDBContext _context;
        public ApplicationForAdmissionRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Проверяет наличие заявлений о приёме для указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsApplicationForAdmissionExists(string userName)
        {

            var applicationsForAdmission = GetApplicationForAdmissions(userName);
            if (applicationsForAdmission != null && applicationsForAdmission.Count() > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех заявлений о приёме указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<ApplicationForAdmission> GetApplicationForAdmissions(string userName)
        {
            var query = GetApplicationForAdmissions()
                .Where(a => a.Abiturient.AppUser.UserName == userName);

            return query;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех заявлений о приёме
        /// </summary>
        /// <returns></returns>
        public IQueryable<ApplicationForAdmission> GetApplicationForAdmissions()
        {
            var query = _context.ApplicationForAdmissions
                .Include(afa => afa.Abiturient.AppUser)
                .Include(afa => afa.EduForm)
                .Include(afa => afa.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(afa => afa.QuotaType);

            return query;
        }
    }
}
