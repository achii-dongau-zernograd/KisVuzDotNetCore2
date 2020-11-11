using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Infrastructure
{
    /// <summary>
    /// Число записей в таблицах
    /// </summary>
    public class NumRowsInDataTables
    {
        /// <summary>
        /// Количество пользователей, зарегистрированных в системе
        /// </summary>
        public int AppUsersNum { get; set; }

        /// <summary>
        /// Количество наименований учебных дисциплин
        /// </summary>
        public int DisciplinesNamesNum { get; set; }

        /// <summary>
        /// Количество преподавателей
        /// </summary>
        public int TeachersNum { get; set; }

        /// <summary>
        /// Количество студентов
        /// </summary>
        public int StudentsNum { get; set; }

        /// <summary>
        /// Количество студенческих групп
        /// </summary>
        public int StudentGroupsNum { get; set; }

        /// <summary>
        /// Количество пользовательских сообщений
        /// </summary>
        public int UserMessagesNum { get; set; }

        /// <summary>
        /// Количество сообщений для учебных групп
        /// </summary>
        public int MessagesFromAppUsersToStudentGroupsNum { get; set; }

        /// <summary>
        /// Количество учебных пособий, изданных вузом
        /// </summary>
        public int UchPosobiesNum { get; set; }

        /// <summary>
        /// Количество научных статей
        /// </summary>
        public int ArticlesNum { get; set; }

        /// <summary>
        /// Количество неподтверждённых научных статей
        /// </summary>
        public int ArticlesNotConfirmedNum { get; set; }

        /// <summary>
        /// Количество патентов и свидетельств
        /// </summary>
        public int PatentsNum { get; set; }

        /// <summary>
        /// Количество неподтверждённых патентов и свидетельств
        /// </summary>
        public int PatentsNotConfirmedNum { get; set; }

        /// <summary>
        /// Количество монографий
        /// </summary>
        public int MonografsNum { get; set; }

        /// <summary>
        /// Количество неподтверждённых монографий
        /// </summary>
        public int MonografsNotConfirmedNum { get; set; }


        /// <summary>
        /// Количество научных журналов
        /// </summary>
        public int ScienceJournalsNum { get; set; }

        /// <summary>
        /// Количество заявок на добавление научных журналов
        /// </summary>
        public int ScienceJournalAddingClaimsNum { get; set; }

        /// <summary>
        /// Количество неподтверждённых заявок на добавление научных журналов
        /// </summary>
        public int ScienceJournalAddingClaimsNotConfirmedNum { get; set; }
        
        /// <summary>
        /// Количество работ пользователей
        /// </summary>
        public int UserWorksNum { get; internal set; }
    }
}
