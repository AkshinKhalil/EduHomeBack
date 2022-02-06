using System;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEnd.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Text { get; set; }
        public bool IsAccess { get; set; }
        public DateTime CreatedTime { get; set; }
        [Required]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
