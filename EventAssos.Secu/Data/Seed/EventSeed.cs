using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace EventAssos.Secu.Data.Seed
{
   public class EventSeed
    {
        public static void SeedEvents(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = Guid.Parse("A1B2C3D4-E5F6-4A5B-8C9D-0E1F2A3B4C5D"),
                    Name = "Soirée Jeux de Société", 
                    Description = "Une soirée conviviale pour découvrir de nouveaux jeux !",
                    Lieu = "Salle Polyvalente, Namur",
                    Start = new DateTime(2026, 05, 20, 19, 0, 0),
                    End = new DateTime(2026, 05, 20, 23, 0, 0),
                    NbMin = 5,
                    NbMax = 20,
                    Statut = EventStatut.EnCours, 
                    CreationDate = DateOnly.FromDateTime(DateTime.Now),
                    LimiteInscription = new DateTime(2026, 05, 19, 23, 59, 59),
                    MiseAJour = DateTime.Now,
                    ListeAttenteActive = true
                }
            );
        }
    }
}

