using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Перечисление типов событий системы дистанционного образования
    /// </summary>
    public enum LmsEventTypesEnum
    {
        ///////////////////////// Приём ///////////////////////
        /// <summary>
        /// Приём - Вступительное испытание
        /// </summary>
        Priem_EntranceTest = 11,

        ///////// События, организовываемые в процессе обучения ///
        /// <summary>
        /// Лекция
        /// </summary>
        StudyingProccess_Lecture = 21,
        /// <summary>
        /// Практическое занятие
        /// </summary>
        StudyingProccess_PracticalLesson = 22,
        /// <summary>
        /// Лабораторная работа
        /// </summary>
        StudyingProccess_LaboratoryWork = 23,

        /////////////////// Текущая аттестация ////////////////
        /// <summary>
        /// Текущая аттестация - тест
        /// </summary>
        CurrentCertification_Test = 31,

        ///////////////// Промежуточная аттестация /////////////        
        /// <summary>
        /// Промежуточная аттестация - зачет
        /// </summary>
        IntermediateCertification_Midterm = 41,
        /// <summary>
        /// Промежуточная аттестация - зачет с оценкой
        /// </summary>
        IntermediateCertification_MidtermWithMark = 42,
        /// <summary>
        /// Промежуточная аттестация - экзамен
        /// </summary>
        IntermediateCertification_Exam = 43,

        ///////// События, организовываемые в процессе государственной итоговой аттестации (ГИА) /////////
        /// <summary>
        /// Государственная итоговая аттестация - защита выпускной квалификационной работы
        /// </summary>
        StateFinalCertification_FinalQualifyingWorkDefense = 51
        
    }
}
