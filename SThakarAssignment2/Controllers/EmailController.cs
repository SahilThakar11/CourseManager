using Microsoft.AspNetCore.Mvc;
using SThakarAssignment2.Entities;
using SThakarAssignment2.Services;

namespace SThakarAssignment2.Controllers
{
    public class EmailController : Controller
    {
        private CourseManagerDbContext _context;
        private CookieServices _cookieServices;
        private EmailServices _emailServices;
        //Public constructor
        public EmailController(CourseManagerDbContext context, CookieServices cookieManager, EmailServices emailServices) 
        {
            _context = context;
            _cookieServices = cookieManager;
            _emailServices = emailServices;
        }
        //To display confirmation Page
        [HttpGet("/Email/Confirmation/{id}-{sId}")]
        public IActionResult ConfirmationView(int id,int sId)
        {
            string welcomeMsg = _cookieServices.GetWelcomeMessage();
            ViewData["WelcomeMessage"] = welcomeMsg;
            Course course = _context.Courses.Find(id);  
            Student student = _context.Students.Find(sId);
            ViewData["sId"] = sId;
            ViewData["sName"] = student?.Name;
            return View("Confirmation",course);
        }

       //To get the responce of yes or no display acoording pages
        [HttpPost("/Email/Responce")]
        public IActionResult SendResponce(string Responce,int id,int sId)
        {
            Course course = _context.Courses.Find(id);
            if(course != null)
            {
             
                Student student = _context.Students?.FirstOrDefault(s =>s.StudentId == sId);
                if (student != null)
                {
                    if (Responce == "Yes")
                    {
                        student.Status = StudentStatus.EnrollmentConfirmed;
                        _context.SaveChanges();
                        return View("ThankYou");
                    }
                    if (Responce == "No")
                    {
                        student.Status = StudentStatus.EnrollmentDeclined;
                        _context.SaveChanges();
                        return View("Sorry");
                    }
                }
            }
                return BadRequest();
            
        }
    }
}
