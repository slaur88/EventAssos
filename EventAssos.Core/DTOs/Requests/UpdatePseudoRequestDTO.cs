using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventAssos.Core.DTOs.Requests
{
    public class UpdatePseudoRequestDTO
    {
        [Required(ErrorMessage = "Le nouveau pseudo est requis.")]
        [StringLength(20, ErrorMessage = "Le pseudo ne peut pas dépasser 20 caractères.")]
        public string NewPseudo { get; set; }
    }
}
