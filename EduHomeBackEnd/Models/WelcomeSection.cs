using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEnd.Models
{
    public class WelcomeSection
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 300)]
        public string Description { get; set; }
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }
    }
}
