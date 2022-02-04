using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackEnd.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 70)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 60)]
        public string Profession { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [StringLength(maximumLength: 100)]
        public string Faculty { get; set; }
        [StringLength(maximumLength: 100)]
        public string Degree { get; set; }
        [StringLength(maximumLength: 100)]
        public string Experience { get; set; }
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }
        public string Number { get; set; }
        [StringLength(maximumLength: 100)]
        public string FacebookURL { get; set; }
        public List<TeacherHobby> TeacherHobbies { get; set; }
        [NotMapped]
        public List<int> HobbiesIds { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public int ImageIds { get; set; }

    }
}
