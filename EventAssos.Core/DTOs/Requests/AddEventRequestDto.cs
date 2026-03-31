using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventAssos.Secu.DTOs.Requests;

public class AddEventRequestDto
{
    [Required(ErrorMessage = "Le titre de l'événement est requis.")]
    public string Name { get; set; } =null!;
    [Required(ErrorMessage = "La description de l'événement est requise.")]
        public string Description { get; set; } = null!;

    [Required] 
    [Range(1,200, ErrorMessage = "Le nombre minimum de participants doit être supérieur ou égal à 1.")]
    public int NbMin { get; set; }
    [Required]
    [Range(1, 200, ErrorMessage = "Le nombre maximum de participants doit être supérieur ou égal à 1.")]
    public int NbMax { get; set; }
    [Required(ErrorMessage = "Le statut de l'événement est requis.")]
    public EventStatut Statut { get; set; }



}