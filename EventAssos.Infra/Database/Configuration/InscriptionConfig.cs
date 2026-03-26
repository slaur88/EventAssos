using EventAssos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Infra.Database.Configuration
{
    public class InscriptionConfig : IEntityTypeConfiguration<Inscription>
    {
        public void Configure(EntityTypeBuilder<Inscription> builder)
        {
            
            builder.HasKey(i => new { i.UserId, i.EventId });

            
            builder.Property(i => i.Statut)
                .IsRequired();

            builder.Property(i => i.Date)
                .IsRequired();

            
            builder.HasOne(i => i.User)
                   .WithMany(u => u.Inscriptions)
                   .HasForeignKey(i => i.UserId);

            
            builder.HasOne(i => i.Event)
                   .WithMany(e => e.Inscriptions)
                   .HasForeignKey(i => i.EventId);
        }
    }
}
