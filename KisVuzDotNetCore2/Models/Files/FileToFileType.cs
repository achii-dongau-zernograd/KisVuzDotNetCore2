namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Определяет соответствие файла различным типам данных
    /// </summary>
    public class FileToFileType
    {
        public int FileToFileTypeId { get; set; }

        public int FileModelId { get; set; }
        public FileModel FileModel { get; set; }

        public int FileDataTypeId { get; set; }
        public FileDataType FileDataType { get; set; }
    }
}