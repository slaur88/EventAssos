using EventAssos.Core.DTOs.Responses;
using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.DTOs.Responses
{
    public class AddEventResponseDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Lieu { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int NbMin { get; set; }
        public int NbMax { get; set; }
        public EventStatut Statut { get; set; }
        public DateOnly CreationDate { get; set; }

        public DateTime LimiteInscription { get; set; }
        public bool ListeAttenteActive { get; set; }

        public List<CategorieResponseDTO> Categorie { get; set; }

        
    }
}
