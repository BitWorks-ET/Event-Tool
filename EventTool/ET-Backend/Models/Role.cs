namespace ET_Backend.Models;

/// <summary>
/// Definiert die möglichen Rollen eines Benutzers innerhalb einer Organisation.
/// </summary>
public enum Role
{
    /// <summary>
    /// Hat vollständige Rechte in der Organisation (z. B. Gründer).
    /// </summary>
    Owner,

    /// <summary>
    /// Organisiert und verwaltet Events, aber keine volle Admin-Rechte.
    /// </summary>
    Organizer,

    /// <summary>
    /// Reguläres Mitglied mit beschränkten Rechten.
    /// </summary>
    Member,

    /// <summary>
    /// Externer Gast mit minimalen Zugriffsrechten.
    /// </summary>
    Guest
}
