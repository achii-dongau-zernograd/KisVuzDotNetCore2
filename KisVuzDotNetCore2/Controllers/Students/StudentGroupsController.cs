using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Query;
using KisVuzDotNetCore2.Models.Struct;
using KisVuzDotNetCore2.Models.Users;

namespace KisVuzDotNetCore2.Controllers.Students
{
    [Authorize(Roles = "Администраторы, Деканат факультета ЭиУТ, Деканат факультета СПО, Деканат инженерно-технологического факультета, Деканат энергетического факультета")]
    public class StudentGroupsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        private IIncludableQueryable<StructFacultet, StructSubvision> _structFacultets => _context.StructFacultets.Include(f=>f.StructSubvision);
        
        private IIncludableQueryable<StudentGroup,AppUser> _studentGroups => _context.StudentGroups
                .Include(g => g.EduForm)
                .Include(g => g.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(g => g.EduKurs)
                .Include(g => g.Kurator.AppUser)
                .Include(g => g.StructFacultet.StructSubvision)
                .Include(g => g.Students)
                    .ThenInclude(s => s.AppUser);        

        public StudentGroupsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает объект факультета по переданному id
        /// </summary>
        /// <param name="id">УИД факультета</param>
        /// <returns></returns>
        private async Task<StructFacultet> GetStructFacultetById(int? id)
        {
            if (id == null) return null;
            return await _structFacultets.SingleOrDefaultAsync(f => f.StructFacultetId == id);
        }

        /// <summary>
        /// Возвращает объект студенческой группы по переданному id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<StudentGroup> GetStudentGroupById(int? id)
        {
            if (id == null) return null;
            return await _studentGroups.SingleOrDefaultAsync(f => f.StudentGroupId == id);
        }

        // GET: StudentGroups
        public async Task<IActionResult> Index()
        {            
            return View(await _studentGroups.ToListAsync());
        }

        public async Task<IActionResult> StudentGroupsOfFacultet(int? StructFacultetId)
        {
            if (StructFacultetId == null) return NotFound();

            var structFacultet = await GetStructFacultetById((int)StructFacultetId);
            if (structFacultet == null) return NotFound();

            ViewData["structFacultet"] = structFacultet;

            var studentGroupsOfFacultet = _studentGroups.Where(g=>g.StructFacultetId == StructFacultetId);
            return View(await studentGroupsOfFacultet.ToListAsync());
        }

        // GET: StudentGroups/Details/5
        public async Task<IActionResult> Details(int? id, int? StructFacultetId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGroup = await _context.StudentGroups
                .Include(s => s.EduForm)
                .Include(s => s.EduKurs)
                .Include(s => s.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(s => s.Kurator.AppUser)
                .Include(s => s.StructFacultet.StructSubvision)
                .SingleOrDefaultAsync(m => m.StudentGroupId == id);
            if (studentGroup == null)
            {
                return NotFound();
            }

            ViewBag.StructFacultetId = StructFacultetId;

            return View(studentGroup);
        }

        // GET: StudentGroups/Create
        public IActionResult Create()
        {
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursName");
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            ViewData["KuratorId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio");
            ViewData["StructFacultetId"] = new SelectList(_context.StructFacultets.Include(sf=>sf.StructSubvision), "StructFacultetId", "StructSubvision.StructSubvisionName");
            return View();
        }

        // POST: StudentGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentGroupId,StudentGroupNamePrefix,StudentGroupNumber,EduProfileId,EduFormId,EduKursId,StructFacultetId,KuratorId")] StudentGroup studentGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", studentGroup.EduFormId);
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursId", studentGroup.EduKursId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", studentGroup.EduProfileId);
            ViewData["KuratorId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", studentGroup.KuratorId);
            ViewData["StructFacultetId"] = new SelectList(_context.StructFacultets, "StructFacultetId", "StructFacultetId", studentGroup.StructFacultetId);
            return View(studentGroup);
        }

        public async Task<IActionResult> CreateByDekanat(int? StructFacultetId)
        {
            if (StructFacultetId == null) return NotFound();

            var structFacultet = await GetStructFacultetById((int)StructFacultetId);
            if (structFacultet == null) return NotFound();

            ViewData["structFacultet"] = structFacultet;

            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursNumber");
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            ViewData["KuratorId"] = new SelectList(_context.Teachers.OrderBy(t=>t.TeacherFio), "TeacherId", "TeacherFio");           
            return View();
        }
        
        // POST: StudentGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByDekanat([Bind("StudentGroupId,StudentGroupNamePrefix,StudentGroupNumber,EduProfileId,EduFormId,EduKursId,StructFacultetId,KuratorId")] StudentGroup studentGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(StudentGroupsOfFacultet),new { studentGroup.StructFacultetId });
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", studentGroup.EduFormId);
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursId", studentGroup.EduKursId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", studentGroup.EduProfileId);
            ViewData["KuratorId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", studentGroup.KuratorId);
            ViewData["StructFacultetId"] = new SelectList(_context.StructFacultets, "StructFacultetId", "StructFacultetId", studentGroup.StructFacultetId);
            return View(studentGroup);
        }

        // GET: StudentGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGroup = await _context.StudentGroups.SingleOrDefaultAsync(m => m.StudentGroupId == id);
            if (studentGroup == null)
            {
                return NotFound();
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", studentGroup.EduFormId);
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursName", studentGroup.EduKursId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", studentGroup.EduProfileId);
            ViewData["KuratorId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio", studentGroup.KuratorId);
            ViewData["StructFacultetId"] = new SelectList(_context.StructFacultets.Include(f=>f.StructSubvision), "StructFacultetId", "StructSubvision.StructSubvisionName", studentGroup.StructFacultetId);
            return View(studentGroup);
        }

        // POST: StudentGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentGroupId,StudentGroupNamePrefix,StudentGroupNumber,EduProfileId,EduFormId,EduKursId,StructFacultetId,KuratorId")] StudentGroup studentGroup)
        {
            if (id != studentGroup.StudentGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentGroupExists(studentGroup.StudentGroupId))
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
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", studentGroup.EduFormId);
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursId", studentGroup.EduKursId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", studentGroup.EduProfileId);
            ViewData["KuratorId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", studentGroup.KuratorId);
            ViewData["StructFacultetId"] = new SelectList(_context.StructFacultets, "StructFacultetId", "StructFacultetId", studentGroup.StructFacultetId);
            return View(studentGroup);
        }

        // GET: StudentGroups/Edit/5
        public async Task<IActionResult> EditByDekanat(int? id, int? StructFacultetId)
        {
            if (id == null || StructFacultetId == null)
            {
                return NotFound();
            }

            var studentGroup = await GetStudentGroupById(id);
            if (studentGroup == null)
            {
                return NotFound();
            }

            var structFacultet = await GetStructFacultetById(StructFacultetId);
            if(structFacultet==null)
            {
                return NotFound();
            }
            ViewData["StructFacultet"] = structFacultet;

            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", studentGroup.EduFormId);
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursNumber", studentGroup.EduKursId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", studentGroup.EduProfileId);
            ViewData["KuratorId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio", studentGroup.KuratorId);
            
            return View(studentGroup);
        }

        // POST: StudentGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditByDekanat(int id, [Bind("StudentGroupId,StudentGroupNamePrefix,StudentGroupNumber,EduProfileId,EduFormId,EduKursId,StructFacultetId,KuratorId")] StudentGroup studentGroup)
        {
            if (id != studentGroup.StudentGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentGroupExists(studentGroup.StudentGroupId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(StudentGroupsOfFacultet),new { studentGroup.StructFacultetId });
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", studentGroup.EduFormId);
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursId", studentGroup.EduKursId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", studentGroup.EduProfileId);
            ViewData["KuratorId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", studentGroup.KuratorId);
            ViewData["StructFacultetId"] = new SelectList(_context.StructFacultets, "StructFacultetId", "StructFacultetId", studentGroup.StructFacultetId);
            return View(studentGroup);
        }

        // GET: StudentGroups/Delete/5
        public async Task<IActionResult> Delete(int? id, int? StructFacultetId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGroup = await GetStudentGroupById(id);
            if (studentGroup == null)
            {
                return NotFound();
            }

            ViewBag.StructFacultetId = StructFacultetId;

            return View(studentGroup);
        }

        // POST: StudentGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? StructFacultetId)
        {
            var studentGroup = await _context.StudentGroups
                .Include(sg=>sg.Vedomosti)
                    .ThenInclude(v=>v.VedomostStudentMarks)
                .Include(sg=>sg.Students)
                    .ThenInclude(s=>s.AppUser)
                        .ThenInclude(au=>au.AppUserStructSubvisions)
                .Include(sg => sg.Students)
                    .ThenInclude(s => s.AppUser)
                        .ThenInclude(au => au.Teachers)
                .Include(sg => sg.Students)
                    .ThenInclude(s => s.AppUser)
                        .ThenInclude(au => au.Students)                
                .SingleOrDefaultAsync(sg => sg.StudentGroupId == id);

            //var appUserIds = studentGroup.Students.Select(s => s.AppUserId);

            _context.StudentGroups.Remove(studentGroup);
            await _context.SaveChangesAsync();

            if(StructFacultetId!=null)
            {
                return RedirectToAction(nameof(StudentGroupsOfFacultet),new { StructFacultetId });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }            
        }

        public IActionResult SendMessagesToStudents(int id, int? StructFacultetId)
        {
            ViewBag.id = id;
            ViewBag.StructFacultetId = StructFacultetId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessagesToStudents(int id, int? StructFacultetId, string message)
        {
            var userSenderId = _context.Users.FirstOrDefault(u=>u.UserName == User.Identity.Name).Id;

            var students = _context.Students.Where(s => s.StudentGroupId == id).ToList();

            var messages = new List<UserMessage>();
            foreach (var student in students)
            {
                var userMessage = new UserMessage { UserSenderId = userSenderId,
                    UserReceiverId = student.AppUserId,
                    UserMessageText = message,
                    UserMessageDate = DateTime.Now
                };

                messages.Add(userMessage);
            }

            _context.UserMessages.AddRange(messages);
            _context.SaveChanges();

            if (StructFacultetId != null)
            {
                return RedirectToAction(nameof(StudentGroupsOfFacultet), new { StructFacultetId });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }


        private bool StudentGroupExists(int id)
        {
            return _context.StudentGroups.Any(e => e.StudentGroupId == id);
        }
    }
}
