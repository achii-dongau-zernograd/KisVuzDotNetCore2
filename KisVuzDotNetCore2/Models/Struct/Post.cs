using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Должность
    /// </summary>
    public class Post
    {
        /// <summary>
        /// УИД должности
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Наименование должности
        /// </summary>
        [Display(Name= "Наименование должности")]
        public string PostName { get; set; }
    }
}