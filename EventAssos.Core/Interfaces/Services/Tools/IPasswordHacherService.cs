using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Services.Tools
{
    public interface IPasswordHacherService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string storedPassword);
    }
}
