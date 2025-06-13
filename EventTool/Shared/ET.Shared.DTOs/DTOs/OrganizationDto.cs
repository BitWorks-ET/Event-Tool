namespace ET.Shared.DTOs;
/// <summary>
/// Detaillierte Informationen zu einer Organisation.
/// </summary>
/// <param name="Id">Die eindeutige ID der Organisation.</param>
/// <param name="Name">Der Name der Organisation.</param>
/// <param name="Domain">Die Domain der Organisation, z. B. für E-Mail-Adressen.</param>
/// <param name="Description">Eine Beschreibung der Organisation.</param>
/// <param name="OrgaPicAsBase64">Ein Logo oder Bild der Organisation als Base64-String.</param>
/// <param name="OwnerFirstName">Vorname des Organisationsinhabers.</param>
/// <param name="OwnerLastName">Nachname des Organisationsinhabers.</param>
/// <param name="OwnerEmail">E-Mail-Adresse des Organisationsinhabers.</param>
/// <param name="InitialPassword">Initiales Passwort für den Zugang zur Organisation.</param>
public record OrganizationDto(
    int Id,
    string Name,
    string Domain,
    string Description,
    string OrgaPicAsBase64,
    string OwnerFirstName,
    string OwnerLastName,
    string OwnerEmail,
    string InitialPassword
);