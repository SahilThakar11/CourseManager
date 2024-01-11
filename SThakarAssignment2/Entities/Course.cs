using System.ComponentModel.DataAnnotations;

namespace SThakarAssignment2.Entities
{
    //Course class
    public class Course
    {
        public int CourseId { get; set; }
        public string? Name { get; set; }

        public string? Instructor { get; set; }
        public DateTime StartDate { get; set; }
        [RegularExpression(@"\d[A-Z]\d{2}")]
        public string? RoomNumber { get; set; }
        public List<Student> Students { get; set; } = new List<Student>(); 
    }
}
