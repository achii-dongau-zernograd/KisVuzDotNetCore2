using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Репозиторий электронных журналов
    /// </summary>
    public class ElGradebookRepository : IElGradebookRepository
    {
        AppIdentityDBContext _context;
        

        public ElGradebookRepository(AppIdentityDBContext context)
        {
            _context = context;            
        }

        /// <summary>
        /// Возвращает запрос на список журналов согласно параметрам фильтра
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <returns></returns>
        public IQueryable<ElGradebook> GetElGradebooks(ElGradebooksFilterAndSortModel filterAndSortModel)
        {
            IQueryable<ElGradebook> query = _context.ElGradebooks;

            if (filterAndSortModel != null)
            {
                if (!string.IsNullOrWhiteSpace(filterAndSortModel.FilterEduYear))
                {
                    query = query.Where(g => g.EduYear == filterAndSortModel.FilterEduYear);
                }

                if (!string.IsNullOrWhiteSpace(filterAndSortModel.FilterFaculty))
                {
                    filterAndSortModel.FilterFaculty = filterAndSortModel.FilterFaculty.Trim();
                    query = query.Where(g => g.Faculty.ToLower().Contains(filterAndSortModel.FilterFaculty.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(filterAndSortModel.FilterDepartment))
                {
                    filterAndSortModel.FilterDepartment = filterAndSortModel.FilterDepartment.Trim();
                    query = query.Where(g => g.Department.ToLower().Contains(filterAndSortModel.FilterDepartment.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(filterAndSortModel.FilterDisciplineName))
                {
                    filterAndSortModel.FilterDisciplineName = filterAndSortModel.FilterDisciplineName.Trim();
                    query = query.Where(g => g.DisciplineName.ToLower().Contains(filterAndSortModel.FilterDisciplineName.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(filterAndSortModel.FilterGroupName))
                {
                    filterAndSortModel.FilterGroupName = filterAndSortModel.FilterGroupName.Trim();
                    query = query.Where(g => g.GroupName.ToLower().Contains(filterAndSortModel.FilterGroupName.ToLower()));
                }

                if (filterAndSortModel.FilterCourse > 0 && filterAndSortModel.FilterCourse <= 6)
                {
                    query = query.Where(g => g.Course == filterAndSortModel.FilterCourse);
                }
                else
                {
                    filterAndSortModel.FilterCourse = 0;
                }

                if (filterAndSortModel.FilterSemesterNumber > 0 && filterAndSortModel.FilterSemesterNumber <= 12)
                {
                    query = query.Where(g => g.SemesterNumber == filterAndSortModel.FilterSemesterNumber);
                }
                else
                {
                    filterAndSortModel.FilterSemesterNumber = 0;
                }
            }
            
            return query;
        }

        /// <summary>
        /// Возвращает список журналов пользователя согласно параметрам фильтра
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<ElGradebook> GetElGradebooks(ElGradebooksFilterAndSortModel filterAndSortModel, string userName)
        {
            if (userName == null)
                return null;

            var query = GetElGradebooks(filterAndSortModel);

            return query;
        }
    }
}
