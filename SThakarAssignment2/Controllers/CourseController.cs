using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SThakarAssignment2.Entities;
using SThakarAssignment2.Services;

namespace SThakarAssignment2.Controllers
{
    public class CourseController : Controller
    {

        public CourseManagerDbContext _context;
        private readonly CookieServices _cookieManager;
        private EmailServices _emailServices;
        //Public constructor to initialize context from DB, cookieServices and emailservices
        public CourseController(CourseManagerDbContext context, CookieServices cookieManager, EmailServices emailServices)
        {
            _context = context;
            _cookieManager = cookieManager;
            _emailServices = emailServices;
        }
        //Get request for showing the view table of courses
        [HttpGet("/View-Courses")]
        public IActionResult ViewCourses()
        {
            string welcomeMsg = _cookieManager.GetWelcomeMessage();
            ViewData["WelcomeMessage"] = welcomeMsg;
            List<Course> courses = _context.Courses.Include(s =>s.Students).OrderByDescending(x => x.CourseId).ToList();
            return View("ViewCourses",courses);
        }
        //get request for adding course and returning view
        [HttpGet("/add-new-course")]
        public IActionResult GetAddCourseRequest()
        {
            string welcomeMsg = _cookieManager.GetWelcomeMessage();
            ViewData["WelcomeMessage"] = welcomeMsg;
            return View("Add", new Course());
        }
        //Post request for adding course and redirect to the view course table
        [HttpPost("/View-Course")]
        public IActionResult AddNewCourse(Course course)
        {
            if(ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();

                TempData["LastActionMessage"] = $"The course \"{course.Name}\" was successfully added";
                return RedirectToAction("ViewCourses");
            }
            else
            {
                return View("Add", course);
            }
        }
        //To get a course by id and display manage course view
        [HttpGet("/Course/{id}")]
        public IActionResult GetCourseId(int id)
        {
            string welcomeMsg = _cookieManager.GetWelcomeMessage();
            ViewData["WelcomeMessage"] = welcomeMsg;
            Course course = _context.Courses.Include(c => c.Students).FirstOrDefault(x => x.CourseId == id);

            return View("ManageCourse",course);
        }
        //Get request for the edit-course and return edit with the course object
        [HttpGet("/Course/{id}/Edit-Course")]
        public IActionResult GetEditCourseById(int id)
        {
            string welcomeMsg = _cookieManager.GetWelcomeMessage();
            ViewData["WelcomeMessage"] = welcomeMsg;
            Course course = _context.Courses.Find(id);
            return View("Edit",course);
        }
        //To process the Edit  
        [HttpPost("/Course/Edit-Courses/{id}")]
        public IActionResult ProcessEditCourse(int id,Course course)
        {
            
            if(id != course.CourseId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _context.Courses.Update(course);
                _context.SaveChanges();

                TempData["LastActionMessage"] = $"The Course \"{course.Name}\" was successfully updated.";
                return RedirectToAction("ViewCourses");
            }
            else
            {
                return View("GetEditCourseById", course);
            }
        }
        //Get students by the id 
        public IActionResult GetStudents(int id)
        {
            Course course = _context.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseId == id);
            if(course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        //for adding the student process by id and display to table of students
        [HttpPost("/Course/ManageCourse/{id}")]
        public IActionResult AddStudent(Student student,int id)
        {
            if(ModelState.IsValid)
            {
                Course course = _context.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseId == id);
                if (course == null || course.Students == null)
                { return NotFound(); }
                student.Status = StudentStatus.ConfirmationMessageNotSent;

                course.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("GetCourseId", new { id });
            }
            else
            {
                return View("ManageCourse");
            }
        }
        //For sending mail to added students in the table
        [HttpPost("/Course/{id}")]
        public IActionResult SendEmail(int id)
        {

            List<Student> students = _context.Students.Include(s => s.Course).Where(s => s.CourseId == id && s.Status == StudentStatus.ConfirmationMessageNotSent).ToList();
            
            foreach(var student in students)
            {
                string toEmail = student.Email;
                string subject = $"Enrollment confirmation for '{student.Course.Name}' required";
                string body = $"<h3>Hello {student.Name}:</h3>\n" +
                              $"Your request to enroll in the course '{student.Course.Name}' in room {student.Course.RoomNumber} starting {student.Course.StartDate} with instructor {student.Course.Instructor}. We are Pleased to have you in the course so if you could <a href='https://localhost:7105/Email/Confirmation/{student.CourseId}-{student.StudentId}'>Confirm</a> as soon as possible that would be appreciated!\n" +
                              $"Sincerely,\n" +
                              $"The Course Manager App.";
                try
                {
                    _emailServices.SendEmail(toEmail, subject, body);
                    student.Status = StudentStatus.ConfirmationMessageSent;
                    _context.SaveChanges();
                    Console.WriteLine($"Mail sent to {toEmail}");
                    
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
            
            return RedirectToAction("GetCourseId",new {id});
        }
    }
}
