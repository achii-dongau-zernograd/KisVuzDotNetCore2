namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Телефон
    /// </summary>
    public class Telephone
    {
        /// <summary>
        /// УИД телефона
        /// </summary>
        public int TelephoneId { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string TelephoneComment { get; set; }
    }
}