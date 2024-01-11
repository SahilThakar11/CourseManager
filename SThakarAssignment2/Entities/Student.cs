namespace SThakarAssignment2.Entities
{
    //Student class
    public class Student
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public StudentStatus Status { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
    //Enum for the student status of the enrollment
    public enum StudentStatus
    {
        ConfirmationMessageNotSent,
        ConfirmationMessageSent,
        EnrollmentConfirmed,
        EnrollmentDeclined
    }
}
