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

        public AbiturController(AppIdentityDBContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IAbiturientRepository abiturRepository,
            IFileModelRepository fileModelRepository,
            IUserDocumentRepository userDocumentRepository,
            ISelectListRepository selectListRepository)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _abiturRepository = abiturRepository;
            _fileModelRepository = fileModelRepository;
            _userDocumentRepository = userDocumentRepository;
            _selectListRepository = selectListRepository;
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

            // Если пользователь не является абитуриентом - выводим соответствующее сообщение
            // и предлагаем стать абитуриентом
            if (!_abiturRepository.IsAbitur(User.Identity.Name))
                return View("NotAbiturMessageForm");

            // Проверяем наличие у абитуриента загруженного заявления
            // на обработку персональных данных
            if (!_abiturRepository.IsLoadedFileApplicationForProcessingPersonalData(User.Identity.Name))
                return View("LoadFileApplicationForProcessingPersonalData");                        

            // Проверяем наличие у абитуриента загруженного документа об образовании
            if (!_abiturRepository.IsLoadedFileEducationDocuments(User.Identity.Name))
                return RedirectToAction(nameof(LoadFileEducationDocument));

            //// Для все загруженных абитуриентом документов об образовании
            //// проверяем заполнение сведений
            //if (!_abiturRepository.IsEducationDocumentsDataEntered(User.Identity.Name))
            //    return RedirectToAction(nameof(EducationDocumentsDataEntering));

            // Проверяем наличие сведений об образовании
            if (! await _abiturRepository.IsUserEducationDataExists(User.Identity.Name))
                return RedirectToAction(nameof(CreateUserEducation));

            // Проверяем наличие у абитуриента загруженной скан-копии паспорта
            if (!_abiturRepository.IsLoadedFilePassport(User.Identity.Name))
                return RedirectToAction(nameof(LoadFilePassport));

            // Проверяем наличие паспортных данных
            if (!await _abiturRepository.IsPassportDataExistsAsync(User.Identity.Name))
                return RedirectToAction(nameof(CreatePassportData));

            // Проверяем наличие заявлений о приёме
            if (!_abiturRepository.IsApplicationForAdmissionExists(User.Identity.Name))
                return RedirectToAction(nameof(CreateApplicationForAdmission));

            ViewBag.Abiturient = await _abiturRepository.GetAbiturientAsync(User.Identity.Name);            
            ViewBag.SelectedTab = selectedTab;
            return View();
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

            return RedirectToAction(nameof(Start));
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

            return RedirectToAction(nameof(Start));
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
                ViewBag.EduNapravls = _selectListRepository.GetSelectListEduNapravlFullNamesOfEduLevel(eduLevelId);
                return View();
            }

            return RedirectToAction(nameof(CreateApplicationForAdmission), new { eduNapravlId });
        }

        public async Task<IActionResult> CreateApplicationForAdmission(int? eduNapravlId)
        {
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
            ViewBag.EduForms = _selectListRepository.GetSelectListEduFormsForAbiturient();
            ViewBag.QuotaTypes = _selectListRepository.GetSelectListQuotaTypes(applicationForAdmission.QuotaTypeId);

            return View(applicationForAdmission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApplicationForAdmission(ApplicationForAdmission applicationForAdmission)
        {
            await _abiturRepository.AddApplicationForAdmission(applicationForAdmission);

            return RedirectToAction(nameof(Start));
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

        /// <summary>
        /// Загрузка заявления
        /// на обработку персональных данных
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoadFileApplicationForProcessingPersonalData(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
                RedirectToAction(nameof(Start));

            UserDocument newUserDocument = await _userDocumentRepository
                .CreateApplicationForProcessingPersonalDataAsync(User.Identity.Name, uploadedFile);

            return RedirectToAction(nameof(Start));
        }

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
    }
}