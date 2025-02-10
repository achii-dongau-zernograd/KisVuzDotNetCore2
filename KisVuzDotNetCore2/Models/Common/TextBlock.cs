namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Блок текста на веб-странице
    /// </summary>
    public class TextBlock
    {
        /// <summary>
        /// УИД текстового блока
        /// </summary>
        public int TextBlockId { get; set; }

        /// <summary>
        /// Тэг текстового блока
        /// </summary>
        public string TextBlockTag { get; set; }

        /// <summary>
        /// Содержимое текстового блока
        /// </summary>
        public string TextBlockText { get; set; }
    }
}
