using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventAssos.Core.DTOs.Requests;

public class AddEventRequestDto
{
    [Required(ErrorMessage = "Le titre de l'événement est requis.")]
    public string Title { get; set; } =null!;
    [Required(ErrorMessage = "La description de l'événement est requise.")]
        public string Description { get; set; } = null!;

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public string? lieu { get; set; }
    [Required] 
    [Range(1,200, ErrorMessage = "Le nombre minimum de participants doit être supérieur ou égal à 1.")]
    public int NbMin { get; set; }
    [Required]
    [Range(1, 200, ErrorMessage = "Le nombre maximum de participants doit être supérieur ou égal à 1.")]
    public int NbMax { get; set; }
    [Required(ErrorMessage = "Le statut de l'événement est requis.")]
    public EventStatut Statut { get; set; }
    public DateOnly CreationDate { get; set; }
    [Required(ErrorMessage = "La date limite d'inscription est requise.")]
    public DateTime LimiteInscription { get; set; }


}
