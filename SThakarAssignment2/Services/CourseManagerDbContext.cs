using Microsoft.EntityFrameworkCore;
using SThakarAssignment2.Entities;

namespace SThakarAssignment2.Services
{
    public class CourseManagerDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        public CourseManagerDbContext(DbContextOptions<CourseManagerDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seeded data of the database
            modelBuilder.Entity<Course>().HasData(

                new Course
                {
                    CourseId = 1,
                    Name = "Microsoft Web Technologies",
                    Instructor = "Mark Rajkumar",
                    StartDate = new DateTime(2023, 09, 18),
                    RoomNumber = "3G17"
                },
                new Course
                {
                    CourseId = 2,
                    Name = "Game Programming With Data Structures",
                    Instructor = "Mark Rajkumar",
                    StartDate = new DateTime(2022, 09, 18),
                    RoomNumber = "4G11"

                });
            modelBuilder.Entity<Student>().HasData(
                new Student {
                StudentId = 1,
                Name = "Sahil Thakar",
                Email = "sahilgamer3113@gmail.com",
                Status = StudentStatus.ConfirmationMessageNotSent,
                CourseId=1,
                },
                new Student
                {
                    StudentId = 2,
                    Name="Het Mehta",
                    Email="test@gmail.com",
                    Status= StudentStatus.ConfirmationMessageNotSent,
                    CourseId=2,
                }
                ,
                new Student
                {
                    StudentId =3,
                    Name="Jeeshant Patel",
                    Email = "JP@yahoo.com",
                    Status =  StudentStatus.ConfirmationMessageNotSent,
                    CourseId=1
                    
                })
                ;
            base.OnModelCreating(modelBuilder);
        }

    }
}
