using EventAssos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Infra.Database.Context
{
    public class EventAssosContext: DbContext
    {
        public EventAssosContext(DbContextOptions<EventAssosContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventAssosContext).Assembly);
        }
    }
}
