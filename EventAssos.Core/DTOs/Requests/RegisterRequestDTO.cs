using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventAssos.Secu.DTOs.Requests
{
    //Création Dto pour la request du register
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "L'email est requis.")]//Validation
        [EmailAddress(ErrorMessage = "Le format est incorrect.")]
        public string Email { get; set; } = null!;

    }
        
}
