namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Перечисление режимов работы с данными по НИР
    /// </summary>
    public enum CreateOrEditNirDataModeEnum
    {
        /// <summary>
        /// Сохранение данных и перенаправление к списку
        /// </summary>
        Saving = 0,
        /// <summary>
        /// Отмена сохранения и удаление временных данных
        /// </summary>
        Canceling = 1,
        /// <summary>
        /// Добавление нового автора
        /// </summary>
        AddingAuthor =11,
        /// <summary>
        /// Редактирование автора
        /// </summary>
        EditingAuthor=12,
        /// <summary>
        /// Удаление автора
        /// </summary>
        RemovingAuthor=13,
        /// <summary>
        /// Установка фильтра фамилий авторов
        /// </summary>
        ApplyAuthorFilter=14,
        /// <summary>
        /// Добавление научной специальности
        /// </summary>
        AddingNirSpecial=21,
        /// <summary>
        /// Редактирование научной специальности
        /// </summary>
        EditingNirSpecial=22,
        /// <summary>
        /// Удаление научной специальности
        /// </summary>
        RemovingNirSpecial=23,
        /// <summary>
        /// Добавление темы НИР
        /// </summary>
        AddingNirTema = 31,
        /// <summary>
        /// Редактирование темы НИР
        /// </summary>
        EditingNirTema = 32,
        /// <summary>
        /// Удаление темы НИР
        /// </summary>
        RemovingNirTema = 33,
        /// <summary>
        /// Добавление базы цитирования
        /// </summary>
        AddingCitationBase = 41,
        /// <summary>
        /// Редактирование базы цитирования
        /// </summary>
        EditingCitationBase = 42,
        /// <summary>
        /// Удаление базы цитирования
        /// </summary>
        RemovingCitationBase = 43,
    }
}
