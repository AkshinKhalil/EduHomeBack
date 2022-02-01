using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEnd.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Logo { get; set; }
        [Required]
        public string SearchIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public int Connectnumber { get; set; }
        [StringLength(maximumLength: 150)]
        public string TwitIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string FacebookIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string VimeoIcon { get; set; }
    }
}
