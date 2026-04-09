using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.DTOs.Responses
{
    public class GetEventResponseDTO
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!; 
        public string? Lieu { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int NbMin { get; set; }
        public int NbMax { get; set; }
        public int NbInscrits { get; set; }
        public string Statut { get; set; } = null!;
        public DateTime LimiteInscription { get; set; }
        public bool ListeAttenteActive { get; set; }

        public List<string> Categories { get; set; } = new();

    }
}
