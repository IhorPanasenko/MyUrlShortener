using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class UrlLink
    {
        [Key]
        public Guid UrlId { get; set; } = Guid.NewGuid();

        [Required]
        public string LongUrl { get; set; } = String.Empty;

        [Required]
        public string ShortUrl { get; set; } = String.Empty;

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        public string? Description { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public AppUser? User { get; set; }
    }
}
