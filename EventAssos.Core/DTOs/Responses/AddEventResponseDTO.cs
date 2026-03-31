using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.DTOs.Responses
{
    internal class AddEventResponseDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? lieu { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int NbMin { get; set; }
        public int NbMax { get; set; }
        public EventStatut Statut { get; set; }
        public DateOnly CreationDate { get; set; }

        public DateTime LimiteInscription { get; set; }

        public DateTime MiseAJour { get; set; }
        public bool ListeAttenteActive { get; set; }

        //Navigation 
        public Guid? CreatedByUserId { get; set; }
        public User? CreatedBy { get; set; }
        public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
        public ICollection<Categorie> Categories { get; set; } = new List<Categorie>();
    }
}
