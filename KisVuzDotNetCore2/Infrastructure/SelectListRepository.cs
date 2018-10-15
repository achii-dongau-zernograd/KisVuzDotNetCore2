using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KisVuzDotNetCore2.Infrastructure
{
    /// <summary>
    /// Репозиторий списков (справочников)
    /// </summary>
    public class SelectListRepository : ISelectListRepository
    {
        private readonly AppIdentityDBContext _context;

        public SelectListRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает список пользователей по строке ФИО
        /// </summary>
        /// <param name="fio"></param>
        /// <returns></returns>
        public SelectList GetSelectListAppUsersByFirstName(string fio)
        {
            string lastName       = "";
            string other          = "";
            string firstNameAbbr  = "";
            string patronymicAbbr = "";
            List<AppUser> findedAppUsers;

            string[] lastNameAndOtherList = fio.Split(' ');
            if(lastNameAndOtherList.Count() > 1)
            {
                lastName = lastNameAndOtherList[0];
                other    = lastNameAndOtherList[1];

                string[] abbreviatures = other.Split('.', StringSplitOptions.RemoveEmptyEntries);
                int abbreviaturesNum = abbreviatures.Count();

                if(abbreviaturesNum > 0)
                {
                    firstNameAbbr  = abbreviatures[0];                    
                }
                if (abbreviaturesNum > 1)
                {
                    patronymicAbbr = abbreviatures[1];
                }

                findedAppUsers = _context.Users.Where(u => u.LastName.ToLower().Contains(lastName)).ToList();

                if (!string.IsNullOrWhiteSpace(firstNameAbbr))
                    findedAppUsers = findedAppUsers.Where(u => u.FirstName.Contains(firstNameAbbr)).ToList();
                if (!string.IsNullOrWhiteSpace(patronymicAbbr))
                    findedAppUsers = findedAppUsers.Where(u => u.Patronymic.Contains(patronymicAbbr)).ToList();
            }
            else
            {
                findedAppUsers = _context.Users.Where(u => u.LastName == fio).ToList();
            }                       

            return new SelectList(findedAppUsers, "Id", "GetFullName");
        }

        /// <summary>
        /// Возвращает список авторов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAuthors(int selectedId = 0)
        {
            return new SelectList(_context.Author.Include(a=>a.AppUser).OrderBy(a=>a.AuthorName), "AuthorId", "AuthorName");
        }

        /// <summary>
        /// Возвращает список дисциплин,
        /// содержащих заданную строку
        /// </summary>
        /// <param name="disciplineNameSearchString"></param>
        /// <returns></returns>
        public SelectList GetSelectListDisciplineNames(string disciplineNameSearchString="")
        {
            if (String.IsNullOrWhiteSpace(disciplineNameSearchString)) return null;
            var disciplineNames = _context.DisciplineNames
                .Where(n => n.DisciplineNameName.ToLower().Contains(disciplineNameSearchString.ToLower()))
                .ToList();
            if (disciplineNames.Count == 0) return null;

            return new SelectList(disciplineNames, "DisciplineNameId", "DisciplineNameName");
        }

        /// <summary>
        /// Возвращает список дисциплин
        /// </summary>        
        public SelectList GetSelectListDisciplines(int selectedId = 0)
        {
            return new SelectList(_context.DisciplineNames.OrderBy(d=>d.DisciplineNameName), "DisciplineNameId", "DisciplineNameName", selectedId);
        }

        /// <summary>
        /// Возвращает список форм обучения
        /// </summary>
        /// <returns></returns>
        public SelectList GetSelectListEduForms(int selectedId = 0)
        {
            return new SelectList(_context.EduForms, "EduFormId", "EduFormName", selectedId);
        }

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых направлений подготовки
        /// </summary>
        /// <returns></returns>
        public SelectList GetSelectListEduNapravlFullNames(int selectedId = 0)
        {            
            return new SelectList(_context.EduNapravls.Include(n=>n.EduUgs.EduLevel), "EduNapravlId", "GetEduNapravlFullName", selectedId);                       
        }

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых профилей подготовки
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduProfileFullNames(int selectedId = 0)
        {
            return new SelectList(_context.EduProfiles.Include(n => n.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых профилей подготовки, доступных методкомиссиям
        /// </summary>
        /// <param name="metodKomissii"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduProfileFullNamesOfMethodicalCommission(IEnumerable<MetodKomissiya> metodKomissii, int selectedId = 0)
        {
            var eduProfiles = new List<EduProfile>();

            foreach (var metodKomissiya in metodKomissii)
            {
                foreach (var metodKomissiyaEduProfile in metodKomissiya.MetodKomissiyaEduProfiles)
                {
                    eduProfiles.Add(metodKomissiyaEduProfile.EduProfile);
                }
            }

            return new SelectList(eduProfiles, "EduProfileId", "GetEduProfileFullName", selectedId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAppUsers(int selectedId = 0)
        {
            return new SelectList(_context.Users, "Id", "GetFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список
        /// программ подготовки
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduProgramPodg(int selectedId)
        {
            return new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgName", selectedId);
        }

        /// <summary>
        /// Возвращает список сроков обучения
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduSrok(int selectedId = 0)
        {
            return new SelectList(_context.EduSrok, "EduSrokId", "EduSrokName", selectedId);
        }

        /// <summary>
        /// Возвращает список методкомиссий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListMetodKomissiyaNames(int selectedId = 0)
        {
            return new SelectList(_context.MetodKomissii, "MetodKomissiyaId", "MetodKomissiyaName", selectedId);
        }

        /// <summary>
        /// Возвращает список помещений
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListPomesheniya(int selectedId = 0)
        {
            return new SelectList(_context.Pomeshenie.OrderBy(p=>p.PomeshenieName), "PomeshenieId", "PomeshenieName", selectedId);
        }

        /// <summary>
        /// Возвращает список кафедр
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListStructKaf(int selectedId = 0)
        {
            return new SelectList(_context.StructKafs.Include(s=>s.StructSubvision), "StructKafId", "StructSubvision.StructSubvisionName", selectedId);
        }

        /// <summary>
        /// Возвращает список ФИО преподавателей
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListTeacherFio(int selectedId = 0)
        {
            return new SelectList(_context.Teachers.OrderBy(t=>t.TeacherFio), "TeacherId", "TeacherFio", selectedId);
        }

        /// <summary>
        /// Возвращает список форм издания
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListUchPosobieFormaIzdaniya(int selectedId = 0)
        {
            return new SelectList(_context.UchPosobieFormaIzdaniya.OrderBy(f => f.UchPosobieFormaIzdaniyaName),
                "UchPosobieFormaIzdaniyaId", "UchPosobieFormaIzdaniyaName", selectedId);
        }

        /// <summary>
        /// Возвращает список видов учебных пособий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListUchPosobieVid(int selectedId = 0)
        {
            return new SelectList(_context.UchPosobieVid.OrderBy(t => t.UchPosobieVidName),
                "UchPosobieVidId", "UchPosobieVidName", selectedId);
        }

        /// <summary>
        /// Возвращает список структурных подразделений
        /// </summary>
        /// <param name="structSubvisionId"></param>
        /// <returns></returns>
        public SelectList GetSelectListStructSubvisions(int? structSubvisionId = null)
        {
            return new SelectList(_context.StructSubvisions.OrderBy(t => t.StructSubvisionName),
                "StructSubvisionId", "StructSubvisionName", structSubvisionId);
        }
    }
}
