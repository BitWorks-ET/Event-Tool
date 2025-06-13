using ET.Shared.DTOs;
using FluentResults;

namespace ET_Backend.Repository.Organization;

/// <summary>
/// Definiert Zugriffsmethoden auf Organisationsdaten in der Datenbank.
/// </summary>
public interface IOrganizationRepository
{
    // === Existenzprüfung ===
    /// <summary>
    /// Prüft, ob eine Organisation mit der angegebenen Domain existiert.
    /// </summary>
    /// <param name="domain">Die Domain der Organisation.</param>
    /// <returns>Ein Result mit true, wenn die Organisation existiert, sonst false.</returns>
    Task<Result<bool>> OrganizationExists(string domain);
    /// <summary>
    /// Prüft, ob eine Organisation mit der angegebenen ID existiert.
    /// </summary>
    /// <param name="id">Die ID der Organisation.</param>
    /// <returns>Ein Result mit true, wenn die Organisation existiert, sonst false.</returns>
    Task<Result<bool>> OrganizationExists(int id);

    // === Lesen ===
    /// <summary>
    /// Gibt eine Liste aller Organisationen zurück.
    /// </summary>
    /// <returns>Ein Result mit einer Liste aller Organisationen.</returns>
    Task<Result<List<Models.Organization>>> GetAllOrganizations();
    /// <summary>
    /// Gibt die Organisation mit der angegebenen Domain zurück.
    /// </summary>
    /// <param name="domain">Die Domain der Organisation.</param>
    /// <returns>Ein Result mit der gefundenen Organisation.</returns>
    Task<Result<Models.Organization>> GetOrganization(string domain);
    /// <summary>
    /// Gibt die Organisation mit der angegebenen ID zurück.
    /// </summary>
    /// <param name="id">Die ID der Organisation.</param>
    /// <returns>Ein Result mit der gefundenen Organisation.</returns>
    Task<Result<Models.Organization>> GetOrganization(int id);
    /// <summary>
    /// Gibt alle Mitglieder einer Organisation anhand der Domain zurück.
    /// </summary>
    /// <param name="domain">Die Domain der Organisation.</param>
    /// <returns>Ein Result mit einer Liste der Mitglieder als DTOs.</returns>
    Task<Result<List<OrganizationMemberDto>>> GetMembersByDomain(string domain);


    // === Schreiben ===

    /// <summary>
    /// Erstellt eine neue Organisation und verknüpft direkt einen Owner.
    /// </summary>
    /// <param name="name">Der Name der Organisation.</param>
    /// <param name="description">Die Beschreibung der Organisation.</param>
    /// <param name="domain">Die eindeutige Domain der Organisation.</param>
    /// <param name="ownerFirstName">Vorname des Besitzers.</param>
    /// <param name="ownerLastName">Nachname des Besitzers.</param>
    /// <param name="ownerEmail">E-Mail-Adresse des Besitzers.</param>
    /// <param name="initialPassword">Initiales Passwort für den Besitzeraccount.</param>
    /// <returns>Ein Result mit der erstellten Organisation.</returns>
    Task<Result<Models.Organization>> CreateOrganization(
        string name,
        string description,
        string domain,
        string ownerFirstName,
        string ownerLastName,
        string ownerEmail,
        string initialPassword
    );
    /// <summary>
    /// Aktualisiert die Daten einer bestehenden Organisation.
    /// </summary>
    /// <param name="organization">Die aktualisierte Organisation.</param>
    /// <returns>Ein Result, das den Erfolg der Operation angibt.</returns>
    Task<Result> EditOrganization(Models.Organization organization);
    /// <summary>
    /// Aktualisiert die Daten einer Organisation anhand ihrer ID.
    /// </summary>
    /// <param name="id">Die ID der Organisation.</param>
    /// <param name="dto">Das DTO mit den aktualisierten Daten.</param>
    /// <returns>Ein Result, das den Erfolg der Operation angibt.</returns>
    Task<Result> UpdateOrganization(int id, OrganizationDto dto);

    // === Löschen ===
    /// <summary>
    /// Löscht eine Organisation anhand ihrer Domain.
    /// </summary>
    /// <param name="domain">Die Domain der Organisation.</param>
    /// <returns>Ein Result, das angibt, ob die Löschung erfolgreich war.</returns>
    Task<Result> DeleteOrganization(string domain);
    /// <summary>
    /// Löscht eine Organisation anhand ihrer ID.
    /// </summary>
    /// <param name="id">Die ID der Organisation.</param>
    /// <returns>Ein Result, das angibt, ob die Löschung erfolgreich war.</returns>
    Task<Result> DeleteOrganization(int id);
}