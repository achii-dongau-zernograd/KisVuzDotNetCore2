using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Перечисление типов заданий, используемых в системе дистанционного образования
    /// </summary>
    public enum LmsTaskTypesEnum
    {
        /// <summary>
        /// Один правильный ответ
        /// </summary>
        OneCorrectAnswer = 1,
        /// <summary>
        /// Несколько правильных ответов
        /// </summary>
        SeveralCorrectAnswers = 2,
        /// <summary>
        /// Ввод текста
        /// </summary>
        InputText = 3,
        /// <summary>
        /// Ввод числа
        /// </summary>
        InputNumber = 4,
        /// <summary>
        /// Загрузка файла со скан-копией решения
        /// </summary>
        FileUpload = 5
    }
}
