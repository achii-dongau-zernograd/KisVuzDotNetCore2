using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.MTO;
using KisVuzDotNetCore2.Models.Struct;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Интерфейс руководителя структурного подразделения
    /// </summary>
    public interface IStructSubvisionChiefRepository
    {
        /// <summary>
        /// Определяет, является ли пользователь руководителем структурного подразделения
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsStructSubvisionChief(string userName);

        /// <summary>
        /// Возвращает модель представления для работы с помещением с указанным УИД
        /// </summary>
        /// <param name="pomeshenieId">УИД помещения</param>
        /// <returns></returns>
        PomeshenieViewModel GetPomeshenieViewModel(int? pomeshenieId);

        /// <summary>
        /// Возвращает объект кафедры по переданному имени пользователя-руководителя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        StructKaf GetKafedra(string userName);

        /// <summary>
        /// Возвращает список преподавателей кафедры. Кафедра определяется по имени пользователя-руководителя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<Teacher> GetTeachersOfKafedra(string userName);
        void UpdateTeacherDisciplineName(TeacherEduProfileDisciplineName teacherDisciplineNameEntry);
        void AddTeacherDisciplineName(TeacherEduProfileDisciplineName teacherDisciplineNameEntry);
        void RemoveTeacherDisciplineName(TeacherEduProfileDisciplineName teacherDisciplineNameEntry);
    }
}
