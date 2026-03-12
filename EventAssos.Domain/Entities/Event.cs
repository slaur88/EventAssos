using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventAssos.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } 
        public string? lieu {  get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        [Range(1,200)]
        public int NbMin { get; set; }
        [Range(1,200)]
        public int NbMax { get; set; }
        public EventCategory Category { get; set; }
        public EventStatut Statut { get; set; }
        public DateOnly CreationDate { get; set; }

    }
}
