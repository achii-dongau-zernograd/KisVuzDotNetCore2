using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.MTO;
using KisVuzDotNetCore2.Models.Sveden;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace KisVuzDotNetCore2.Controllers.Users
{
    public class StructSubvisionChiefArmController : Controller
    {
        IStructSubvisionChiefRepository _structSubvisionChiefRepository;
        ISelectListRepository _selectListRepository;
        public StructSubvisionChiefArmController(IStructSubvisionChiefRepository structSubvisionChiefRepository,
            ISelectListRepository selectListRepository)
        {
            _structSubvisionChiefRepository = structSubvisionChiefRepository;
            _selectListRepository = selectListRepository;
        }

        public IActionResult Oborudovanie(int? pomeshenieId)
        {            
            PomeshenieViewModel pomeshenieViewModel = _structSubvisionChiefRepository.GetPomeshenieViewModel(pomeshenieId);
            List<Oborudovanie> oborudovanie=new List<Oborudovanie>();
            return View(oborudovanie);
        }

        public IActionResult TeacherDisciplineNames()
        {
            ViewBag.Kafedra = _structSubvisionChiefRepository.GetKafedra(User.Identity.Name);
            var viewModel = _structSubvisionChiefRepository.GetTeachersOfKafedra(User.Identity.Name);
            
            return View(viewModel);
        }

        public IActionResult TeacherDisciplineNamesEdit(int teacherId)
        {
            var teacher = _structSubvisionChiefRepository.GetTeachersOfKafedra(User.Identity.Name)
                .FirstOrDefault(t => t.TeacherId == teacherId);
            
            return View(teacher);
        }

        public IActionResult TeacherDisciplineNameCreate(int teacherId)
        {
            var teacher = _structSubvisionChiefRepository.GetTeachersOfKafedra(User.Identity.Name)
                .FirstOrDefault(t => t.TeacherId == teacherId);

            if (teacher == null)
                return NotFound();
            
            var teacherDisciplineNameEntry = new TeacherEduProfileDisciplineName { TeacherId = teacherId };

            ViewBag.Teacher = teacher;
            ViewBag.DisciplineNames = _selectListRepository.GetSelectListDisciplines();
            return View(teacherDisciplineNameEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TeacherDisciplineNameCreate(int teacherId,
            TeacherEduProfileDisciplineName teacherEduProfileDisciplineName)
        {
            var teacher = _structSubvisionChiefRepository.GetTeachersOfKafedra(User.Identity.Name)
                .FirstOrDefault(t => t.TeacherId == teacherId);

            if (teacher == null)
                return NotFound();

            ViewBag.Teacher = teacher;

            var teacherDisciplineNameEntry = new TeacherEduProfileDisciplineName();
            teacherDisciplineNameEntry.TeacherId = teacher.TeacherId;
            teacherDisciplineNameEntry.DisciplineNameId = teacherEduProfileDisciplineName.DisciplineNameId;
            _structSubvisionChiefRepository.AddTeacherDisciplineName(teacherDisciplineNameEntry);

            return RedirectToAction(nameof(TeacherDisciplineNamesEdit), new { teacherId = teacher.TeacherId });
        }


        public IActionResult TeacherDisciplineNameEdit(int teacherId, int teacherDisciplineNameId)
        {          
            var teacher = _structSubvisionChiefRepository.GetTeachersOfKafedra(User.Identity.Name)
                .FirstOrDefault(t => t.TeacherId == teacherId);

            if (teacher == null)
                return NotFound();

            ViewBag.Teacher = teacher;

            var teacherDisciplineNameEntry = teacher.TeacherEduProfileDisciplineNames
                .Single(td => td.TeacherEduProfileDisciplineNameId == teacherDisciplineNameId);

            ViewBag.DisciplineNames = _selectListRepository.GetSelectListDisciplines(teacherDisciplineNameEntry.DisciplineNameId);

            return View(teacherDisciplineNameEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TeacherDisciplineNameEdit(int teacherId,
            TeacherEduProfileDisciplineName teacherEduProfileDisciplineName)
        {            
            var teacher = _structSubvisionChiefRepository.GetTeachersOfKafedra(User.Identity.Name)
                .FirstOrDefault(t => t.TeacherId == teacherId);

            if (teacher == null)
                return NotFound();

            ViewBag.Teacher = teacher;

            var teacherDisciplineNameEntry = teacher.TeacherEduProfileDisciplineNames
                .Single(td => td.TeacherEduProfileDisciplineNameId == teacherEduProfileDisciplineName.TeacherEduProfileDisciplineNameId);
            ViewBag.DisciplineNames = _selectListRepository.GetSelectListDisciplines(teacherDisciplineNameEntry.DisciplineNameId);

            if (teacherDisciplineNameEntry != null)
            {
                teacherDisciplineNameEntry.DisciplineNameId = teacherEduProfileDisciplineName.DisciplineNameId;
                _structSubvisionChiefRepository.UpdateTeacherDisciplineName(teacherDisciplineNameEntry);

                return RedirectToAction(nameof(TeacherDisciplineNamesEdit), new { teacherId = teacher.TeacherId });
            }

            return View(teacherDisciplineNameEntry);
        }

        public IActionResult TeacherDisciplineNameDelete(int teacherId,
            int teacherDisciplineNameId)
        {
            var teacher = _structSubvisionChiefRepository.GetTeachersOfKafedra(User.Identity.Name)
                .FirstOrDefault(t => t.TeacherId == teacherId);

            if (teacher == null)
                return NotFound();

            ViewBag.Teacher = teacher;

            var teacherDisciplineNameEntry = teacher.TeacherEduProfileDisciplineNames
                .Single(td => td.TeacherEduProfileDisciplineNameId == teacherDisciplineNameId);                       

            return View(teacherDisciplineNameEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TeacherDisciplineNameDelete(TeacherEduProfileDisciplineName teacherDisciplineName)
        {
            var teacher = _structSubvisionChiefRepository.GetTeachersOfKafedra(User.Identity.Name)
                .FirstOrDefault(t => t.TeacherId == teacherDisciplineName.TeacherId);

            if (teacher == null)
                return NotFound();
                        
            var teacherDisciplineNameEntry = teacher.TeacherEduProfileDisciplineNames
                .Single(td => td.TeacherEduProfileDisciplineNameId == teacherDisciplineName.TeacherEduProfileDisciplineNameId);

            if(teacherDisciplineNameEntry !=null )
            {
                _structSubvisionChiefRepository.RemoveTeacherDisciplineName(teacherDisciplineNameEntry);
            }

            return RedirectToAction(nameof(TeacherDisciplineNamesEdit), new { teacherId = teacher.TeacherId });
        }
    }
}