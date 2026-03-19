using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.DTOs.Responses
{
    //Création Dto de responde du register
    public class RegisterResponseDTO
    {

        public Guid Id { get; set; }
      
        public string Email { get; set; }

        
    }
}
