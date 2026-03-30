using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.DTOs.Requests
{
    public class UpdateEventRequestDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Lieu { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int? NbMin { get; set; }
        public int? NbMax { get; set; }
        public DateTime? LimiteInscription { get; set; }
        public bool? ListeAttenteActive { get; set; }
        public List<Guid>? CategorieIds { get; set; }
    }
}
