using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebUrlShortener.ViewModels
{
    public class UrlLinkViewModel
    {
        public Guid UrlId { get; set; } = Guid.NewGuid();

        [Required]
        public string LongUrl { get; set; } = String.Empty;
        
        public string? ShortUrl { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        public string? Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public AppUserViewModel? User { get; set; }
    }
}
