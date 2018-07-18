using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель связи TemaNir и EduProfiles
    /// </summary>
    public class NirTemaEduProfile
    {
        /// <summary>
        /// Идентификатор темы TemaNir и EduProfiles
        /// </summary>
        public int NirTemaEduProfileId { get; set; }

        /// <summary>
        /// Навигационные свойства
        /// </summary>
        public NirTema NirTema { get; set; }
        public int NirTemaId { get; set; }

        public EduProfile EduProfile { get; set; }
        public int EduProfileId { get; set; }
    }
}
