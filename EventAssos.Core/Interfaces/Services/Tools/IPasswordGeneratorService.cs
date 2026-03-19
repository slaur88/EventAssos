using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Services.Tools
{
    public interface IPasswordGeneratorService
    {
        // Longueur configurable avec valeur par défaut
        string RandomPassword(int length = 12);
    }
}
