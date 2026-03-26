using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventAssos.Core.DTOs.Requests
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "L'email est requise.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        public string Password { get; set; } = null!;
    }
}
