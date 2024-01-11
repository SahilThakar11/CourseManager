namespace SThakarAssignment2.Services
{
    public class CookieServices : ICookieServices
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CookieServices(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        //To display cookie Message
        public string GetWelcomeMessage()
        {
            var _context = _contextAccessor.HttpContext;
            //Check if cookie exists
            if (_context.Request.Cookies["FirstVisit"] == null)
            {
                //Expire cookie time set to 90
                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(90)
                };
                _context.Response.Cookies.Append("FirstVisit", DateTime.Now.ToString(), options);
                return "Hey ! Welcome to the Course Manager App!";
            }
            else
            {
                var firstVisitTimeStamp = _context.Request.Cookies["FirstVisit"];
                return $"Welcome back! You first visited this app on:{firstVisitTimeStamp}";
            }
        }
    }
}
