namespace EduHomeBackEnd.Models
{
    public class TeacherHobby
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int HobbyId { get; set; }
        public Teacher Teacher { get; set; }
        public Hobby Hobby { get; set; }
    }
}
