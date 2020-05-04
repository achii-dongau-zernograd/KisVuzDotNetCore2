namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Интерфейс "Абитуриент"
    /// </summary>
    public interface IAbiturRepository
    {
        /// <summary>
        /// Определяет, является ли пользователь абитуриентом
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsAbitur(string userName);

        /// <summary>
        /// Проверяет наличие у абитуриента загруженного заявления
        /// на обработку персональных данных
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsLoadedFileApplicationForProcessingPersonalData(string userName);

        /// <summary>
        /// Проверяет наличие у абитуриента загруженных
        /// документов об образовании
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsLoadedFileEducationDocuments(string userName);
    }
}
