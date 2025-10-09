namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    /// <summary>
    /// Модель фильтра учебных пособий
    /// </summary>
    public class UchPosobieFilterModel
    {
        /// <summary>
        /// Год издания
        /// </summary>
        public string GodIzdaniya { get; set; } = "";
        /// <summary>
        /// Фрагмент библиографического описания
        /// </summary>
        public string BiblFragment { get; set; } = "";
    }
}