namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Перечисление типов работ пользователя (эссе, курсовые, ВКР и пр.)
    /// </summary>
    public enum UserWorkTypeEnum
    {
        /// <summary>
        /// Эссе
        /// </summary>
        Esse=1,
        /// <summary>
        /// Реферат
        /// </summary>
        Referat=2,
        /// <summary>
        /// Сочинение
        /// </summary>
        Sochinenie=3,
        /// <summary>
        /// Перевод текста с иностранного языка / на иностранный язык
        /// </summary>
        Perevod=4,
        /// <summary>
        /// Отчет по лабораторной работе
        /// </summary>
        OtchetLabRabota=5,
        /// <summary>
        /// Отчет по практике
        /// </summary>
        OtchetPractica=6,
        /// <summary>
        /// Отчет по самостоятельной работе
        /// </summary>
        OtchetSamostRabota=7,
        /// <summary>
        /// Курсовая работа
        /// </summary>
        KursRabota=8,
        /// <summary>
        /// Курсовой проект
        /// </summary>
        KursProekt=9,
        /// <summary>
        /// Выпускная квалификационная работа
        /// </summary>
        VKR=10,
        /// <summary>
        /// Другое
        /// </summary>
        Other=11

    }
}