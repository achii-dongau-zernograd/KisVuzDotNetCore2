using KisVuzDotNetCore2.Models.Files;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KisVuzDotNetCore2.Models.Nir
{

    /// <summary>
    /// Репозиторий монографий
    /// </summary>
    public class MonografRepository : IMonografRepository
    {
        private readonly AppIdentityDBContext _context;

        IFileModelRepository _fileModelRepository;

        public MonografRepository(AppIdentityDBContext context, IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }
        
        /// <summary>
        /// Возвращает монографию пользователя userName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>

        public Monograf GetMonograf(int? id)
        {
            Monograf monograf = new Monograf();
            if (id == null)
            {
                return monograf;
            }

            List<Monograf> monografs = GetMonografs();
            monograf = monografs.SingleOrDefault(m => m.MonografId == id);
            return monograf;
        }

        /// <summary>
        /// Возвращает список всех монографий
        /// </summary>        
        /// <returns></returns>

        public List<Monograf> GetMonografs()
        {
            List<Monograf> monografs = _context.Monografs
                .Include(m => m.MonografNirSpecials)
                   .ThenInclude(ns => ns.NirSpecial)
                        .ThenInclude(np => np.NirSpecialEduProfiles)
                            .ThenInclude(ep => ep.EduProfile)
                                .ThenInclude(en => en.EduNapravl)
                                    .ThenInclude(eu => eu.EduUgs)
                                        .ThenInclude(el => el.EduLevel)
                .Include (m => m.MonografAuthors)
                    .ThenInclude(ma=> ma.Author)
                    .Include(m => m.MonografNirTemas)
                       .ThenInclude(n => n.NirTema)
                        .ThenInclude(ne => ne.NirTemaEduProfileList)
                            .ThenInclude(ep => ep.EduProfile)
                                .ThenInclude(en => en.EduNapravl)
                                    .ThenInclude(eu => eu.EduUgs)
                                        .ThenInclude(el => el.EduLevel)
                .Include(p => p.Year)
                .Include(p => p.FileModel)
                .Include(p => p.RowStatus)
                .OrderByDescending(m=>m.Year.YearName)
                .ToList();

            return monografs;
        }

        /// <summary>
        /// Добавляет монографию
        /// </summary>        
        /// <returns></returns>
        public void AddMonograf(Monograf monograf)
        {
            _context.Monografs.Add(monograf);
            _context.SaveChanges();
        }
        
        /// <summary>
        /// Удаляет монографию
        /// </summary>
        /// <param name="monografId"></param>
        public void RemoveMonografAsync(int monografId)
        {
            var monograf = GetMonograf(monografId);
            if (monograf == null) return;
            _context.Monografs.Remove(monograf);
            if (monograf.FileModel != null)
            {
                _fileModelRepository.RemoveFileModelAsync(monograf.FileModel);
            }
            _context.SaveChanges();
           
        }
        

        /// <summary>
        /// Обновляет монографию 
        /// </summary>
        /// <param name="monografEntry"></param>
        /// <param name="monograf"></param>
        public void UpdateMonograf(Monograf monografEntry, Monograf monograf)
        {
            monografEntry.MonografName= monograf.MonografName;
            monografEntry.FileModelId = monograf.FileModelId;
            monografEntry.YearId = monograf.YearId;
            monografEntry.RowStatusId = monograf.RowStatusId;
            monografEntry.IsACHII = monograf.IsACHII;            
            monografEntry.MonografNirTemas = monograf.MonografNirTemas;

            ////////
            /// + списки направлений, авторов и пр.
            if (monograf.MonografNirSpecials != null && monograf.MonografNirSpecials.Count > 0)
            {
                foreach (var monografNirSpecial in monograf.MonografNirSpecials)
                {
                    bool isExists = false;
                    foreach (var monografNirSpecialEntry in monografEntry.MonografNirSpecials)
                    {
                        if (monografNirSpecialEntry.NirSpecialId == monografNirSpecial.NirSpecialId)
                        {
                            isExists = true;
                        }
                    }
                    if (!isExists)
                    {
                        monografEntry.MonografNirSpecials.Add(monografNirSpecial);
                    }
                }
            }

            if (monograf.MonografAuthors != null && monograf.MonografAuthors.Count > 0)
            {
                foreach (var monografAuthor in monograf.MonografAuthors)
                {
                    bool isExists = false;
                    foreach (var monografAuthorEntry in monografEntry.MonografAuthors)
                    {
                        if (monografAuthorEntry.MonografAuthorId == monografAuthor.MonografAuthorId)
                        {
                            isExists = true;
                        }
                    }
                    if (!isExists)
                    {
                        monografEntry.MonografAuthors.Add(monografAuthor);
                    }
                }

                decimal firstAuthorPart = 1;
                for (int i = 1; i < monografEntry.MonografAuthors.Count; i++)
                {
                    firstAuthorPart -= monografEntry.MonografAuthors[i].AuthorPart;
                }

                if (firstAuthorPart < 0) firstAuthorPart = 0;
                monografEntry.MonografAuthors[0].AuthorPart = firstAuthorPart;
            }

            _context.SaveChanges();
        }
    }
}