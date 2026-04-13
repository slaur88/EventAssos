using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Services.Data
{
    public interface IInscriptionService
    {
        Task<string> InscriptionMemberAsync(Guid userId, Guid eventId);
    }
}
