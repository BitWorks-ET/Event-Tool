using ET_Backend.Models;
using ET.Shared.DTOs;
using FluentResults;
using System.Security.Claims;

namespace ET_Backend.Services.Person
{
    /// <summary>
    /// Definiert die Schnittstelle für Benutzerservices, 
    /// z. B. für Profil- und Mitgliedschaftsverwaltung.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gibt das aktuell eingeloggte Benutzerkonto zurück.
        /// </summary>
        /// <param name="user">Der <see cref="ClaimsPrincipal"/> des eingeloggten Nutzers.</param>
        /// <returns>Ein <see cref="Account"/> oder <c>null</c>, falls nicht gefunden.</returns>
        Task<Account?> GetCurrentUserAsync(ClaimsPrincipal user); // für EventController
        /// <summary>
        /// Holt die Benutzerdaten zu einer gegebenen ID.
        /// </summary>
        /// <param name="id">Die ID des Benutzers.</param>
        /// <returns>Ein <see cref="UserDto"/> mit den Benutzerdaten.</returns>
        Task<Result<UserDto>> GetUserAsync(int id);
        /// <summary>
        /// Gibt alle Mitgliedschaften eines Benutzers zurück.
        /// </summary>
        /// <param name="id">Die ID des Benutzers.</param>
        /// <returns>Eine Liste von <see cref="MembershipDto"/>-Objekten.</returns>
        Task<Result<List<MembershipDto>>> GetMembershipsAsync(int id);
        /// <summary>
        /// Ändert die E-Mail-Adresse eines Kontos.
        /// </summary>
        /// <param name="accountId">Die ID des Accounts.</param>
        /// <param name="newEmail">Die neue E-Mail-Adresse.</param>
        Task<Result> UpdateEmailAsync(int accountId, string newEmail);
        /// <summary>
        /// Entfernt die Mitgliedschaft eines Accounts aus einer Organisation.
        /// </summary>
        /// <param name="accountId">Die Account-ID.</param>
        /// <param name="orgId">Die Organisations-ID.</param>
        Task<Result> DeleteMembershipAsync(int accountId, int orgId);

        /// <summary>
        /// Aktualisiert die Benutzerdaten.
        /// </summary>
        /// <param name="dto">Neue Benutzerdaten als <see cref="UserDto"/>.</param>
        Task<Result> UpdateUserAsync(UserDto dto);
        /// <summary>
        /// Löscht einen Benutzer samt zugehöriger Accounts.
        /// </summary>
        /// <param name="userId">Die ID des zu löschenden Benutzers.</param>
        Task<Result> DeleteUserAsync(int userId);
    }
}