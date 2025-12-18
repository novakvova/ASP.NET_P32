using Core.SMTP;

namespace Core.Interfaces;

public interface ISmtpService
{
    Task<bool> SendEmailAsync(EmailMessage message);
}
