using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Репозиторий патентов (свидетельств)
    /// </summary>
    public class PatentRepository : IPatentRepository
    {
        AppIdentityDBContext _context;
        IFileModelRepository _fileModelRepository;

        public PatentRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Возвращает патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Patent GetPatent(int? id)
        {
            Patent patent = new Patent();

            if (id == null)
            {
                return patent;
            }

            List<Patent> userPatents = GetPatents();
            patent = userPatents.SingleOrDefault(p => p.PatentId == id);
            return patent;
        }

        /// <summary>
        /// Возвращает список всех патентов (свидетельств)
        /// </summary>        
        /// <returns></returns>
        public List<Patent> GetPatents()
        {
            List<Patent> patents = _context.Patents
                .Include(p => p.PatentNirSpecials)
                    .ThenInclude(ns => ns.NirSpecial)
                        .ThenInclude(np => np.NirSpecialEduProfiles)
                            .ThenInclude(ep => ep.EduProfile)
                                .ThenInclude(en => en.EduNapravl)
                                    .ThenInclude(eu => eu.EduUgs)
                                        .ThenInclude(el => el.EduLevel)
                .Include(p => p.PatentAuthors)
                    .ThenInclude(pa => pa.Author)
                .Include(p => p.PatentVid)
                    .ThenInclude(pv => pv.PatentVidGroup)
                .Include(p => p.PatentNirTemas)
                    .ThenInclude(n => n.NirTema)
                        .ThenInclude(ne => ne.NirTemaEduProfileList)
                            .ThenInclude(ep => ep.EduProfile)
                                .ThenInclude(en => en.EduNapravl)
                                    .ThenInclude(eu => eu.EduUgs)
                                        .ThenInclude(el => el.EduLevel)
                .Include(p => p.Year)
                .Include(p => p.FileModel)
                .Include(p => p.RowStatus)
                .OrderByDescending(p=>p.Year.YearName)
                .ToList();

            return patents;
        }

        /// <summary>
        /// Подтверждение патента (свидетельства)
        /// </summary>
        /// <param name="patentId"></param>
        /// <returns></returns>        
        public void ConfirmPatent(int patentId)
        {
            var patent = _context.Patents.SingleOrDefault(p => p.PatentId == patentId);
            patent.RowStatusId = (int)RowStatusEnum.Confirmed;
            _context.SaveChanges();         
        }

        /// <summary>
        /// Добавляет патент (свидетельство) по УИД
        /// </summary>
        /// <param name="patent"></param>        
        public void AddPatent(Patent patent)
        {
            if (patent.PatentId != 0) return;

            _context.Patents.Add(patent);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет патент (свидетельство) по УИД
        /// </summary>
        /// <param name="patentId"></param>        
        /// <returns></returns>
        public Patent RemovePatent(int patentId)
        {
            var patent = GetPatent(patentId);
            if (patent == null) return null;

            _context.Patents.Remove(patent);
            if (patent.FileModelId != null)
            {
                _fileModelRepository.RemoveFileModelAsync(patent.FileModel);
            }
            _context.SaveChanges();

            return patent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patentEntry"></param>
        /// <param name="patent"></param>
        public void UpdatePatent(Patent patentEntry, Patent patent)
        {
            patentEntry.PatentNumber = patent.PatentNumber;
            patentEntry.PatentName = patent.PatentName;
            patentEntry.PatentVidId = patent.PatentVidId;
            patentEntry.FileModelId = patent.FileModelId;
            patentEntry.YearId = patent.YearId;
            patentEntry.PatentOwner = patent.PatentOwner;
            patentEntry.IsACHII = patent.IsACHII;
            patentEntry.IsZarubejn = patent.IsZarubejn;
            patentEntry.RowStatusId = patent.RowStatusId;
            
            if (patent.PatentNirSpecials != null && patent.PatentNirSpecials.Count > 0)
            {
                foreach (var patentNirSpecial in patent.PatentNirSpecials)
                {
                    bool isExists = false;
                    foreach (var patentNirSpecialEntry in patentEntry.PatentNirSpecials)
                    {
                        if (patentNirSpecialEntry.NirSpecialId == patentNirSpecial.NirSpecialId)
                        {
                            isExists = true;
                        }
                    }
                    if (!isExists)
                    {
                        patentEntry.PatentNirSpecials.Add(patentNirSpecial);
                    }
                }
            }

            if (patent.PatentAuthors != null && patent.PatentAuthors.Count > 0)
            {
                foreach (var patentAuthor in patent.PatentAuthors)
                {
                    bool isExists = false;
                    foreach (var patentAuthorEntry in patentEntry.PatentAuthors)
                    {
                        if (patentAuthorEntry.PatentAuthorId == patentAuthor.PatentAuthorId)
                        {
                            isExists = true;
                        }
                    }
                    if (!isExists)
                    {
                        patentEntry.PatentAuthors.Add(patentAuthor);
                    }
                }

                decimal firstAuthorPart = 1;
                for (int i = 1; i < patentEntry.PatentAuthors.Count; i++)
                {
                    firstAuthorPart -= patentEntry.PatentAuthors[i].AuthorPart;
                }

                if (firstAuthorPart < 0) firstAuthorPart = 0;
                patentEntry.PatentAuthors[0].AuthorPart = firstAuthorPart;
            }

            _context.SaveChanges();
        }

    }
}