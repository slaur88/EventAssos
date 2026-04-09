using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using EventAssos.Secu.Interfaces.Services.Data;
using EventAssos.Secu.Interfaces.Services.Tools;
using EventAssos.Secu.Services.Data;
using EventAssos.Secu.Services.Tools;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Data.Seed
{
    public static class AdminSeed
    {
        public static async Task SeedMadameDupontAsync(IServiceProvider serviceProvider)
        {
            // On crée un scope pour récupérer les services injectés
            using var scope = serviceProvider.CreateScope();

            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var hasher = serviceProvider.GetRequiredService<IPasswordHasherService>();

            // Vérifier si l'admin existe déjà pour éviter les doublons
            var adminEmail = "dupont@admin.com";
            var existingUser = await userService.GetUserByEmailAsync(adminEmail);

            if (existingUser == null)
            {
                //Création de l'objet User selon entité 
                var admin = new User
                {
                    Id = Guid.NewGuid(),
                    Email = adminEmail,
                    Pseudo = "MmeDupont",
                    Role = UserRole.Admin,
                    Password = hasher.HashPassword("Admin123!"), // On hache le mot de passe via le service
                    Birthdate = new DateOnly(1980, 5, 15),
                    // Initialisation obligatoire des listes pour éviter les erreurs plus tard
                    CreatedEvents = new List<Event>(),
                    Inscriptions = new List<Inscription>()
                };

                // 3. Sauvegarde en base de données
                await userService.AddAsync(admin);

                Console.WriteLine("✅ SEED : Le compte admin de Madame Dupont a été créé (Mdp: Admin123!)");
            }
            else
            {
                Console.WriteLine("ℹ️ SEED : L'admin existe déjà, passage de l'étape.");
            }
        }
    }
}
