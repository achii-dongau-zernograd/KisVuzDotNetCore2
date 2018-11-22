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
        /// Добавление нового автора
        /// </summary>
        AddingAuthor=11,
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
        AddingNirSpecial =21,
        /// <summary>
        /// Редактирование научной специальности
        /// </summary>
        EditingNirSpecial=22,
        /// <summary>
        /// Удаление научной специальности
        /// </summary>
        RemovingNirSpecial=23
    }
}
