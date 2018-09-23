﻿using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Репозиторий методкомиссий
    /// </summary>
    public class MetodKomissiyaRepository : IMetodKomissiyaRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public MetodKomissiyaRepository(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment=appEnvironment;
    }

        /// <summary>
        /// Возвращает образовательную программу по УИД,
        /// если она доступна пользователю
        /// </summary>
        /// <param name="eduProgramId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<EduProgram> GetEduProgramByUserNameAsync(int eduProgramId, string userName)
        {
            var eduPrograms = await GetEduProgramsByUserNameAsync(userName);
            if (eduPrograms.Count() == 0) return null;

            var eduProgram = eduPrograms.SingleOrDefault(p => p.EduProgramId == eduProgramId);
            return eduProgram;
        }

        /// <summary>
        /// Возвращает заполненный объект пользователя по переданному имени
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<AppUser> GetAppUserAsync(string userName)
        {
            // Поиск аккаунта пользователя
            var appUser = await _context.Users
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                                        .ThenInclude(ep => ep.EduProgramPodg)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                                        .ThenInclude(ep => ep.EduProgramEduForms)
                                            .ThenInclude(epef=>epef.EduForm)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                                        .ThenInclude(ep => ep.EduProgramEduYears)
                                            .ThenInclude(epey => epey.EduYear)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                                        .ThenInclude(ep => ep.FileModel)                                            
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Where(u => u.UserName == userName)
                .SingleOrDefaultAsync();

            return appUser;
        }


        /// <summary>
        /// Возвращает набор образовательных программ,
        /// к которым имеет доступ пользователь
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EduProgram>> GetEduProgramsByUserNameAsync(string userName)
        {
            // Поиск аккаунта пользователя
            var appUser = await GetAppUserAsync(userName);

            var eduPrograms = new List<EduProgram>();

            foreach (var teacher in appUser.Teachers)
            {
                foreach (var teacherMetodKomissiya in teacher.TeacherMetodKomissii)
                {
                    foreach (var metodKomissiyaEduProfiles in teacherMetodKomissiya.MetodKomissiya.MetodKomissiyaEduProfiles)
                    {
                        foreach (var eduProgram in metodKomissiyaEduProfiles.EduProfile.EduPrograms)
                        {
                            eduPrograms.Add(eduProgram);
                        }
                    }
                }
            }

            return eduPrograms;
        }

        /// <summary>
        /// Определяет, является ли аутентифицированный
        /// пользователь членом методкомиссии
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsMetodKomissiyaMember(string userName)
        {
            // Поиск аккаунта пользователя
            var appUser = _context.Users
                .Include(u=>u.Teachers)
                    .ThenInclude(t=>t.TeacherMetodKomissii)
                .Where(u => u.UserName == userName)
                .SingleOrDefault();
            if (appUser == null) return false;

            foreach (var teacher in appUser.Teachers)
            {
                if(teacher.TeacherMetodKomissii.Any())
                {
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Обновляет образовательную программу, если она доступна пользователю
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="eduProgram"></param>
        /// <param name="uploadedFile"></param>
        /// <param name="eduFormIds"></param>
        /// <param name="eduYearIds"></param>
        public async Task UpdateEduProgramByUserNameAsync(string userName, EduProgram eduProgram, IFormFile uploadedFile, int[] eduFormIds, int[] eduYearIds)
        {
            ///////
            try
            {
                if (uploadedFile != null)
                {
                    FileModel fileModel = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, "Образовательная программа", FileDataTypeEnum.OPOP);
                    await _context.SaveChangesAsync();
                    int? fileToRemoveId = eduProgram.FileModelId;
                    eduProgram.FileModelId = fileModel.Id;
                    await _context.SaveChangesAsync();
                    KisVuzDotNetCore2.Models.Files.Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                }

                _context.Update(eduProgram);
                await _context.SaveChangesAsync();

                if (eduFormIds != null)
                {
                    _context.EduProgramEduForms.RemoveRange(_context.EduProgramEduForms.Where(f => f.EduProgramId == eduProgram.EduProgramId));
                    await _context.SaveChangesAsync();

                    var eduProgramEduFormList = new List<EduProgramEduForm>();
                    foreach (int eduFormId in eduFormIds)
                    {
                        EduProgramEduForm eduProgramEduForm = new EduProgramEduForm
                        {
                            EduProgramId = eduProgram.EduProgramId,
                            EduFormId = eduFormId
                        };
                        eduProgramEduFormList.Add(eduProgramEduForm);
                    }
                    await _context.EduProgramEduForms.AddRangeAsync(eduProgramEduFormList);
                    await _context.SaveChangesAsync();
                }

                if (eduYearIds != null)
                {
                    _context.EduProgramEduYears.RemoveRange(_context.EduProgramEduYears.Where(f => f.EduProgramId == eduProgram.EduProgramId));
                    await _context.SaveChangesAsync();

                    var eduProgramEduYearList = new List<EduProgramEduYear>();
                    foreach (int eduYearId in eduYearIds)
                    {
                        EduProgramEduYear eduProgramEduYear = new EduProgramEduYear
                        {
                            EduProgramId = eduProgram.EduProgramId,
                            EduYearId = eduYearId
                        };
                        eduProgramEduYearList.Add(eduProgramEduYear);
                    }
                    await _context.EduProgramEduYears.AddRangeAsync(eduProgramEduYearList);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            ///////
        }

        /// <summary>
        /// Возвращает перечисление методкомиссий,
        /// в которые входит пользователь
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MetodKomissiya>> GetMetodKomissiiByUserNameAsync(string userName)
        {
            var appUser = await GetAppUserAsync(userName);
            var metodKomissii = new List<MetodKomissiya>();
            appUser.Teachers
                .ForEach(t=>t.TeacherMetodKomissii
                    .ForEach(tm => metodKomissii.Add(tm.MetodKomissiya) ));

            return metodKomissii;
        }

        /// <summary>
        /// Удаляет образовательную программу,
        /// если она доступна пользователю
        /// </summary>
        /// <param name="eduProgram"></param>
        /// <param name="userName"></param>
        public async Task RemoveEduProgramByUserNameAsync(EduProgram eduProgram, string userName)
        {
            var p = await GetEduProgramByUserNameAsync(eduProgram.EduProgramId, userName);

            if(p!=null && p.Equals(eduProgram))
            {
                _context.EduPrograms.Remove(eduProgram);
                Files.Files.RemoveFile(_context, _appEnvironment, eduProgram?.FileModelId);
                await _context.SaveChangesAsync();
            }            
        }
    }
}
