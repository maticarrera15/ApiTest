namespace ApiTest.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);

        Task SendEmailCodePsw(string to, string code);
    }
}
