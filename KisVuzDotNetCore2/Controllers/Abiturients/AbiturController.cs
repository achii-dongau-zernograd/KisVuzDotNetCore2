using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Priem;
using KisVuzDotNetCore2.Models.Struct;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;
using Spire.Pdf;
using Spire.Pdf.Graphics;

namespace KisVuzDotNetCore2.Controllers
{
    public class AbiturController : Controller
    {
        string searchTemplate = "";

        List<priemKolMest> priemKolMest = new List<priemKolMest>();
        List<PriemExam> PriemExam = new List<PriemExam>();
        List<priemKolTarget> priemKolTarget = new List<priemKolTarget>();
        List<BlankNum> blankNum = new List<BlankNum>();

        private readonly AppIdentityDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAbiturientRepository _abiturRepository;
        private readonly IFileModelRepository _fileModelRepository;
        private readonly IUserDocumentRepository _userDocumentRepository;
        private readonly ISelectListRepository _selectListRepository;
        private readonly IDocumentSamplesRepository _documentSamplesRepository;

        public AbiturController(AppIdentityDBContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IAbiturientRepository abiturRepository,
            IFileModelRepository fileModelRepository,
            IUserDocumentRepository userDocumentRepository,
            ISelectListRepository selectListRepository,
            IDocumentSamplesRepository documentSamplesRepository)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _abiturRepository = abiturRepository;
            _fileModelRepository = fileModelRepository;
            _userDocumentRepository = userDocumentRepository;
            _selectListRepository = selectListRepository;
            _documentSamplesRepository = documentSamplesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Стартовая страница абитуриента
        public async Task<IActionResult> Start(string selectedTab)
        {
            // Если пользователь не вошёл в систему,
            // отображаем форму, позволяющую либо зарегистрироваться,
            // либо войти в систему
            if (!User.Identity.IsAuthenticated)
                return View("AbiturLoginPage");

            var abiturient = await _abiturRepository.GetAbiturientAsync(User.Identity.Name);

            // Если пользователь не является абитуриентом - выводим соответствующее сообщение
            // и предлагаем стать абитуриентом            
            if (abiturient == null)
                return View("NotAbiturMessageForm");

            // Проверяем наличие у абитуриента загруженного заявления
            // на обработку персональных данных
            if (!_abiturRepository.IsLoadedFileApplicationForProcessingPersonalData(abiturient))                
                return RedirectToAction(nameof(LoadFileApplicationForProcessingPersonalData));

            // Проверяем наличие у абитуриента загруженной фотографии
            if (!_abiturRepository.IsLoadedFilePhoto(abiturient))
                return RedirectToAction(nameof(LoadFilePhoto));

            // Проверяем наличие у абитуриента загруженной скан-копии СНИЛС
            if (!_abiturRepository.IsLoadedFileSNILS(abiturient))
                return RedirectToAction(nameof(LoadFileSNILS));

            // Проверяем наличие у абитуриента загруженного документа об образовании
            if (!_abiturRepository.IsLoadedFileEducationDocuments(abiturient))
                return RedirectToAction(nameof(LoadFileEducationDocument));
            
            // Проверяем наличие сведений об образовании
            //if (! await _abiturRepository.IsUserEducationDataExists(abiturient))
            //    return RedirectToAction(nameof(CreateUserEducation));

            // Проверяем наличие у абитуриента загруженной скан-копии паспорта
            if (!_abiturRepository.IsLoadedFilePassport(abiturient))
                return RedirectToAction(nameof(LoadFilePassport));                        

            // Проверяем наличие паспортных данных
            //if (!_abiturRepository.IsPassportDataExists(abiturient))
            //    return RedirectToAction(nameof(CreatePassportData));

            // Проверяем наличие у абитуриента загруженной карточки абитуриента
            if (!_abiturRepository.IsLoadedFileAbiturientCard(abiturient))
                return RedirectToAction(nameof(LoadFileAbiturientCard));

            // Проверяем наличие заявлений о приёме
            if (!_abiturRepository.IsApplicationForAdmissionExists(abiturient))
                return RedirectToAction(nameof(CreateApplicationForAdmission));
            
            ViewBag.Abiturient = abiturient;
            ViewBag.SelectedTab = selectedTab;
            
            return View();
        }
        #endregion

        #region Личные данные абитуриентов
        public async Task<IActionResult> ChangePersonalInformation()
        {
            var abiturient = await _abiturRepository.GetAbiturientAsync(User.Identity.Name);
            if(abiturient.AppUser.Citizenship == null) abiturient.AppUser.Citizenship = "Россия";
            if (abiturient.AppUser.Nationality == null) abiturient.AppUser.Nationality = "Русский";            

            ViewBag.Genders = _selectListRepository.GetSelectListGenders(abiturient.AppUser.GenderId ?? 0);
            ViewBag.MilitaryServiceStatuses = _selectListRepository.GetSelectListMilitaryServiceStatuses(abiturient.AppUser.MilitaryServiceStatusId ?? 0);


            return View(abiturient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePersonalInformation(Abiturient abiturientUpdateData)
        {
            var entryAbitur = await _abiturRepository.GetAbiturientAsync(User.Identity.Name);
            if (entryAbitur == null)
                return NotFound();
            if (entryAbitur.AbiturientId != abiturientUpdateData.AbiturientId)
                return NotFound();

            entryAbitur.AppUser.GenderId = abiturientUpdateData.AppUser.GenderId;
            entryAbitur.AppUser.Citizenship = abiturientUpdateData.AppUser.Citizenship;
            entryAbitur.AppUser.Nationality = abiturientUpdateData.AppUser.Nationality;

            entryAbitur.AppUser.MilitaryServiceStatusId = abiturientUpdateData.AppUser.MilitaryServiceStatusId;
            entryAbitur.IsHostelRequired = abiturientUpdateData.IsHostelRequired;

            await _abiturRepository.UpdateAbiturientAsync(entryAbitur);

            return RedirectToAction(nameof(Start), new { selectedTab = "personalInformation" });
        }

        /// <summary>
        /// Добавление иностранного языка, которым владеет абитуриент
        /// </summary>
        /// <returns></returns>
        public IActionResult AddAppUserForeignLanguage()
        {
            ViewBag.ForeignLanguages = _selectListRepository.GetSelectListForeignLanguages();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAppUserForeignLanguage(AppUserForeignLanguage appUserForeignLanguage)
        { 
            await _abiturRepository.AddAbiturientForeignLanguage(User.Identity.Name, appUserForeignLanguage.ForeignLanguageId);

            return RedirectToAction(nameof(Start), new { selectedTab = "personalInformation" });
        }

        /// <summary>
        /// Добавление контакта ближайшего родственника
        /// </summary>
        /// <returns></returns>
        public IActionResult AddFamilyMemberContact()
        {
            ViewBag.FamilyMemberTypes = _selectListRepository.GetSelectListFamilyMemberTypes();
            return View();
        }

        /// <summary>
        /// Добавление контакта ближайшего родственника
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFamilyMemberContact(FamilyMemberContact familyMemberContact)
        {
            if(ModelState.IsValid)
            {
                await _abiturRepository.AddFamilyMemberContactAsync(User.Identity.Name, familyMemberContact);
                return RedirectToAction(nameof(Start), new { selectedTab = "personalInformation" });
            }

            ViewBag.FamilyMemberTypes = _selectListRepository.GetSelectListFamilyMemberTypes();
            return View(familyMemberContact);            
        }
        #endregion

                
        #region Заявления о приёме
        public IActionResult ChooseEduNapravl(int? eduLevelId, int? eduNapravlId)
        {
            if (eduLevelId == null)
            {
                ViewBag.EduLevels = _selectListRepository.GetSelectListEduLevels();
                return View();
            }

            if (eduNapravlId == null)
            {
                ViewBag.EduLevelId = eduLevelId;
                //ViewBag.EduNapravls = _selectListRepository.GetSelectListEduNapravlFullNamesOfEduLevel(eduLevelId);
                ViewBag.EduNapravls = _selectListRepository.GetSelectListEduNapravlFullNamesOfEduLevelWithout230203and230301(eduLevelId);
                return View();
            }

            return RedirectToAction(nameof(CreateApplicationForAdmission), new { eduNapravlId });
        }

        public async Task<IActionResult> CreateApplicationForAdmission(int? eduNapravlId)
        {
            int numApplicationForAdmissions = _abiturRepository.GetNumberOfApplicationForAdmissions(User.Identity.Name);
            if (numApplicationForAdmissions >= 6)
                return View("Max" +
                    "NumberOfApplicationForAdmissions");

            if (eduNapravlId == null)
                return RedirectToAction(nameof(ChooseEduNapravl));

            var abiturient = await _abiturRepository.GetAbiturientAsync(User.Identity.Name);

            int numOfApplicationsForAdmission = 0;
            if (abiturient.ApplicationForAdmissions != null)
                numOfApplicationsForAdmission = abiturient.ApplicationForAdmissions.Count();

            var applicationForAdmission = new ApplicationForAdmission
            {
                CreationDate = DateTime.Now,
                RegNumber = "-",
                Abiturient = abiturient,
                AbiturientId = abiturient.AbiturientId,
                QuotaTypeId = (int)QuotaTypesEnum.KontrolnieCifri,
                PriorityId = numOfApplicationsForAdmission + 1
            };

            ViewBag.EduNapravl = await _context.EduNapravls
                .Include(n => n.EduUgs.EduLevel)
                .FirstOrDefaultAsync(n=>n.EduNapravlId == eduNapravlId);

            ViewBag.EduProfiles = _selectListRepository.GetSelectListEduProfilesOfEduNapravl(eduNapravlId);
            //ViewBag.EduForms = _selectListRepository.GetSelectListEduFormsForAbiturient();
            ViewBag.EduForms = _selectListRepository.GetSelectListEduFormsForAbiturient(eduNapravlId ?? 0, 0);
            //ViewBag.QuotaTypes = _selectListRepository.GetSelectListQuotaTypes(applicationForAdmission.QuotaTypeId);
            ViewBag.QuotaTypes = _selectListRepository.GetSelectListQuotaTypes(eduNapravlId ?? 0, applicationForAdmission.QuotaTypeId);

            return View(applicationForAdmission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApplicationForAdmission(ApplicationForAdmission applicationForAdmission)
        {
            await _abiturRepository.AddApplicationForAdmission(applicationForAdmission);

            //return RedirectToAction(nameof(Start), new { selectedTab = "applicationForAdmissions" } );
            return RedirectToAction(nameof(ApplicationForAdmissionFileLoad), new { applicationForAdmission.ApplicationForAdmissionId });
        }

        public async Task<IActionResult> ApplicationForAdmissionEdit(int applicationForAdmissionId)
        {
            var applicationForAdmission = await _abiturRepository.GetApplicationForAdmissionAsync(User.Identity.Name, applicationForAdmissionId);
            if (applicationForAdmission == null)
                return NotFound();

            var documentSamples = _documentSamplesRepository.GetDocumentSamples(applicationForAdmission.EduProfileId, FileDataTypeEnum.ApplicationForAdmission);
            ViewBag.DocumentSamples = await documentSamples.ToListAsync();

            ViewBag.EduProfiles = _selectListRepository.GetSelectListEduProfileFullNames(applicationForAdmission.EduProfileId);
            ViewBag.EduForms = _selectListRepository.GetSelectListEduFormsForAbiturient(applicationForAdmission.EduFormId);
            ViewBag.QuotaTypes = _selectListRepository.GetSelectListQuotaTypes(applicationForAdmission.QuotaTypeId);

            return View(applicationForAdmission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplicationForAdmissionEdit(ApplicationForAdmission applicationForAdmission,
            IFormFile uploadedFile)
        {
            applicationForAdmission.RowStatusId = (int)RowStatusEnum.ChangedByUser;
            await _abiturRepository.UpdateApplicationForAdmissionAsync(applicationForAdmission, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "applicationForAdmissions" });
        }

        public async Task<IActionResult> ApplicationForAdmissionRemove(int applicationForAdmissionId)
        {
            var applicationForAdmission = await _abiturRepository.GetApplicationForAdmissionAsync(User.Identity.Name, applicationForAdmissionId);
            if (applicationForAdmission == null)
                return NotFound();                       

            return View(applicationForAdmission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplicationForAdmissionRemove(ApplicationForAdmission applicationForAdmission)
        {            
            await _abiturRepository.RemoveApplicationForAdmissionAsync(User.Identity.Name, applicationForAdmission.ApplicationForAdmissionId);

            return RedirectToAction(nameof(Start), new { selectedTab = "applicationForAdmissions" });
        }

        /// <summary>
        /// Загрузка скан-копии заявления о приёме
        /// </summary>
        /// <param name="applicationForAdmissionId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ApplicationForAdmissionFileLoad(int applicationForAdmissionId)
        {
            var applicationForAdmission = await _abiturRepository.GetApplicationForAdmissionAsync(User.Identity.Name, applicationForAdmissionId);

            var documentSamples = _documentSamplesRepository.GetDocumentSamples(applicationForAdmission.EduProfileId, FileDataTypeEnum.ApplicationForAdmission);
            ViewBag.DocumentSamples = await documentSamples.ToListAsync();

            return View(applicationForAdmission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplicationForAdmissionFileLoad(int applicationForAdmissionId, IFormFile uploadedFile)
        {
            await _abiturRepository.ApplicationForAdmissionFileLoadAsync(User.Identity.Name, applicationForAdmissionId, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "applicationForAdmissions" });
        }
        #endregion

        #region Индивидуальные достижения абитуриента
        public async Task<IActionResult> CreateAbiturientIndividualAchievment()
        {
            var abiturient = await _abiturRepository.GetAbiturientAsync(User.Identity.Name);

            var abiturientIndividualAchievment = new AbiturientIndividualAchievment
            {
                AbiturientId = abiturient.AbiturientId,
                Abiturient = abiturient
            };

            ViewBag.AbiturientIndividualAchievmentTypes = _selectListRepository.GetSelectListAbiturientIndividualAchievmentTypes();
            return View(abiturientIndividualAchievment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAbiturientIndividualAchievment(AbiturientIndividualAchievment abiturientIndividualAchievment,
            IFormFile uploadedFile)
        {
            await _abiturRepository.AddAbiturientIndividualAchievment(abiturientIndividualAchievment);
            if (uploadedFile != null)
            {
                await _abiturRepository.UpdateAbiturientIndividualAchievmentFileAsync(abiturientIndividualAchievment.AbiturientIndividualAchievmentId,
                uploadedFile);
            }

            return RedirectToAction(nameof(Start), new { selectedTab = "abiturientAchievments" });
        }

        /// <summary>
        /// Представление загрузки скан-копии подтверждающего документа
        /// индивидуального достижения абитуриента
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LoadAbiturientIndividualAchievmentFile(int abiturientIndividualAchievmentId)
        {
            var abiturAchievment = await _abiturRepository.GetAbiturientIndividualAchievmentAsync(abiturientIndividualAchievmentId);

            return View(abiturAchievment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadAbiturientIndividualAchievmentFile(int abiturientIndividualAchievmentId,
            IFormFile uploadedFile)
        {
            await _abiturRepository.UpdateAbiturientIndividualAchievmentFileAsync(abiturientIndividualAchievmentId, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "abiturientAchievments" });
        }

        /// <summary>
        /// Редактирование записи об индивидуальном достижении абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievmentId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AbiturientIndividualAchievmentEdit(int abiturientIndividualAchievmentId)
        {
            var abiturientIndividualAchievment = await _abiturRepository.GetAbiturientIndividualAchievmentAsync(abiturientIndividualAchievmentId);
            ViewBag.AbiturientIndividualAchievmentTypes = _selectListRepository.GetSelectListAbiturientIndividualAchievmentTypes(abiturientIndividualAchievment.AbiturientIndividualAchievmentTypeId);
            return View(abiturientIndividualAchievment);
        }

        /// <summary>
        /// Редактирование записи об индиальном достижении абитуриента (HttpPost)
        /// </summary>
        /// <param name="abiturientIndividualAchievment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AbiturientIndividualAchievmentEdit(AbiturientIndividualAchievment abiturientIndividualAchievment,
            IFormFile uploadedFile)
        {
            await _abiturRepository.UpdateAbiturientIndividualAchievment(abiturientIndividualAchievment, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "abiturientAchievments" });
        }


        public async Task<IActionResult> AbiturientIndividualAchievmentRemove(int abiturientIndividualAchievmentId)
        {
            var abiturientIndividualAchievment = await _abiturRepository.GetAbiturientIndividualAchievmentAsync(abiturientIndividualAchievmentId);

            return View(abiturientIndividualAchievment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AbiturientIndividualAchievmentRemove(AbiturientIndividualAchievment abiturientIndividualAchievment)
        {
            await _abiturRepository.RemoveAbiturientIndividualAchievmentAsync(abiturientIndividualAchievment.AbiturientIndividualAchievmentId);

            return RedirectToAction(nameof(Start), new { selectedTab = "abiturientAchievments" });
        }
        #endregion

        #region Льготы
        public async Task<IActionResult> CreateAbiturientAdmissionPrivilege()
        {
            var abiturient = await _abiturRepository.GetAbiturientAsync(User.Identity.Name);
            ViewBag.ApplicationForAdmissions = _selectListRepository.GetSelectListApplicationForAdmissions(abiturient.AbiturientId, 0);
            ViewBag.AdmissionPrivilegeTypes = _selectListRepository.GetSelectListAdmissionPrivilegeTypes();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAbiturientAdmissionPrivilege(AdmissionPrivilege admissionPrivilege,
            IFormFile uploadedFile)
        {
            await _abiturRepository.CreateAdmissionPrivilegeAsync(User.Identity.Name, admissionPrivilege, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "admissionPrivileges" });
        }

        /// <summary>
        /// Удаление льготы абитуриента при поступлении
        /// </summary>
        /// <param name="abiturientAdmissionPrivilegeId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AbiturientAdmissionPrivilegeRemove( int abiturientAdmissionPrivilegeId)
        {
            AdmissionPrivilege admissionPrivilege = await _abiturRepository.GetAdmissionPrivilegeAsync(User.Identity.Name, abiturientAdmissionPrivilegeId);

            if (admissionPrivilege == null)
                return NotFound();

            return View(admissionPrivilege);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AbiturientAdmissionPrivilegeRemove(AdmissionPrivilege admissionPrivilege)
        {
            await _abiturRepository.RemoveAdmissionPrivilegeAsync(User.Identity.Name, admissionPrivilege.AdmissionPrivilegeId);                       

            return RedirectToAction(nameof(Start), new { selectedTab = "admissionPrivileges" });
        }

        /// <summary>
        /// Редактирование льготы для поступления
        /// </summary>
        /// <param name="abiturientAdmissionPrivilegeId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AbiturientAdmissionPrivilegeEdit(int abiturientAdmissionPrivilegeId)
        {
            AdmissionPrivilege admissionPrivilege = await _abiturRepository.GetAdmissionPrivilegeAsync(User.Identity.Name, abiturientAdmissionPrivilegeId);

            if (admissionPrivilege == null)
                return NotFound();

            ViewBag.ApplicationForAdmissions = _selectListRepository.GetSelectListApplicationForAdmissions(admissionPrivilege.ApplicationForAdmission.AbiturientId);
            ViewBag.AdmissionPrivilegeTypes = _selectListRepository.GetSelectListAdmissionPrivilegeTypes(admissionPrivilege.AdmissionPrivilegeTypeId);

            return View(admissionPrivilege);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AbiturientAdmissionPrivilegeEdit(AdmissionPrivilege admissionPrivilege,
            IFormFile uploadedFile)
        {
            await _abiturRepository.UpdateAdmissionPrivilegeAsync(User.Identity.Name, admissionPrivilege, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "admissionPrivileges" });
        }
        #endregion

        #region Заявления о согласии на зачисление
        public async Task<IActionResult> CreateAbiturientConsentToEnrollment(int applicationForAdmissionId)
        {
            var applicationForAdmission = await _abiturRepository.GetApplicationForAdmissionAsync(User.Identity.Name, applicationForAdmissionId);

            var documentSamples = _documentSamplesRepository.GetDocumentSamples(applicationForAdmission.EduProfileId, FileDataTypeEnum.ConsentToEnrollment);
            ViewBag.DocumentSamples = await documentSamples.ToListAsync();

            var consentToEnrollment = new ConsentToEnrollment
            {
                ApplicationForAdmission = applicationForAdmission,
                ApplicationForAdmissionId = applicationForAdmission.ApplicationForAdmissionId
            };

            return View(consentToEnrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAbiturientConsentToEnrollment(ConsentToEnrollment consentToEnrollment,
            IFormFile uploadedFile)
        {
            await _abiturRepository.CreateConsentToEnrollmentAsync(User.Identity.Name, consentToEnrollment, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "consentToEnrollments" });
        }

        /// <summary>
        /// Редактирование заявления о согласии на зачисление абитуриента
        /// </summary>
        /// <param name="abiturientConsentToEnrollmentId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AbiturientConsentToEnrollmentEdit(int abiturientConsentToEnrollmentId)
        {
            if (abiturientConsentToEnrollmentId == 0)
                return NotFound();

            var consentToEnrollment = await _abiturRepository.GetConsentToEnrollment(User.Identity.Name, abiturientConsentToEnrollmentId);
            if (consentToEnrollment == null)
                return NotFound();

            var documentSamples = _documentSamplesRepository.GetDocumentSamples(consentToEnrollment.ApplicationForAdmission.EduProfileId, FileDataTypeEnum.ConsentToEnrollment);
            ViewBag.DocumentSamples = await documentSamples.ToListAsync();

            ViewBag.ApplicationForAdmissions = _selectListRepository
                .GetSelectListApplicationForAdmissions(consentToEnrollment.ApplicationForAdmission.AbiturientId,
                consentToEnrollment.ApplicationForAdmissionId);

            return View(consentToEnrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AbiturientConsentToEnrollmentEdit(ConsentToEnrollment consentToEnrollment, IFormFile uploadedFile)
        {
            if (consentToEnrollment == null)
                return NotFound();

            await _abiturRepository.UpdateConsentToEnrollment(User.Identity.Name, consentToEnrollment, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "consentToEnrollments" });
        }

        public async Task<IActionResult> AbiturientConsentToEnrollmentRemove(int abiturientConsentToEnrollmentId)
        {
            if (abiturientConsentToEnrollmentId == 0)
                return NotFound();

            var consentToEnrollment = await _abiturRepository.GetConsentToEnrollment(User.Identity.Name, abiturientConsentToEnrollmentId);
            if (consentToEnrollment == null)
                return NotFound();

            return View(consentToEnrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AbiturientConsentToEnrollmentRemove(ConsentToEnrollment consentToEnrollment)
        {
            if (consentToEnrollment == null)
                return NotFound();
                        
            await _abiturRepository.RemoveConsentToEnrollment(User.Identity.Name, consentToEnrollment.ConsentToEnrollmentId);

            return RedirectToAction(nameof(Start), new { selectedTab = "consentToEnrollments" });
        }
        #endregion

        #region Заявления об отзыве документов

        public async Task<IActionResult> CreateRevocationStatement()
        {
            var documentSamples = _documentSamplesRepository.GetDocumentSamples(FileDataTypeEnum.AbiturientFiles_RevocationStatement);
            ViewBag.DocumentSamples = await documentSamples.ToListAsync();


            var abiturient = await _abiturRepository.GetAbiturientAsync(User.Identity.Name);
            if (abiturient == null)
                return NotFound();

            ViewBag.ApplicationForAdmissions = _selectListRepository.GetSelectListApplicationForAdmissions(abiturient.AbiturientId, 0);

            var revocationStatement = new RevocationStatement
            {
                 Date = DateTime.Now
            };

            return View(revocationStatement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRevocationStatement(RevocationStatement revocationStatement, 
            IFormFile uploadedFile)
        {            
            if (uploadedFile == null /*|| string.IsNullOrWhiteSpace(revocationStatement.RejectionReason)*/)
            {
                var documentSamples = _documentSamplesRepository.GetDocumentSamples(FileDataTypeEnum.AbiturientFiles_RevocationStatement);
                ViewBag.DocumentSamples = await documentSamples.ToListAsync();

                var abiturient = await _abiturRepository.GetAbiturientAsync(User.Identity.Name);
                if (abiturient == null)
                    return NotFound();

                ViewBag.ApplicationForAdmissions = _selectListRepository.GetSelectListApplicationForAdmissions(
                    abiturient.AbiturientId,
                    revocationStatement.ApplicationForAdmissionId);
                
                return View(revocationStatement);
            }

            revocationStatement.RowStatusId = (int)RowStatusEnum.NotConfirmed;

            await _abiturRepository.CreateRevocationStatement(User.Identity.Name, revocationStatement, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "revocationStatements" });
        }



        public async Task<IActionResult> RevocationStatementEdit(int revocationStatementId)
        {
            var documentSamples = _documentSamplesRepository.GetDocumentSamples(FileDataTypeEnum.AbiturientFiles_RevocationStatement);
            ViewBag.DocumentSamples = await documentSamples.ToListAsync();

            var abiturient = await _abiturRepository.GetAbiturientAsync(User.Identity.Name);
            if (abiturient == null)
                return NotFound();
                        

            var entry = await _abiturRepository.GetRevocationStatementAsync(User.Identity.Name, revocationStatementId);
            ViewBag.ApplicationForAdmissions = _selectListRepository.GetSelectListApplicationForAdmissions(abiturient.AbiturientId, entry.ApplicationForAdmissionId);

            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RevocationStatementEdit(RevocationStatement revocationStatement,
            IFormFile uploadedFile)
        {
            await _abiturRepository.UpdateRevocationStatement(User.Identity.Name, revocationStatement, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "revocationStatements" });
        }




        public async Task<IActionResult> RevocationStatementRemove(int revocationStatementId)
        {
            var revocationStatement = await _abiturRepository.GetRevocationStatementAsync(User.Identity.Name, revocationStatementId);
            if (revocationStatement == null)
                return NotFound();

            return View(revocationStatement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RevocationStatementRemove(RevocationStatement revocationStatement)
        {
            await _abiturRepository.RemoveRevocationStatementAsync(User.Identity.Name, revocationStatement.RevocationStatementId);

            return RedirectToAction(nameof(Start), new { selectedTab = "revocationStatements" });
        }

        #endregion

        #region Договоры
        public async Task<IActionResult> CreateAbiturientContract(int applicationForAdmissionId)
        {
            var applicationForAdmission = await _abiturRepository.GetApplicationForAdmissionAsync(User.Identity.Name, applicationForAdmissionId);

            int contractTypeId = 0;
            IQueryable<DocumentSample> documentSamples = null;
            if (applicationForAdmission.QuotaTypeId == (int)QuotaTypesEnum.CelevoyPriem)
            {
                contractTypeId = (int)ContractTypesEnum.ContractTargetTraining;
                documentSamples = _documentSamplesRepository.GetDocumentSamples(FileDataTypeEnum.DogovorOCelevomObuchenii);
            }

            if (applicationForAdmission.QuotaTypeId == (int)QuotaTypesEnum.DogovorObOkazaniiPlatnihObrazovatUslug)
            {
                contractTypeId = (int)ContractTypesEnum.ContractPaidTraining;
                documentSamples = _documentSamplesRepository.GetDocumentSamples(FileDataTypeEnum.DogovorObOkazaniiPlatnihObrazovatelnihUslug);
            }

            if(documentSamples != null)
            {
                ViewBag.DocumentSamples = await documentSamples.ToListAsync();
            }
            
            var contract = new Contract
            {
                AppUserId = applicationForAdmission.Abiturient.AppUserId,
                ApplicationForAdmission = applicationForAdmission,
                ApplicationForAdmissionId = applicationForAdmission.ApplicationForAdmissionId,
                ContractTypeId = contractTypeId,
                RowStatusId = (int) RowStatusEnum.NotConfirmed
            };

            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAbiturientContract(Contract contract,
            IFormFile uploadedFile)
        {
            await _abiturRepository.CreateContractAsync(User.Identity.Name, contract, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "contracts" });
        }

        /// <summary>
        /// Редактирование договора абитуриента
        /// </summary>
        /// <param name="abiturientConsentToEnrollmentId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AbiturientContractEdit(int abiturientContractId)
        {
            if (abiturientContractId == 0)
                return NotFound();

            var abiturientContract = await _abiturRepository.GetContractAsync(User.Identity.Name, abiturientContractId);
            if (abiturientContract == null)
                return NotFound();                        

            return View(abiturientContract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AbiturientContractEdit(Contract contract, IFormFile uploadedFile)
        {
            if (contract == null)
                return NotFound();

            await _abiturRepository.UpdateContractAsync(User.Identity.Name, contract, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "contracts" });
        }

        /// <summary>
        /// Удаление договора абитуриента
        /// </summary>
        /// <param name="abiturientConsentToEnrollmentId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AbiturientContractRemove(int abiturientContractId)
        {
            if (abiturientContractId == 0)
                return NotFound();

            var abiturientContract = await _abiturRepository.GetContractAsync(User.Identity.Name, abiturientContractId);
            if (abiturientContract == null)
                return NotFound();

            return View(abiturientContract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AbiturientContractRemove(Contract contract)
        {
            if (contract == null)
                return NotFound();

            await _abiturRepository.RemoveContractAsync(User.Identity.Name, contract);

            return RedirectToAction(nameof(Start), new { selectedTab = "contracts" });
        }
        #endregion

        #region Оплата по договору
        public async Task<IActionResult> CreateAbiturientContractPayment(int abiturientContractId)
        {
            var contract = await _abiturRepository.GetContractAsync(User.Identity.Name, abiturientContractId);
            ViewBag.Contract = contract;

            var payment = new Payment
            {
                ContractId = contract.ContractId,
                Contract = contract,
                Description = $"Оплата по договору",
                PaymentDate = DateTime.Now,
                RowStatusId = (int)RowStatusEnum.NotConfirmed
            };

            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAbiturientContractPayment(Payment payment, IFormFile uploadedFile)
        {
            await _abiturRepository.CreateContractPaymentAsync(User.Identity.Name, payment, uploadedFile);

            return RedirectToAction(nameof(Start), new { selectedTab = "contracts" });
        }
        #endregion

        #region Паспортные данные
        public async Task<IActionResult> CreatePassportData()
        {
            UserDocument userDocumentPassport = await _userDocumentRepository.GetUserDocumentPassportWithoutPassportDataAsync(User.Identity.Name);
            
            var passportData = await _abiturRepository.GetPassportDataAsync(User.Identity.Name);

            if(passportData == null)
            {
                var appUser = (await _abiturRepository.GetAbiturientAsync(User.Identity.Name)).AppUser;
                passportData = new PassportData
                {
                    AppUserId = appUser.Id,
                    AppUser = appUser,
                    DataVidachi = DateTime.Now,
                    Address = new Address { Country = "Россия" },
                    UserDocument = userDocumentPassport,
                    UserDocumentId = userDocumentPassport.UserDocumentId
                };
            }

            ViewBag.PopulatedLocalities = _selectListRepository.GetSelectListPopulatedLocalities();
            return View(passportData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePassportData(PassportData passportData)
        {
            if(ModelState.IsValid)
            {
                await _abiturRepository.AddPassportDataAsync(passportData);
            }
            else
            {
                ViewBag.PopulatedLocalities = _selectListRepository.GetSelectListPopulatedLocalities();
                return View(passportData);
            }

            return RedirectToAction(nameof(Start));
        }

        /// <summary>
        /// Изменение паспортных данных
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ChangePassportData()
        {
            var passport = await _abiturRepository.GetPassportDataAsync(User.Identity.Name);
            if (passport == null) return NotFound();

            if (passport == null)
            {
                passport = new PassportData
                {
                    RowStatusId = (int)RowStatusEnum.ChangedByUser
                };
            }

            ViewBag.PopulatedLocalities = _selectListRepository.GetSelectListPopulatedLocalities(passport.Address.PopulatedLocalityId ?? 0);
            return View(passport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassportData(PassportData passportData)
        {
            if (ModelState.IsValid)
            {
                await _abiturRepository.ChangePassportData(User.Identity.Name, passportData);
            }
            else
            {
                ViewBag.PopulatedLocalities = _selectListRepository.GetSelectListPopulatedLocalities();
                return View(passportData);
            }

            return RedirectToAction(nameof(Start));
        }
        #endregion

        #region Добавление сведений об образовании абитуриента для первого незаполненного загруженного документа об образовании
        /// <summary>
        /// Добавление сведений об образовании абитуриента для первого незаполненного загруженного документа об образовании
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateUserEducation()
        {
            var eduDocumentsWithoutUserEducationData = _abiturRepository.GetUserDocumentsWithoutUserEducationDataAsync(User.Identity.Name);
            var eduDocument = await eduDocumentsWithoutUserEducationData.FirstOrDefaultAsync();

            var userEducation = new UserEducation
            {
                AppUserId = eduDocument.AppUserId,
                AppUser = eduDocument.AppUser,
                FileDataTypeId = eduDocument.FileDataTypeId,
                FileDataType = eduDocument.FileDataType,
                UserDocumentId = eduDocument.UserDocumentId,
                UserDocument = eduDocument,                
            };

            ViewBag.EducationalInstitutions = _selectListRepository.GetSelectListEducationalInstitutions();
            ViewBag.FileDataTypes = _selectListRepository.GetSelectListEducationDocumentsForAbiturients();
            ViewBag.UserQualifications = _selectListRepository.GetSelectListUserQualifications(User.Identity.Name);
            return View(userEducation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUserEducation(UserEducation userEducation)
        {
            await _abiturRepository.AddUserEducationAsync(userEducation);
            return RedirectToAction(nameof(Start));
        }
        #endregion

        #region Загрузка заявления на обработку персональных данных
        /// <summary>
        /// Загрузка заявления
        /// на обработку персональных данных
        /// </summary>
        public async Task<IActionResult> LoadFileApplicationForProcessingPersonalData()
        {
            var documentSamples = _documentSamplesRepository.GetDocumentSamples(FileDataTypeEnum.SoglasieNaObrabotkuPersonalnihDannih);
            ViewBag.DocumentSamples = await documentSamples.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadFileApplicationForProcessingPersonalData(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
                RedirectToAction(nameof(Start));

            UserDocument newUserDocument = await _userDocumentRepository
                .CreateApplicationForProcessingPersonalDataAsync(User.Identity.Name, uploadedFile);

            return RedirectToAction(nameof(Start));
        }
        #endregion

        #region Загрузка документа об образовании
        /// <summary>
        /// Загрузка документа об образовании
        /// </summary>
        /// <returns></returns>
        public IActionResult LoadFileEducationDocument()
        {            
            ViewBag.TypesOfEducationDocuments = _selectListRepository.GetSelectListEducationDocumentsForAbiturients((int)FileDataTypeEnum.AttestatOSrednemObshemObrazovanii);

            return View();
        }

        /// <summary>
        /// Загрузка документа об образовании
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoadFileEducationDocument(IFormFile uploadedFile,
            FileDataTypeEnum typeOfEducationDocument)
        {
            if (uploadedFile == null)
                RedirectToAction(nameof(Start));

            UserDocument newUserDocument = await _userDocumentRepository
                .CreateEducationDocumentAsync(User.Identity.Name, uploadedFile, typeOfEducationDocument);

            return RedirectToAction(nameof(Start));
        }
        #endregion

        #region Загрузка паспорта абитуриента
        /// <summary>
        /// Загрузка документа об образовании
        /// </summary>
        /// <returns></returns>
        public IActionResult LoadFilePassport()
        {            
            
            return View();
        }

        /// <summary>
        /// Загрузка паспорта
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoadFilePassport(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
                RedirectToAction(nameof(Start));                       

            UserDocument newUserDocument = await _userDocumentRepository
                .CreatePassport(User.Identity.Name, uploadedFile);

            return RedirectToAction(nameof(Start));
        }
        #endregion

        #region Загрузка карточки абитуриента
        public async Task<IActionResult> LoadFileAbiturientCard()
        {
            var documentSamples = _documentSamplesRepository.GetDocumentSamples(FileDataTypeEnum.AbiturientCard);
            ViewBag.DocumentSamples = await documentSamples.ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadFileAbiturientCard(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
                RedirectToAction(nameof(Start));

            UserDocument newUserDocument = await _userDocumentRepository
                .CreateAbiturientCard(User.Identity.Name, uploadedFile);

            return RedirectToAction(nameof(Start));
        }
        #endregion

        #region Загрузка фотографии абитуриента
        public IActionResult LoadFilePhoto()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadFilePhoto(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
                RedirectToAction(nameof(Start));

            UserDocument newUserDocument = await _userDocumentRepository
                .CreatePhoto(User.Identity.Name, uploadedFile);

            return RedirectToAction(nameof(Start));
        }
        #endregion

        #region Загрузка СНИЛС абитуриента
        public IActionResult LoadFileSNILS()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadFileSNILS(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
                RedirectToAction(nameof(Start));

            UserDocument newUserDocument = await _userDocumentRepository
                .CreateSNILS(User.Identity.Name, uploadedFile);

            return RedirectToAction(nameof(Start));
        }
        #endregion

        #region Регистрация абитуриентов и прием документов
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(Start));

            return View();
        }

        /// <summary>
        /// Регистрация существующего пользователя в качестве абитуриента
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUserAsAbiturient()
        {
            await _abiturRepository.RegisterUserAsAbiturient(User.Identity.Name);
            return RedirectToAction(nameof(Start));
        }

        /// <summary>
        /// Регистрация нового пользователя в качестве абитуриента
        /// </summary>
        /// <param name="appUserCreateModelAbitur"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(AppUserCreateModelAbitur model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Patronymic = model.Patronymic,
                    Birthdate = model.Birthdate,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email,
                    Email = model.Email,
                    AppUserStatusId = (int?)AppUserStatusEnum.NewUser,
                    RegisterDateTime = DateTime.Now,
                    Abiturient = new Abiturient
                    {
                        AbiturientStatusId = (int)AbiturientStatusEnum.NewAbiturient                        
                    }
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Если регистрация прошла успешно, логинимся под созданным пользователем
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction(nameof(Start));
                    }
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);            
        }
                

        /// <summary>
        /// Генерирует заявление о приёме
        /// </summary>
        /// <returns></returns>
        public IActionResult GenerateApplicationForAdmission()
        {
            var systemFontCollection = SystemFonts.Collection;

            #region SixLabors.ImageSharp
            using (Image img = new Image<Rgba32>(1500, 500))
            {
                PathBuilder pathBuilder = new PathBuilder();
                pathBuilder.SetOrigin(new PointF(500, 0));
                pathBuilder.AddBezier(new PointF(50, 450), new PointF(200, 50), new PointF(300, 50), new PointF(450, 450));
                // add more complex paths and shapes here.

                IPath path = pathBuilder.Build();

                // For production application we would recomend you create a FontCollection
                // singleton and manually install the ttf fonts yourself as using SystemFonts
                // can be expensive and you risk font existing or not existing on a deployment
                // by deployment basis.
                var font = SystemFonts.CreateFont(
                    "Arial", 14, FontStyle.Regular);

                string text = "Тестовая строка";
                var textGraphicsOptions = new TextGraphicsOptions(true) // draw the text along the path wrapping at the end of the line
                {
                    WrapTextWidth = path.Length
                };

                // lets generate the text as a set of vectors drawn along the path


                //var glyphs = TextBuilder.GenerateGlyphs(text, path, new RendererOptions(font, textGraphicsOptions.DpiX, textGraphicsOptions.DpiY)
                //{
                //    HorizontalAlignment = textGraphicsOptions.HorizontalAlignment,
                //    TabWidth = textGraphicsOptions.TabWidth,
                //    VerticalAlignment = textGraphicsOptions.VerticalAlignment,
                //    WrappingWidth = textGraphicsOptions.WrapTextWidth,
                //    ApplyKerning = textGraphicsOptions.ApplyKerning
                //});

                img.Mutate(ctx => ctx
                    .Fill(Color.White) // white background image
                    .Draw(Color.Gray, 3, path)
                    .DrawText(text,font,Color.Black,new PointF(50,250))); // draw the path so we can see what the text is supposed to be following
                    //.Fill((GraphicsOptions)textGraphicsOptions, Color.Black, glyphs));

                img.Save("wordart.png");                               
            }
            #endregion

            #region Free Spire.PDF
            //Create a pdf document.
            PdfDocument doc = new PdfDocument();
            // Create one page
            PdfPageBase page = doc.Pages.Add();
            
            //Draw the text
            page.Canvas.DrawString(@"TestString",
                                   new PdfFont(PdfFontFamily.TimesRoman, 14f),
                                   new PdfSolidBrush(new PdfRGBColor(0,0,0)),
                                   10, 10);

            var font2 = new System.Drawing.Font("Times New Roman", 14f);
            PdfTrueTypeFont pdfTrueTypeFont = new PdfTrueTypeFont(font2, true);
            PdfFontBase fontUtf = pdfTrueTypeFont;
            page.Canvas.DrawString("Заявление о приёме",
                fontUtf,
                new PdfSolidBrush(new PdfRGBColor(0, 0, 0)),
                30, 30,
                new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle));


            //Draw the image
            PdfImage image = PdfImage.FromFile(@"wordart.png");
            float width = image.Width * 0.75f;
            float height = image.Height * 0.75f;
            float x = (page.Canvas.ClientSize.Width - width) / 2;
            page.Canvas.DrawImage(image, x, 150, width, height);




            //Save pdf file.
            doc.SaveToFile("HelloWorld.pdf");
            doc.Close();

            #endregion

            return View();
        }
        #endregion

        #region Замена и удаление документов абитуриента
        public async Task<IActionResult> ReloadUserDocument(int userDocumentId)
        {
            var userDocument = await _abiturRepository.GetUserDocumentAsync(User.Identity.Name, userDocumentId);

            var documentSamples = _documentSamplesRepository.GetDocumentSamples((FileDataTypeEnum)userDocument.FileDataTypeId);
            ViewBag.DocumentSamples = await documentSamples.ToListAsync();

            return View(userDocument);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReloadUserDocumentConfirmed(int userDocumentId, IFormFile uploadedFile)
        {
            await _abiturRepository.ReloadUserDocumentAsync(User.Identity.Name, userDocumentId, uploadedFile);

            return RedirectToAction(nameof(Start));
        }


        public async Task<IActionResult> RemoveUserDocument(int userDocumentId)
        {
            var userDocument = await _abiturRepository.GetUserDocumentAsync(User.Identity.Name, userDocumentId);

            return View(userDocument);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserDocumentConfirmed(int userDocumentId)
        {
            await _abiturRepository.RemoveUserDocumentAsync(User.Identity.Name, userDocumentId);            

            return RedirectToAction(nameof(Start));
        }
        #endregion

        
        /////////////////////////////////////////////////////////////////////////////////////////
        #region Раздел "Сведения об образовательной организации"

        /// <summary>
        /// Подраздел для представления информации по СПО
        /// </summary>
        /// <returns></returns>
        public IActionResult SrProf()
        {
            searchTemplate = "среднее";            
            
            GetDataForTables(ref priemKolMest, ref PriemExam, ref priemKolTarget, ref blankNum, searchTemplate);

            ViewData["priemKolMest"] = priemKolMest;
            ViewData["PriemExam"] = PriemExam;
            ViewData["priemKolTarget"] = priemKolTarget;
            ViewData["blankNum"] = blankNum;

            return View();
        }

        private void GetDataForTables(ref List<priemKolMest> priemKolMest, ref List<PriemExam> priemExam, ref List<priemKolTarget> priemKolTarget, ref List<BlankNum> blankNum, string searchTemplate)
        {
            priemKolMest =  _context.priemKolMest
                .Include(p => p.EduNapravl)
                    .ThenInclude(n => n.EduUgs)
                        .ThenInclude(ugs => ugs.EduLevel)
                .Where(p => p.EduNapravl.EduUgs.EduLevel.EduLevelName.Contains(searchTemplate))
                .ToList();

            priemExam = _context.PriemExam
                .Include(p => p.EduNapravl)
                    .ThenInclude(n => n.EduUgs)
                        .ThenInclude(ugs => ugs.EduLevel)
                .Where(p => p.EduNapravl.EduUgs.EduLevel.EduLevelName.Contains(searchTemplate))
                .ToList();

            priemKolTarget = _context.priemKolTarget
                .Include(p => p.EduNapravl)
                    .ThenInclude(n => n.EduUgs)
                        .ThenInclude(ugs => ugs.EduLevel)
                .Where(p => p.EduNapravl.EduUgs.EduLevel.EduLevelName.Contains(searchTemplate))
                .ToList();

            blankNum=_context.BlankNums
                .Include(p=> p.EduNapravl)
                .ThenInclude(n => n.EduUgs)
                        .ThenInclude(ugs => ugs.EduLevel)
                .Where(p => p.EduNapravl.EduUgs.EduLevel.EduLevelName.Contains(searchTemplate))
                .ToList();
        }

        /// <summary>
        /// Подраздел для представления информации по бакалавриату
        /// </summary>
        /// <returns></returns>
        public IActionResult Bachelor()
        {
            searchTemplate = "бакалавриат";

            GetDataForTables(ref priemKolMest, ref PriemExam, ref priemKolTarget, ref blankNum, searchTemplate);

            ViewData["priemKolMest"] = priemKolMest;
            ViewData["PriemExam"] = PriemExam;
            ViewData["priemKolTarget"] = priemKolTarget;
            ViewData["blankNum"] = blankNum;

            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по специалитету
        /// </summary>
        /// <returns></returns>
        public IActionResult Special()
        {
            searchTemplate = "специалитет";

            GetDataForTables(ref priemKolMest, ref PriemExam, ref priemKolTarget, ref blankNum, searchTemplate);

            ViewData["priemKolMest"] = priemKolMest;
            ViewData["PriemExam"] = PriemExam;
            ViewData["priemKolTarget"] = priemKolTarget;
            ViewData["blankNum"] = blankNum;

            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по магистратуре
        /// </summary>
        /// <returns></returns>
        public IActionResult Magistr()
        {
            searchTemplate = "магистратура";

            GetDataForTables(ref priemKolMest, ref PriemExam, ref priemKolTarget, ref blankNum, searchTemplate);

            ViewData["priemKolMest"] = priemKolMest;
            ViewData["PriemExam"] = PriemExam;
            ViewData["priemKolTarget"] = priemKolTarget;
            ViewData["blankNum"] = blankNum;

            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по аспирантуре
        /// </summary>
        /// <returns></returns>
        public IActionResult Postgraduate()
        {
            searchTemplate = "аспирантура";

            GetDataForTables(ref priemKolMest, ref PriemExam, ref priemKolTarget, ref blankNum, searchTemplate);

            ViewData["priemKolMest"] = priemKolMest;
            ViewData["PriemExam"] = PriemExam;
            ViewData["priemKolTarget"] = priemKolTarget;
            ViewData["blankNum"] = blankNum;

            return View();
        }
        #endregion
    }
}