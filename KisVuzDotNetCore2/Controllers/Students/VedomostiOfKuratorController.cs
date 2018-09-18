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

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы, Преподаватели")]
    public class VedomostiOfKuratorController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private IStudentRepository _studentRepository;

        public VedomostiOfKuratorController(AppIdentityDBContext context,
            IStudentRepository studentRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
        }

        // GET: Vedomosti
        public async Task<IActionResult> Index()
        {
            // Курируемые группы
            var studentGroupsOfKurator = await _studentRepository.GetStudentGroupsOfKuratorByUserNameAsync(User.Identity.Name);
            if (studentGroupsOfKurator == null) return NotFound();                       

            return View(studentGroupsOfKurator);            
        }        
        
        // GET: Vedomosti/Create
        public IActionResult Create(int StudentGroupId)
        {
            if (StudentGroupId == 0) return NotFound();

            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName");
            ViewData["SemestrNameId"] = new SelectList(_context.SemestrNames, "SemestrNameId", "SemestrNameName");
            ViewData["StudentGroupId"] = StudentGroupId;
            return View();
        }

        // POST: Vedomosti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VedomostId,EduYearId,StudentGroupId,SemestrNameId,DisciplineName")] Vedomost vedomost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vedomost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName", vedomost.EduYearId);
            ViewData["SemestrNameId"] = new SelectList(_context.SemestrNames, "SemestrNameId", "SemestrNameName", vedomost.SemestrNameId);
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups.Include(g => g.EduKurs), "StudentGroupId", "StudentGroupName", vedomost.StudentGroupId);
            return View(vedomost);
        }

        /// <summary>
        /// Сформировать список студентов с оценками для текущей ведомости
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateVedomostStudentMarks(int id)
        {
            var vedomost = await _context.Vedomosti
                .Include(v => v.EduYear)
                .Include(v => v.SemestrName)
                .Include(v => v.StudentGroup.EduKurs)
                .Include(v => v.StudentGroup.Students)
                .Include(v => v.VedomostStudentMarks)
                .SingleOrDefaultAsync(v=>v.VedomostId == id);

            if(vedomost!=null && vedomost.VedomostStudentMarks.Count==0)
            {
                foreach (var student in vedomost.StudentGroup.Students)
                {
                    var studentMark = new VedomostStudentMark();
                    studentMark.StudentId = student.StudentId;
                    studentMark.VedomostStudentMarkNameId = (int)VedomostStudentMarkNameEnum.NeYavNeAtt;
                    vedomost.VedomostStudentMarks.Add(studentMark);                    
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(VedomostStudentMarks), new { id });
        }

        /// <summary>
        /// Передаёт в представление таблицу с оценками по текущей ведомости
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> VedomostStudentMarks(int id)
        {
            var vedomost = await _context.Vedomosti
                .Include(v => v.EduYear)
                .Include(v => v.SemestrName)
                .Include(v => v.StudentGroup.EduKurs)
                .Include(v => v.StudentGroup.Students)
                .Include(v => v.VedomostStudentMarks)
                    .ThenInclude(m=>m.VedomostStudentMarkName)
                .SingleOrDefaultAsync(v => v.VedomostId == id);

            if (vedomost != null)
            {
                return View(vedomost);
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Добавление строки в ведомость
        /// </summary>
        /// <param name="id">УИД ведомости</param>
        /// <returns></returns>
        public async Task<IActionResult> VedomostStudentMarksAddRow(int id)
        {
            // Определяем группу, для которой составлена ведомость
            var studentGroup = await _context.Vedomosti.Where(v => v.VedomostId == id).SingleOrDefaultAsync();

            // Составляем полный список студентов группы, для которой составлена ведомость
            var students = await _context.Students.Where(s => s.StudentGroupId == studentGroup.StudentGroupId).ToListAsync();

            // Получаем уже имеющиеся записи ведомости
            var editingStudentMark = await _context.VedomostStudentMarks
                .Include(m => m.VedomostStudentMarkName)
                .Include(m => m.Student.StudentGroup.EduKurs)
                .Include(m => m.Vedomost.EduYear)
                .Include(m => m.Vedomost.SemestrName)
                .Where(m => m.VedomostId == id).ToListAsync();

            var addedStudentsIds = editingStudentMark.Select(m=>m.StudentId).ToList();

            // Если уже имеются какие-либо записи,
            // исключаем студентов из списка,
            // чтобы избежать дублирования
            if (editingStudentMark.Count>0)
            {
                foreach (var addedStudentsId in addedStudentsIds)
                {
                    var addedStudent = students.Where(s => s.StudentId == addedStudentsId).SingleOrDefault();
                    if(addedStudent!=null)
                    {
                        students.Remove(addedStudent);
                    }                    
                }
            }

            if(students.Count>0)
            {
                ViewBag.students = new SelectList(students,
                "StudentId",
                "StudentFio");
            }
            
            var vedomost = await _context.Vedomosti
                .Include(v => v.EduYear)
                .Include(v => v.SemestrName)
                .Include(v => v.StudentGroup.EduKurs)
                .Include(v => v.StudentGroup.Students)
                .Include(v => v.VedomostStudentMarks)
                    .ThenInclude(m => m.VedomostStudentMarkName)
                .SingleOrDefaultAsync(v => v.VedomostId == id);
            ViewBag.vedomost = vedomost;

            if (vedomost == null)
            {
                return NotFound();
            }

            ViewBag.VedomostStudentMarkNameId = new SelectList(_context.VedomostStudentMarkNames,
                "VedomostStudentMarkNameId",
                "VedomostStudentMarkNameName");

            var vedomostStudentMark = new VedomostStudentMark();
            vedomostStudentMark.VedomostId = id;

            return View(vedomostStudentMark);
        }

        [HttpPost]
        public async Task<IActionResult> VedomostStudentMarksAddRow(VedomostStudentMark vedomostStudentMark)
        {
            _context.VedomostStudentMarks.Add(vedomostStudentMark);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(VedomostStudentMarks), new { id = vedomostStudentMark.VedomostId });
        }

        /// <summary>
        /// Редактирование оценки студента в ведомости
        /// </summary>
        /// <param name="id">УИД ведомости</param>
        /// <param name="StudentMarkId">УИД оценки студента в ведомости</param>
        /// <returns></returns>
        public async Task<IActionResult> VedomostStudentMarksEditRow(int id, int VedomostStudentMarkId)
        {
            var editingStudentMark = await _context.VedomostStudentMarks
                .Include(m => m.VedomostStudentMarkName)
                .Include(m => m.Student.StudentGroup.EduKurs)
                .Include(m => m.Vedomost.EduYear)
                .Include(m => m.Vedomost.SemestrName)
                .Where(m => m.VedomostId == id && m.VedomostStudentMarkId == VedomostStudentMarkId)
                .SingleOrDefaultAsync();

            ViewBag.VedomostStudentMarkNameId = new SelectList(_context.VedomostStudentMarkNames,
                "VedomostStudentMarkNameId",
                "VedomostStudentMarkNameName",
                editingStudentMark.VedomostStudentMarkNameId);

            return View(editingStudentMark);
        }
                

        [HttpPost]
        public async Task<IActionResult> VedomostStudentMarksEditRow(VedomostStudentMark vedomostStudentMark)
        {
            var editingStudentMark = await _context.VedomostStudentMarks                
                .Where(m => m.VedomostStudentMarkId == vedomostStudentMark.VedomostStudentMarkId)
                .SingleOrDefaultAsync();

            if(editingStudentMark!=null)
            {
                editingStudentMark.VedomostStudentMarkNameId = vedomostStudentMark.VedomostStudentMarkNameId;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(VedomostStudentMarks), new { id = vedomostStudentMark.VedomostId });
        }

        /// <summary>
        /// Удаление оценки студента в ведомости
        /// </summary>
        /// <param name="id"></param>
        /// <param name="VedomostStudentMarkId"></param>
        /// <returns></returns>
        public async Task<IActionResult> VedomostStudentMarksDeleteRow(int id, int VedomostStudentMarkId)
        {
            var deletingStudentMark = await _context.VedomostStudentMarks
                .Include(m => m.VedomostStudentMarkName)
                .Include(m => m.Student.StudentGroup.EduKurs)
                .Include(m => m.Vedomost.EduYear)
                .Include(m => m.Vedomost.SemestrName)
                .Where(m => m.VedomostId == id && m.VedomostStudentMarkId == VedomostStudentMarkId)
                .SingleOrDefaultAsync();                        

            return View(deletingStudentMark);
        }

        [HttpPost]
        public async Task<IActionResult> VedomostStudentMarksDeleteRow(VedomostStudentMark vedomostStudentMark)
        {
            var deletingStudentMark = await _context.VedomostStudentMarks
                .Where(m => m.VedomostStudentMarkId == vedomostStudentMark.VedomostStudentMarkId)
                .SingleOrDefaultAsync();

            if (deletingStudentMark != null)
            {
                _context.VedomostStudentMarks.Remove(deletingStudentMark);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(VedomostStudentMarks), new { id = vedomostStudentMark.VedomostId });
        }


        // GET: Vedomosti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vedomost = await _context.Vedomosti.SingleOrDefaultAsync(m => m.VedomostId == id);
            if (vedomost == null)
            {
                return NotFound();
            }
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName", vedomost.EduYearId);
            ViewData["SemestrNameId"] = new SelectList(_context.SemestrNames, "SemestrNameId", "SemestrNameName", vedomost.SemestrNameId);
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups.Include(g => g.EduKurs), "StudentGroupId", "StudentGroupName", vedomost.StudentGroupId);
            return View(vedomost);
        }

        // POST: Vedomosti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VedomostId,EduYearId,StudentGroupId,SemestrNameId,DisciplineName")] Vedomost vedomost)
        {
            if (id != vedomost.VedomostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vedomost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VedomostExists(vedomost.VedomostId))
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
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName", vedomost.EduYearId);
            ViewData["SemestrNameId"] = new SelectList(_context.SemestrNames, "SemestrNameId", "SemestrNameName", vedomost.SemestrNameId);
            ViewData["StudentGroupId"] = new SelectList(_context.StudentGroups.Include(g => g.EduKurs), "StudentGroupId", "StudentGroupName", vedomost.StudentGroupId);
            return View(vedomost);
        }

        // GET: Vedomosti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vedomost = await _context.Vedomosti
                .Include(v => v.EduYear)
                .Include(v => v.SemestrName)
                .Include(v => v.StudentGroup.EduKurs)
                .SingleOrDefaultAsync(m => m.VedomostId == id);
            if (vedomost == null)
            {
                return NotFound();
            }

            return View(vedomost);
        }

        // POST: Vedomosti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vedomost = await _context.Vedomosti.SingleOrDefaultAsync(m => m.VedomostId == id);
            _context.Vedomosti.Remove(vedomost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VedomostExists(int id)
        {
            return _context.Vedomosti.Any(e => e.VedomostId == id);
        }
    }
}
