using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Interfaces.Services.Tools
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
