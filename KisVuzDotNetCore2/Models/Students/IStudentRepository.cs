using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Students;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Students
{
    /// <summary>
    /// Интерфейс "Хранилище данных о студентах"
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// Список студентов
        /// </summary>
        IIncludableQueryable<Student, EduKurs> Students { get; }

        /// <summary>
        /// Студенческие группы
        /// </summary>
        IIncludableQueryable<StudentGroup, AppUser> StudentGroups { get; }

        /// <summary>
        /// Возвращает StudentGroup с заполненным списком студентов
        /// </summary>
        /// <param name="studentGroupId">УИД студ. группы</param>
        Task<StudentGroup> GetStudentGroupByIdAsync(int? studentGroupId);
        Task<Student> AddStudentAsync(Student student);
        Task<Student> GetStudentByIdAsync(int? studentId);
    }
}
