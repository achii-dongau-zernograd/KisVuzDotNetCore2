using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.UchPosobiya;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    /// <summary>
    /// Репозиторий учебных пособий
    /// </summary>
    public class UchPosobiyaRepository : IUchPosobiyaRepository
    {
        AppIdentityDBContext _context;
        IFileModelRepository _fileModelRepository;

        public UchPosobiyaRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Запрос к БД с поиском всех уч. пособий со всеми связанными данными
        /// </summary>
        IIncludableQueryable<UchPosobie, FileDataTypeGroup> UchPosobiya
        {
            get
            {
                return _context.UchPosobie
                .Include(u => u.UchPosobieAuthors)
                    .ThenInclude(ua => ua.Author)
                        .ThenInclude(a => a.AppUser)
                .Include(u => u.UchPosobieDisciplineNames)
                    .ThenInclude(ud => ud.DisciplineName)
                .Include(u => u.UchPosobieFormaIzdaniya)
                .Include(u => u.UchPosobieVid)
                .Include(u => u.EduForms)
                    .ThenInclude(ef => ef.EduForm)
                .Include(u => u.EduNapravls)
                    .ThenInclude(n => n.EduNapravl.EduUgs.EduLevel)
                .Include(u => u.FileModel)
                    .ThenInclude(fm => fm.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileDataType.FileDataTypeGroup);
            }
        }                       

        /// <summary>
        /// Возвращает список учебных пособий
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UchPosobie>> GetUchPosobiyaAsync(UchPosobieFilterModel uchPosobieFilterModel)
        {
            if(uchPosobieFilterModel == null)
                return UchPosobiya;
                        
            if (uchPosobieFilterModel.GodIzdaniya == null)
            {
                return UchPosobiya;
            }
            var filteredData = UchPosobiya.Where(u => u.GodIzdaniya == uchPosobieFilterModel.GodIzdaniya);

            return await filteredData.ToListAsync();
        }

        /// <summary>
        /// Возвращает учебное пособие по переданному УИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UchPosobie> GetUchPosobieByIdAsync(int id)
        {
            if (id == 0)
            {
                return new UchPosobie();
            }
            
            return await UchPosobiya.SingleOrDefaultAsync(p => p.UchPosobieId == id);
        }

        /// <summary>
        /// Обновляет учебное пособие
        /// </summary>
        /// <param name="uchPosobieEditViewModel"></param>
        /// <returns></returns>
        public async Task UpdateUchPosobie(UchPosobieEditViewModel uchPosobieEditViewModel)
        {
            if (uchPosobieEditViewModel == null) return;
            
            if(uchPosobieEditViewModel.FormFile!=null)
            { 
                FileDataTypeEnum fileDataTypeEnum = await GetFileDataTypeEnumByUchPosobieVidId(uchPosobieEditViewModel.UchPosobie.UchPosobieVidId);
                var fileModel = await _fileModelRepository.UploadUchPosobieAsync(uchPosobieEditViewModel.FormFile, fileDataTypeEnum);
                if(fileModel!=null)//Если новый файл успешно загружен
                {
                    if (uchPosobieEditViewModel.UchPosobie.FileModelId != 0)// и при этом ранее уже был загружен другой файл
                    {
                        await _fileModelRepository.RemoveFileAsync(uchPosobieEditViewModel.UchPosobie.FileModelId);// Удаляем старый файл                        
                    }
                }

                uchPosobieEditViewModel.UchPosobie.FileModelId = fileModel.Id;
            }

            _context.UchPosobie.Update(uchPosobieEditViewModel.UchPosobie);
            try
            {
                // Attempt to save changes to the database
                await _context.SaveChangesAsync();                
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is UchPosobie)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            // TODO: decide which value should be written to database
                            proposedValues[property] = proposedValue;
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name);
                    }
                }
            }

        }

        /// <summary>
        /// Возвращает объект типа данных
        /// по переданному идентификатору
        /// вида учебного издания
        /// </summary>
        /// <param name="uchPosobieVidId"></param>
        /// <returns></returns>
        private async Task<FileDataTypeEnum> GetFileDataTypeEnumByUchPosobieVidId(int uchPosobieVidId)
        {
            var uchPosobieVid = await _context.UchPosobieVid.SingleOrDefaultAsync(v=>v.UchPosobieVidId == uchPosobieVidId);
            switch(uchPosobieVid.UchPosobieVidName)
            {
                case "Учебное пособие":
                    return FileDataTypeEnum.UchebnoePosobie;
                case "Курс лекций":
                    return FileDataTypeEnum.KursLekcii;
                case "Лабораторный практикум":
                    return FileDataTypeEnum.LaboratorniiPraktikum;
                case "Методические указания":
                    return FileDataTypeEnum.MetodicheskieUkazaniya;
                default:
                    return FileDataTypeEnum.UchebnoePosobie;
            }
        }

        /// <summary>
        /// Удаляет учебное пособие
        /// </summary>
        /// <param name="uchPosobie"></param>
        /// <returns></returns>
        public async Task RemoveUchPosobieAsync(UchPosobie uchPosobie)
        {
            if (uchPosobie == null) return;

            //if(uchPosobie.EduForms!=null)
            //{
            //    _context.RemoveRange(uchPosobie.EduForms);
            //}
            _context.Remove(uchPosobie);
            await _context.SaveChangesAsync();

            if (uchPosobie.FileModel != null)
            {
                await _fileModelRepository.RemoveFileModelAsync(uchPosobie.FileModel);
                await _context.SaveChangesAsync();
            }
                
        }

        /// <summary>
        /// Обновляет сведения об авторе учебного пособия
        /// </summary>
        /// <param name="uchPosobieAuthor"></param>
        public async Task UpdateUchPosobieAuthorAsync(UchPosobieAuthor uchPosobieAuthor)
        {
            if (uchPosobieAuthor == null) return;

            // Исключаем дублирование привязки
            if(uchPosobieAuthor.UchPosobieAuthorId == 0)
            {
                var existsObjects = _context.UchPosobieAuthor
                    .Where(ua => ua.AuthorId == uchPosobieAuthor.AuthorId && ua.UchPosobieId == uchPosobieAuthor.UchPosobieId).Any();
                if (existsObjects) return;
            }

            _context.UchPosobieAuthor.Update(uchPosobieAuthor);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает объект привязки автора к учебному пособию по УИД
        /// </summary>
        /// <param name="uchPosobieAuthorId"></param>
        /// <returns></returns>
        public async Task<UchPosobieAuthor> GetUchPosobieAuthorByIdAsync(int uchPosobieAuthorId)
        {
            return await _context.UchPosobieAuthor
                .Include(a => a.Author)
                .Include(a => a.UchPosobie)
                .SingleOrDefaultAsync(ua => ua.UchPosobieAuthorId == uchPosobieAuthorId);
        }

        /// <summary>
        /// Назначает пользователя автором
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        public async Task<Author> CreateAuthorByAppUserIdAsync(string appUserId)
        {
            if (string.IsNullOrWhiteSpace(appUserId)) return null;
            var appUser = await _context.Users
                .Include(u=>u.Author)
                .SingleOrDefaultAsync(u=>u.Id == appUserId);
            if (appUser == null) return null;

            // Если пользователь уже является автором, возвращаем объект Author
            if(appUser.Author.Count!=0)
            {
                return appUser.Author.FirstOrDefault();
            }

            // Иначе назначаем пользователя автором 
            var newAuthor = new Author { AppUserId = appUserId, AuthorName = appUser.GetFullName };
            var addedAuthor = await _context.Author.AddAsync(newAuthor);

            await _context.SaveChangesAsync();
            return addedAuthor.Entity;
        }

        /// <summary>
        /// Удаляет объект привязки автора к учебному пособию по УИД
        /// </summary>
        /// <param name="uchPosobieAuthorId"></param>
        /// <returns></returns>
        public async Task RemoveUchPosobieAuthorByIdAsync(int uchPosobieAuthorId)
        {
            if (uchPosobieAuthorId == 0) return;

            var uchPosobieAuthor = await GetUchPosobieAuthorByIdAsync(uchPosobieAuthorId);
            if (uchPosobieAuthor == null) return;

            _context.UchPosobieAuthor.Remove(uchPosobieAuthor);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет объект привязки автора к учебному пособию
        /// </summary>
        /// <param name="uchPosobieDisciplineName"></param>
        /// <returns></returns>
        public async Task UpdateUchPosobieDisciplineNameAsync(UchPosobieDisciplineName uchPosobieDisciplineName)
        {
            if (uchPosobieDisciplineName == null) return;

            bool isExists = await _context.UchPosobieDisciplineName
                .Where(ud => ud.DisciplineNameId == uchPosobieDisciplineName.DisciplineNameId
                && ud.UchPosobieId == uchPosobieDisciplineName.UchPosobieId).AnyAsync();
            if (!isExists)
            {
                _context.UchPosobieDisciplineName.Update(uchPosobieDisciplineName);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Возвращает привязку "Учебное пособие - Дисциплина" по УИД
        /// </summary>
        /// <param name="uchPosobieDisciplineNameId"></param>
        /// <returns></returns>
        public async Task<UchPosobieDisciplineName> GetUchPosobieDisciplineNameByIdAsync(int uchPosobieDisciplineNameId)
        {
            if (uchPosobieDisciplineNameId == 0) return null;
            return await _context.UchPosobieDisciplineName
                .Include(ud=>ud.DisciplineName)
                .Include(ud=>ud.UchPosobie)
                .SingleOrDefaultAsync(ud => ud.UchPosobieDisciplineNameId == uchPosobieDisciplineNameId);
        }

        /// <summary>
        /// Удаление привязки "Учебное пособие - Дисциплина"
        /// </summary>
        /// <param name="uchPosobieDisciplineName"></param>
        /// <returns></returns>
        public async Task RemoveUchPosobieDisciplineNameAsync(UchPosobieDisciplineName uchPosobieDisciplineName)
        {
            if (uchPosobieDisciplineName == null) return;
            _context.UchPosobieDisciplineName.Remove(uchPosobieDisciplineName);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновление привязки "Учебное пособие - Направление"
        /// </summary>
        /// <param name="uchPosobieEduNapravl"></param>
        /// <returns></returns>
        public async Task UpdateUchPosobieEduNapravlAsync(UchPosobieEduNapravl uchPosobieEduNapravl)
        {
            if (uchPosobieEduNapravl == null) return;

            bool isExists = await _context.UchPosobieEduNapravl
                .Where(un => un.EduNapravlId == uchPosobieEduNapravl.EduNapravlId
                && un.UchPosobieId == uchPosobieEduNapravl.UchPosobieId).AnyAsync();
            if (!isExists)
            {
                _context.UchPosobieEduNapravl.Update(uchPosobieEduNapravl);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Возвращает привязку "Учебное пособие - Направление" по УИД
        /// </summary>
        /// <param name="uchPosobieEduNapravlId"></param>
        /// <returns></returns>
        public async Task<UchPosobieEduNapravl> GetUchPosobieEduNapravlByIdAsync(int uchPosobieEduNapravlId)
        {
            if (uchPosobieEduNapravlId == 0) return null;
            return await _context.UchPosobieEduNapravl
                .Include(un => un.EduNapravl)
                .Include(un => un.UchPosobie)
                .SingleOrDefaultAsync(un => un.UchPosobieEduNapravlId == uchPosobieEduNapravlId);
        }

        /// <summary>
        /// Удаляет привязку "Учебное пособие - Направление"
        /// </summary>
        /// <param name="uchPosobieEduNapravl"></param>
        /// <returns></returns>
        public async Task RemoveUchPosobieEduNapravlAsync(UchPosobieEduNapravl uchPosobieEduNapravl)
        {
            if (uchPosobieEduNapravl == null) return;
            _context.UchPosobieEduNapravl.Remove(uchPosobieEduNapravl);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список форм обучения
        /// </summary>
        /// <returns></returns>
        public List<EduForm> GetEduForms()
        {
            return _context.EduForms.ToList();
        }

        /// <summary>
        /// Обновляет привязку "Учебное пособие - Форма обучения"
        /// </summary>
        /// <param name="uchPosobieEduForm"></param>
        /// <returns></returns>
        public async Task UpdateUchPosobieEduFormAsync(UchPosobieEduForm uchPosobieEduForm)
        {
            if (uchPosobieEduForm == null) return;

            bool isExists = await _context.UchPosobieEduForm
                .Where(uf => uf.EduFormId == uchPosobieEduForm.EduFormId
                && uf.UchPosobieId == uchPosobieEduForm.UchPosobieId).AnyAsync();
            if (!isExists)
            {
                _context.UchPosobieEduForm.Update(uchPosobieEduForm);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Возвращает привязку "Учебное пособие - Форма обучения" по УИД
        /// </summary>
        /// <param name="uchPosobieEduFormId"></param>
        /// <returns></returns>
        public async Task<UchPosobieEduForm> GetUchPosobieEduFormByIdAsync(int uchPosobieEduFormId)
        {
            if (uchPosobieEduFormId == 0) return null;
            return await _context.UchPosobieEduForm
                .Include(uf => uf.EduForm)
                .Include(uf => uf.UchPosobie)
                .SingleOrDefaultAsync(un => un.UchPosobieEduFormId == uchPosobieEduFormId);
        }

        /// <summary>
        /// Удаляет привязку "Учебное пособие - Форма обучения"
        /// </summary>
        /// <param name="uchPosobieEduForm"></param>
        /// <returns></returns>
        public async Task RemoveUchPosobieEduFormAsync(UchPosobieEduForm uchPosobieEduForm)
        {
            if (uchPosobieEduForm == null) return;
            _context.UchPosobieEduForm.Remove(uchPosobieEduForm);
            await _context.SaveChangesAsync();
        }
    }
}
