namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Перечисление статусов пользователей системы
    /// </summary>
    public enum AppUserStatusEnum
    {
        /// <summary>
        /// Новый пользователь
        /// </summary>
        NewUser = 1,
        /// <summary>
        /// Подтверждённый пользователь - абитуриент
        /// </summary>
        ConfirmedUser = 2,
        /// <summary>
        /// Аккаунт пользователя, помеченный к удалению
        /// </summary>
        ToDelete = 3
    }
}
