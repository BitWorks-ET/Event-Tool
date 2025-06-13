namespace ET_Backend.Models;

/// <summary>
/// Gibt den aktuellen Status eines Prozesses oder Events an.
/// </summary>
public enum Status
{
    /// <summary>
    /// Der Vorgang befindet sich im Entwurfsmodus und ist noch nicht veröffentlicht.
    /// </summary>
    Entwurf,

    /// <summary>
    /// Der Vorgang ist öffentlich zugänglich oder aktiv.
    /// </summary>
    Offen,

    /// <summary>
    /// Der Vorgang wurde abgeschlossen oder deaktiviert.
    /// </summary>
    Geschlossen

    // TODO: Weitere Statuswerte bei Bedarf hinzufügen
}
