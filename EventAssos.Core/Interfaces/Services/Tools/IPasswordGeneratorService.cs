using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Interfaces.Services.Tools
{
    public interface IPasswordGeneratorService
    {
        // Longueur configurable avec valeur par défaut
        string RandomPassword(int length = 12);
    }
}
