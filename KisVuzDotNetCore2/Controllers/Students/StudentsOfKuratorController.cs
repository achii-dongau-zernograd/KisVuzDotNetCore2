using System;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models.Files;

namespace KisVuzDotNetCore2.Controllers.Students
{
    [Authorize(Roles = "Администраторы, Преподаватели")]
    public class StudentsOfKuratorController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private IStudentRepository _studentRepository;
        private readonly IFileModelRepository _fileModelRepository;

        private UserManager<AppUser> userManager;

        public StudentsOfKuratorController(AppIdentityDBContext context,
            IStudentRepository studentRepository,
            IFileModelRepository fileModelRepository,
            UserManager<AppUser> usrMgr)
        {
            _context = context;
            _studentRepository = studentRepository;
            _fileModelRepository = fileModelRepository;
            userManager = usrMgr;
        }               

        /// <summary>
        /// Списки студентов курируемых групп куратора
        /// </summary>
        /// <param name="StudentGroupId"></param>
        /// <param name="StructFacultetId"></param>
        /// <returns></returns>        
        public async Task<IActionResult> Index()
        {
            if (User.Identity.Name == null) return NotFound();
            var studentGroupsOfKurator = await _studentRepository.GetStudentGroupsOfKuratorByUserNameAsync(User.Identity.Name);
            if (studentGroupsOfKurator == null) return NotFound();

            return View(studentGroupsOfKurator);
        }
                

        #region Создание студента с аккаунтом и добавлением в группу
        // GET: Students/Create
        public async Task<IActionResult> CreateStudentWithAccount(int? StudentGroupId)
        {
            if (StudentGroupId == null) return NotFound();            
            var studentGroup = await _studentRepository.GetStudentGroupByIdAsync(StudentGroupId);
            if (studentGroup == null) return NotFound();
            ViewBag.StudentGroupId = studentGroup.StudentGroupId;            
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudentWithAccount(CreateStudentWithAccountModel student)
        {
            if (ModelState.IsValid)
            {
                // Создаём аккаунт студента
                AppUser studentAppUser = new AppUser();
                studentAppUser.UserName = student.Email;
                studentAppUser.Email = student.Email;
                studentAppUser.Birthdate = student.Birthdate;
                studentAppUser.FirstName = student.FirstName;
                studentAppUser.LastName = student.LastName;
                studentAppUser.Patronymic = student.Patronymic;
                studentAppUser.PhoneNumber = student.PhoneNumber;                

                IdentityResult result = await userManager.CreateAsync(studentAppUser, student.Password);

                if (result.Succeeded)
                {
                    // Создаём объект Student
                    Student newStudent = new Student();
                    newStudent.StudentFio = $"{student.LastName} {student.FirstName} {student.Patronymic}";
                    newStudent.ZachetnayaKnijkaNumber = student.ZachetnayaKnijkaNumber;
                    newStudent.AppUserId = studentAppUser.Id;
                    newStudent.StudentGroupId = student.StudentGroupId;
                    var addedStudent = await _studentRepository.AddStudentAsync(newStudent);
                                        
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }                
            }

            ViewBag.StudentGroupId = student.StudentGroupId;            
            return View(student);
        }
        #endregion

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentRepository.GetStudentByIdAsync(id);
            
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            Student student,
            IFormFile uploadFile)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadFile != null)
                    {
                        FileModel f = await _fileModelRepository.UploadRezultOsvoenObrazovatProgrAsync(student, uploadFile);
                        student.RezultOsvoenObrazovatProgrId = f.Id;
                    }
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["AppUserIds"] = new SelectList(_context.Users, "Id", "GetFullName", student.AppUserId);
            ViewData["StudentGroupIds"] = new SelectList(_context.StudentGroups.Include(g => g.EduKurs), "StudentGroupId", "StudentGroupName", student.StudentGroupId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id,
            int? StudentGroupId,
            int? StructFacultetId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            ViewBag.StructFacultetId = StructFacultetId;
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);            

            if(student != null)
            {
                if (student.AppUserId != null)
                {
                    AppUser user = await userManager.FindByIdAsync(student.AppUserId);

                    if (user != null)
                    {
                        IdentityResult result = await userManager.DeleteAsync(user);
                        if (!result.Succeeded)                        
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователь не найден");
                    }
                }
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("", "Студент не найден");
            }
                        
            return RedirectToAction(nameof(Index));
        }

        #region Вспомогательные методы
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        #endregion
    }
}
