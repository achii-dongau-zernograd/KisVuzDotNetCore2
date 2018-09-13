using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace KisVuzDotNetCore2.Models.Students
{
    /// <summary>
    /// Хранилище данных о студентах
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        private AppIdentityDBContext _context;
        

        public StudentRepository(AppIdentityDBContext context)
        {
            _context = context;            
        }

        /// <summary>
        /// Список студентов
        /// </summary>
        public IIncludableQueryable<Student, EduKurs> Students => _context.Students
            .Include(s => s.AppUser)
            .Include(s => s.StudentGroup.EduKurs);

        public IIncludableQueryable<StudentGroup, AppUser> StudentGroups => _context.StudentGroups
                .Include(g => g.EduForm)
                .Include(g => g.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(g => g.EduKurs)
                .Include(g => g.Kurator.AppUser)
                .Include(g => g.StructFacultet.StructSubvision)
                .Include(g => g.Students)
                    .ThenInclude(s => s.AppUser);

        /// <summary>
        /// Создаёт объект студента с аккаунтом
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<Student> AddStudentAsync(Student student)
        {            
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        /// <summary>
        /// Возвращает объект Student по переданному идентификатору студента
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<Student> GetStudentByIdAsync(int? studentId)
        {
            var student = _context.Students
                .Include(s => s.AppUser)
                .Include(s => s.StudentGroup.EduKurs)                
                .SingleOrDefaultAsync(m => m.StudentId == studentId);
            return student;
        }

        /// <summary>
        /// Возвращает StudentGroup с заполненным списком студентов
        /// </summary>
        /// <param name="studentGroupId"></param>
        public async Task<StudentGroup> GetStudentGroupByIdAsync(int? studentGroupId)
        {
            var studentGroup=await StudentGroups
                .SingleOrDefaultAsync(g=>g.StudentGroupId == studentGroupId);
            return studentGroup;
        }

        /// <summary>
        /// Возвращает курируемые группы пользователя с заданным именем
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<List<StudentGroup>> GetStudentGroupsOfKuratorByUserNameAsync(string userName)
        {
            var user = await _context.Users
                .Include(u=>u.Teachers)
                    .ThenInclude(t=>t.StudentGroupsOfKurator)
                        .ThenInclude(g=>g.Students)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.StudentGroupsOfKurator)
                        .ThenInclude(g => g.EduKurs)
                .Where(u => u.UserName == userName)
                .SingleOrDefaultAsync() as AppUser;

            var studentGroupList = new List<StudentGroup>();

            foreach (var teacher in user.Teachers)
            {
                studentGroupList.AddRange(teacher.StudentGroupsOfKurator);
            }

            return studentGroupList;
        }
    }
}
