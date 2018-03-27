namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Факс
    /// </summary>
    public class Fax
    {
        /// <summary>
        /// УИД факса
        /// </summary>
        public int FaxId { get; set; }

        /// <summary>
        /// Факс
        /// </summary>
        public string FaxValue { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string FaxComment { get; set; }
    }
}