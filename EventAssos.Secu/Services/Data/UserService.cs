using EventAssos.Secu.DTOs.Requests;
using EventAssos.Secu.DTOs.Responses;
using EventAssos.Secu.Interfaces.Repositories;
using EventAssos.Secu.Interfaces.Services;
using EventAssos.Secu.Interfaces.Services.Data;
using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Services.Data;

public class UserService(IUserRepository _userRepository) : IUserService
{
    

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task UpdatePseudo(Guid userId, string newPseudo)
    {
        // 1. On vérifie si le pseudo est déjà pris
        var existingUser = await _userRepository.GetUserByPseudoAsync(newPseudo);

        if (existingUser != null && existingUser.Id != userId)
        {
            throw new InvalidOperationException("Ce pseudo est déjà utilisé par un autre membre.");
        }

        // 2. On récupère l'utilisateur à modifier
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) throw new KeyNotFoundException("Utilisateur non trouvé.");

        // 3. On applique le changement et on sauvegarde
        user.Pseudo = newPseudo;
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var existingUser = await _userRepository.ExistsAsync(id);
        if (!existingUser) throw new KeyNotFoundException("Id not found");
        await _userRepository.DeleteAsync(id);
    }
}
