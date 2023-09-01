using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class AboutPageModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string AlgorithmDescription { get; set; } = String.Empty;
    }
}
