using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Settings
{
    public class EmailSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string SenderEmail { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
