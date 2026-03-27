using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Services.Tools
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
