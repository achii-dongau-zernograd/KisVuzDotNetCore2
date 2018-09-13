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

namespace KisVuzDotNetCore2.Controllers.Students
{
    [Authorize(Roles = "Администраторы, Деканат факультета ЭиУТ, Деканат факультета СПО, Деканат инженерно-технологического факультета, Деканат энергетического факультета")]
    public class StudentsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private IStudentRepository _studentRepository;

        private UserManager<AppUser> userManager;

        public StudentsController(AppIdentityDBContext context,
            IStudentRepository studentRepository,
            UserManager<AppUser> usrMgr)
        {
            _context = context;
            _studentRepository = studentRepository;
            userManager = usrMgr;
        }

        // GET: Students
        public async Task<IActionResult> Index(int? StudentGroupId, int? StructFacultetId)
        {
            if(StudentGroupId!=null)
            {
                var studentGroup = await _studentRepository.GetStudentGroupByIdAsync(StudentGroupId);
                if (studentGroup == null) return NotFound();
                ViewData["studentGroup"] = studentGroup;
                ViewBag.StructFacultetId = StructFacultetId;
                return View();
            }
            
            return View(_studentRepository.Students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.AppUser)
                .Include(s => s.StudentGroup)
                .SingleOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "GetFullName");
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups.Include(g=>g.EduKurs), "StudentGroupId", "StudentGroupName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentFio,ZachetnayaKnijkaNumber,AppUserId,StudentGroupId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", student.AppUserId);
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups, "StudentGroupId", "StudentGroupId", student.StudentGroupId);
            return View(student);
        }

        #region Создание студента с аккаунтом и добавлением в группу
        // GET: Students/Create
        public async Task<IActionResult> CreateStudentWithAccount(int? StudentGroupId, int? StructFacultetId)
        {
            if (StudentGroupId == null) return NotFound();            
            var studentGroup = await _studentRepository.GetStudentGroupByIdAsync(StudentGroupId);
            if (studentGroup == null) return NotFound();
            ViewBag.StudentGroupId = studentGroup.StudentGroupId;
            ViewBag.StructFacultetId = StructFacultetId;
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudentWithAccount(CreateStudentWithAccountModel student,
            int? StructFacultetId)
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
                                        
                    return RedirectToAction(nameof(Index), new { student.StudentGroupId, StructFacultetId });
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
            ViewBag.StructFacultetId = StructFacultetId;
            return View(student);
        }
        #endregion

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id,
            int? StudentGroupId,
            int? StructFacultetId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            ViewBag.StudentGroupId = StudentGroupId;
            ViewBag.StructFacultetId = StructFacultetId;
            ViewData["AppUserIds"] = new SelectList(_context.Users, "Id", "GetFullName", student.AppUserId);
            ViewData["StudentGroupIds"] = new SelectList(_context.StudentGroups.Include(g=>g.EduKurs), "StudentGroupId", "StudentGroupName", student.StudentGroupId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentFio,ZachetnayaKnijkaNumber,AppUserId,StudentGroupId")] Student student,
            int? StudentGroupId,
            int? StructFacultetId)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index),new { StudentGroupId, StructFacultetId });
            }
            ViewBag.StudentGroupId = StudentGroupId;
            ViewBag.StructFacultetId = StructFacultetId;
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
        public async Task<IActionResult> DeleteConfirmed(int id,
            int? StudentGroupId,
            int? StructFacultetId)
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
                        
            return RedirectToAction(nameof(Index), new { StructFacultetId, StudentGroupId });
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
