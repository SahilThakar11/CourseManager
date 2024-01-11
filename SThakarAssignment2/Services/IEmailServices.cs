namespace SThakarAssignment2.Services
{
    public interface IEmailServices
    {
        void SendEmail(string to,string subject,string body);
    }
}
