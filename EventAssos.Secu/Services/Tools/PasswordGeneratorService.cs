using EventAssos.Secu.Interfaces.Services.Tools;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EventAssos.Secu.Services.Tools;

internal class PasswordGeneratorService : IPasswordGeneratorService
{

    public string RandomPassword(int length = 12)
    {
        const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";

        return new string(Enumerable.Repeat(caracteres, length) //Répète la string caracteres exactement 'length' fois.
            .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)])//Pour chaque répétition de la string il choisit un caractère aléatoire
            .ToArray());
    }
}
