using EventAssos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Infra.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(t =>
            t.HasCheckConstraint("CK_User_Email_Format", "Email LIKE '%_@%_.%_'"));

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

            builder.HasIndex(u => u.Email)
            .IsUnique();

            builder.Property(u => u.Pseudo)
            .HasMaxLength(50);

            builder.HasIndex(u => u.Pseudo)
            .IsUnique();

            builder.Property(u => u.Password)
            .HasMaxLength(255);

            builder.Property(u => u.Birthdate);

            builder.Property(u => u.UserGenre);

            builder.Property(u => u.Role);


        }
    }
}
