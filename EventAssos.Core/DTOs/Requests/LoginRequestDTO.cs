using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventAssos.Secu.DTOs.Requests
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "L'identifiant est requis.")]
        public string Identifier { get; set; } = null!;

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        public string Password { get; set; } = null!;
    }
}
