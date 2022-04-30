
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
 
namespace URLShortener.DataAccess.Models
{
    [Index(nameof(URL.ShortCode), IsUnique = true)]
    public class URL : Base
    {

        public int UrlID { get; set; }
        [Required]
        public string ProvidedURL { get; set; }
        [Required]
        public string ShortURL { get; set; }
        public int Visits { get; set; }

        [Required]
        [MaxLength(10)]
        public string ShortCode { get; set; }

    }
}