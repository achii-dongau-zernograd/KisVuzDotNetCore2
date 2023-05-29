using KisVuzDotNetCore2.Models.MTO;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Репозиторий руководителя структурного подразделения
    /// </summary>
    public class StructSubvisionChiefRepository : IStructSubvisionChiefRepository
    {
        AppIdentityDBContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        public StructSubvisionChiefRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает модель представления для работы с помещением с указанным УИД
        /// </summary>
        /// <param name="pomeshenieId"></param>
        /// <returns></returns>
        public PomeshenieViewModel GetPomeshenieViewModel(int? pomeshenieId)
        {
            throw new NotImplementedException();
        }
               

        /// <summary>
        /// Определяет, является ли пользователь руководителем структурного подразделения
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsStructSubvisionChief(string userName)
        {
            bool isStructSubvisionChief = _context.StructSubvisions
                .Include(s=>s.StructSubvisionAccountChief)
                .FirstOrDefault(s=>s.StructSubvisionAccountChief.UserName==userName) != null;
            return isStructSubvisionChief;
        }

        /// <summary>
        /// Возвращает список преподавателей кафедры по userName зав. кафедрой
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<Teacher> GetTeachersOfKafedra(string userName)
        {
            var kaf = GetKafedra(userName);
            
            var teachersOfKafedra = kaf.TeacherStructKafPostStavka
                .Select(tskp => tskp.Teacher)
                .Distinct()
                .ToList();

            return teachersOfKafedra;
        }

        /// <summary>
        /// Возвращает объект кафедры по переданному имени пользователя-руководителя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public StructKaf GetKafedra(string userName)
        {
            string userNameId = _context.Users.Single(u => u.UserName == userName).Id;

            var kaf = _context.StructKafs
                .Include(k => k.StructSubvision)
                .Include(k => k.TeacherStructKafPostStavka)
                    .ThenInclude(t => t.Post)
                .Include(k => k.TeacherStructKafPostStavka)
                    .ThenInclude(tskps => tskps.Teacher.TeacherEduProfileDisciplineNames)
                        .ThenInclude(t => t.DisciplineName)
                .Include(k => k.TeacherStructKafPostStavka)
                    .ThenInclude(tskps => tskps.Teacher.TeacherEduProfileDisciplineNames)
                        .ThenInclude(t => t.EduProfile)
                            .ThenInclude(t => t.EduNapravl)
                                .ThenInclude(t => t.EduUgs)
                                    .ThenInclude(t => t.EduLevel)
                .FirstOrDefault(k => k.StructSubvision.StructSubvisionAccountChiefId == userNameId);

            return kaf;
        }

        /// <summary>
        /// Обновляет закрепление дисциплины за преподавателем
        /// </summary>
        /// <param name="teacherDisciplineNameEntry"></param>
        public void UpdateTeacherDisciplineName(TeacherEduProfileDisciplineName teacherDisciplineNameEntry)
        {
            _context.Update(teacherDisciplineNameEntry);
            _context.SaveChanges();
        }

        public void AddTeacherDisciplineName(TeacherEduProfileDisciplineName teacherDisciplineNameEntry)
        {
            _context.Add(teacherDisciplineNameEntry);
            _context.SaveChanges();
        }

        public void RemoveTeacherDisciplineName(TeacherEduProfileDisciplineName teacherDisciplineNameEntry)
        {
            _context.Remove(teacherDisciplineNameEntry);
            _context.SaveChanges();
        }
    }
}
