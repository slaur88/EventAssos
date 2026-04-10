using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.DTOs.Responses;

public class EventStatsResponseDTO
{
    public Guid EventId { get; set; }
    public string EventName { get; set; } = string.Empty;

    public int NbMax { get; set; }
    public int NbMin { get; set; }

    public int TotalInscrits { get; set; } //Confimr + liste d'attente
    public int NbConfirmes { get; set; } //Statut inscription.Confirmé
    public int NbListeAttente { get; set; } // Statut inscription.ListeAttente

    public int PlacesRestantes => Math.Max(0, NbMax - NbConfirmes);
    public double TauxRemplissage => NbMax > 0 ? (double)NbConfirmes / NbMax * 100 : 0;

    public bool EstComplet => NbConfirmes >= NbMax;
    public bool SeuilMinimumAtteint => NbConfirmes >= NbMin;

    public List<InscriptionParJourDTO> InscriptionsParJour { get; set; } = new();
}
