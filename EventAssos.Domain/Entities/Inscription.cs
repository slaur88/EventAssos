using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Domain.Entities
{
    public class Inscription
    {
        
        
        //Navigation
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public StatutInscription Statut { get; set; }
        public DateTime Date
        {
            get; set;
        }
    }
}
