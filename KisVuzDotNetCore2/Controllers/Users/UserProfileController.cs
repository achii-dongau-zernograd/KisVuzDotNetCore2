using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Users;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Files;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize]
    public class UserProfileController:Controller
    {
        private AppIdentityDBContext context;
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private Task<AppUser> CurrentUser => userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        private IUserProfileRepository _userProfileRepository;
        private ISelectListRepository _selectListRepository;
        private IFileModelRepository _fileModelRepository;

        public UserProfileController(AppIdentityDBContext ctx,
            UserManager<AppUser> userMgr,
            RoleManager<IdentityRole> roleMgr,
            IUserProfileRepository userProfileRepository,
            ISelectListRepository selectListRepository,
            IFileModelRepository fileModelRepository)
        {
            context = ctx;
            userManager = userMgr;
            roleManager = roleMgr;
            _userProfileRepository = userProfileRepository;
            _selectListRepository = selectListRepository;
            _fileModelRepository = fileModelRepository;
        }

        #region Научная работа
        public IActionResult Articles()
        {
            List<Article> userArticles = _userProfileRepository.GetArticles(User.Identity.Name);
            return View(userArticles);
        }

        public IActionResult CreateOrEditArticle(int? id)
        {
            Article article = _userProfileRepository.GetArticle(id, User.Identity.Name);
            if (article == null) return NotFound();

            ViewBag.ScienceJournals = _selectListRepository.GetSelectListScienceJournals(article.ScienceJournalId);
            ViewBag.Years = _selectListRepository.GetSelectListYears(article.YearId);
            ViewBag.NirSpecials = _selectListRepository.GetSelectListNirSpecials();
            
            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditArticle(Article article, int NirSpecialIdAdd, int NirSpecialIdRemove, CreateOrEditNirDataModeEnum mode, IFormFile uploadFile)
        {
            if(uploadFile!=null)
            {
                FileModel f = await _fileModelRepository.UploadArticleAsync(article, uploadFile);                
            }                        

            Article articleEntry = _userProfileRepository.GetArticle(article.ArticleId, User.Identity.Name);
            if (articleEntry == null)
            {
                _userProfileRepository.CreateArticle(article, User.Identity.Name);
                articleEntry = article;
            }
            else
            {
                article.ArticleNirSpecials = articleEntry.ArticleNirSpecials;
                article.ArticleAuthors = articleEntry.ArticleAuthors;
                article.ArticleNirTemas = articleEntry.ArticleNirTemas;
                _userProfileRepository.UpdateArticle(articleEntry, article);
            }


            switch (mode)
            {
                case CreateOrEditNirDataModeEnum.Saving:
                    _userProfileRepository.UpdateArticle(articleEntry, article);
                    return RedirectToAction("Articles");                    
                case CreateOrEditNirDataModeEnum.AddingAuthor:
                    break;
                case CreateOrEditNirDataModeEnum.EditingAuthor:
                    break;
                case CreateOrEditNirDataModeEnum.RemovingAuthor:
                    break;
                case CreateOrEditNirDataModeEnum.AddingNirSpecial:
                    if(NirSpecialIdAdd!=0)
                    {
                        var isExists = article.ArticleNirSpecials.FirstOrDefault(s => s.NirSpecialId == NirSpecialIdAdd) != null;
                        if(!isExists)
                        {
                            article.ArticleNirSpecials.Add(new ArticleNirSpecial { NirSpecialId = NirSpecialIdAdd });
                            _userProfileRepository.UpdateArticle(articleEntry, article);
                        }                       
                    }
                    break;
                case CreateOrEditNirDataModeEnum.EditingNirSpecial:
                    break;
                case CreateOrEditNirDataModeEnum.RemovingNirSpecial:
                    if (NirSpecialIdRemove != 0)
                    {
                        var articleToRemove = article.ArticleNirSpecials.FirstOrDefault(s=>s.NirSpecialId == NirSpecialIdRemove);
                        if(articleToRemove!=null)
                        {
                            article.ArticleNirSpecials.Remove(articleToRemove);
                            _userProfileRepository.UpdateArticle(articleEntry, article);
                        }                        
                    }
                    break;
                default:
                    break;
            }
                        

            ViewBag.ScienceJournals = _selectListRepository.GetSelectListScienceJournals(article.ScienceJournalId);
            ViewBag.Years = _selectListRepository.GetSelectListYears(article.YearId);
            ViewBag.NirSpecials = _selectListRepository.GetSelectListNirSpecials();
            ViewBag.UploadedFile = uploadFile;
            return View(articleEntry);
        }              

        #endregion

        public async Task<IActionResult> Index(string id)
        {
            AppUser user;

            if (id==null)
            { 
                user = await CurrentUser;                
            }
            else
            {
                user = await userManager.FindByIdAsync(id);
                if (user == null)
                    return NotFound();
            }

            
            bool canEdit = false;// Флаг разрешения редактирования профиля
            if(HttpContext.User.Identity.Name != null)
            {
                AppUser currentUser = await CurrentUser;
                if (user.Id == currentUser.Id)
                {
                    canEdit = true;
                }
            }

            user.EduLevelGroup = await context.EduLevelGroups.Where(l => l.EduLevelGroupId == user.EduLevelGroupId).FirstOrDefaultAsync();
            user.Qualifications = await context.Qualifications.Where(q => q.AppUserId == user.Id && q.RowStatusId == (int)RowStatusEnum.Confirmed).ToListAsync();
            user.AcademicDegree = await context.AcademicDegrees.Where(d => d.AcademicDegreeId == user.AcademicDegreeId).FirstOrDefaultAsync();
            user.AcademicStat = await context.AcademicStats.Where(s => s.AcademicStatId == user.AcademicStatId).FirstOrDefaultAsync();
            user.RefresherCourses = await context.RefresherCourses.Where(c=>c.AppUserId == user.Id && c.RowStatusId==(int)RowStatusEnum.Confirmed).ToListAsync();
            user.ProfessionalRetrainings = await context.ProfessionalRetrainings.Where(c => c.AppUserId == user.Id && c.RowStatusId == (int)RowStatusEnum.Confirmed).ToListAsync();
            user.UserWorks = await context.UserWorks
                .Include(w=>w.FileModel)
                .Include(w=>w.UserWorkType)
                .Include(w=>w.UserWorkReviews)
                    .ThenInclude(w=>w.UserWorkReviewMark)
                .Where(w=>w.AppUserId==user.Id)
                .ToListAsync();

            user.Author = await context.Author
                .Include(a=>a.UchPosobieAuthors)
                    .ThenInclude(ua=>ua.UchPosobie)
                .Include(a => a.UchPosobieAuthors)
                    .ThenInclude(ua => ua.UchPosobie)
                        .ThenInclude(u => u.UchPosobieDisciplineNames)
                    .ThenInclude(ud => ud.DisciplineName)
                .Include(a => a.UchPosobieAuthors)
                    .ThenInclude(ua => ua.UchPosobie)
                        .ThenInclude(u => u.UchPosobieFormaIzdaniya)
                .Include(a => a.UchPosobieAuthors)
                    .ThenInclude(ua => ua.UchPosobie)
                        .ThenInclude(u => u.UchPosobieVid)
                .Include(a => a.UchPosobieAuthors)
                    .ThenInclude(ua => ua.UchPosobie)
                        .ThenInclude(u => u.EduForms)
                            .ThenInclude(ef => ef.EduForm)
                .Include(a => a.UchPosobieAuthors)
                    .ThenInclude(ua => ua.UchPosobie)
                            .ThenInclude(u => u.EduNapravls)
                                .ThenInclude(n => n.EduNapravl.EduUgs.EduLevel)
                .Include(a => a.UchPosobieAuthors)
                    .ThenInclude(ua => ua.UchPosobie)
                                .ThenInclude(u => u.FileModel)
                                    .ThenInclude(fm => fm.FileToFileTypes)
                                        .ThenInclude(ftft => ftft.FileDataType.FileDataTypeGroup)
                .Where(a => a.AppUserId == user.Id)
                .ToListAsync();

            var student= await context.Students
                .Include(s=>s.VedomostStudentMarks)
                    .ThenInclude(v=>v.VedomostStudentMarkName)
                .Include(s => s.VedomostStudentMarks)
                    .ThenInclude(v => v.Vedomost.SemestrName)
                .Include(s => s.VedomostStudentMarks)
                    .ThenInclude(v => v.Vedomost.EduYear)
                .Include(s => s.StudentGroup.EduForm)
                .Include(s => s.StudentGroup.EduKurs)
                .Include(s => s.StudentGroup.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(s => s.StudentGroup.StructFacultet.StructSubvision)                
                .SingleOrDefaultAsync(s=>s.AppUserId==user.Id);
            ViewData["student"] = student;

            user.Teachers = await context.Teachers
                .Include(t=>t.StudentGroupsOfKurator)
                    .ThenInclude(sg=>sg.EduKurs)
                .Include(t=>t.TeacherStructKafPostStavka)
                    .ThenInclude(tsps=>tsps.StructKaf.StructSubvision)
                .Include(t => t.TeacherStructKafPostStavka)
                    .ThenInclude(tsps => tsps.Post)
                .Where(t => t.AppUserId == user.Id)
                .ToListAsync();

            ViewBag.CanEdit = canEdit;
            return View(user);
        }
        
        public async Task<IActionResult> ChangeProfile()
        {
            AppUser user = await CurrentUser;
            UserProfileModificationModel profile = new UserProfileModificationModel();
            profile.AppUserPhoto = user.AppUserPhoto;
            profile.UserName = user.UserName;
            profile.Email = user.Email;
            profile.PhoneNumber = user.PhoneNumber;
            profile.LastName = user.LastName;
            profile.FirstName = user.FirstName;
            profile.Patronymic = user.Patronymic;
            profile.Birthdate = user.Birthdate;
            profile.EduLevelGroupId = user.EduLevelGroupId;
            profile.AcademicDegreeId = user.AcademicDegreeId;
            profile.AcademicStatId = user.AcademicStatId;
            profile.DateStartWorking = user.DateStartWorking;
            profile.DateStartWorkingSpec = user.DateStartWorkingSpec;

            ViewData["EduLevelGroups"] = new SelectList(context.EduLevelGroups, "EduLevelGroupId", "EduLevelGroupName", user.EduLevelGroupId);
            ViewData["AcademicDegrees"] = new SelectList(context.AcademicDegrees, "AcademicDegreeId", "AcademicDegreeName", user.AcademicDegreeId);
            ViewData["AcademicStats"] = new SelectList(context.AcademicStats, "AcademicStatId", "AcademicStatName", user.AcademicStatId);

            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfile(UserProfileModificationModel user)
        {
            if(ModelState.IsValid)
            {
                AppUser changingUser = await CurrentUser;
                //changingUser.UserName    = user.UserName;
                //changingUser.Email       = user.Email;
                changingUser.PhoneNumber = user.PhoneNumber;
                changingUser.LastName    = user.LastName;
                changingUser.FirstName   = user.FirstName;
                changingUser.Patronymic  = user.Patronymic;
                changingUser.Birthdate   = user.Birthdate;
                changingUser.EduLevelGroupId = user.EduLevelGroupId;
                changingUser.AcademicDegreeId = user.AcademicDegreeId;
                changingUser.AcademicStatId = user.AcademicStatId;
                changingUser.DateStartWorking = user.DateStartWorking;
                changingUser.DateStartWorkingSpec = user.DateStartWorkingSpec;

                if (user.AppUserPhotoFile != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(user.AppUserPhotoFile.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)user.AppUserPhotoFile.Length);
                    }                                        
                    changingUser.AppUserPhoto = imageData;                    
                }

                await userManager.UpdateAsync(changingUser);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        
        [HttpPost]
        public async Task<IActionResult> AppUserFotoUpload(IFormFile AppUserPhoto)
        {
            if (AppUserPhoto != null)
            {
                byte[] imageData = null;
                
                using (var binaryReader = new BinaryReader(AppUserPhoto.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)AppUserPhoto.Length);
                }
                
                AppUser changingUser = await CurrentUser;
                changingUser.AppUserPhoto = imageData;
                await userManager.UpdateAsync(changingUser);                                
            }
            return RedirectToAction("Index");
        }
    }
}
