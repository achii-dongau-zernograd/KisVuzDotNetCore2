namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Учебный год"
    /// </summary>
    public class EduYear
    {
        /// <summary>
        /// УИД учебного года
        /// </summary>
        public int EduYearId { get; set; }

        /// <summary>
        /// Наименование учебного года
        /// </summary>
        public string EduYearName { get; set; }
    }
}