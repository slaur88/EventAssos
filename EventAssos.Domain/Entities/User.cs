using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Pseudo { get; set; } = null!;
        public required string Email { get; set; }
        public string Password { get; set; } = null!;
        public int Birthdate { get; set; }
        public UserGenre UserGenre { get; set; }
        public UserRole Role { get; set; }
    }
}