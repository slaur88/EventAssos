using EventAssos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Infra.Database.Configuration
{
    internal class EventConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable(t => {
                t.HasCheckConstraint("CK_Event_Participants",
                    "NbMin BETWEEN 1 AND 200 AND NbMax BETWEEN 1 AND 200 AND NbMin <= NbMax");
            });

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Description);

            builder.Property(e => e.Lieu);

            builder.Property(e => e.Start);

            builder.Property(e => e.End);

            builder.Property(e => e.NbMin);

            builder.Property(e => e.NbMax);

            builder.Property(e => e.Statut);

            builder.Property(e => e.CreationDate);


            builder.HasOne(e => e.CreatedBy)
               .WithMany(u => u.CreatedEvents)
               .HasForeignKey(e => e.CreatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Categories)
                .WithMany(c => c.Events);

        }
    }
}
