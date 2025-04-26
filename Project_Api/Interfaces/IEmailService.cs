namespace Project_Api.Interfaces
{
    public interface IEmailService
    {
        Task sendEmailAsync(string Email, string subject, string message);
    }
}
