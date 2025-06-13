using ET_Frontend.Models.AccountManagement;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ET_Frontend.Services.ApiClients
{
    /// <summary>
    /// Schnittstelle zur Kapselung der API-Aufrufe für Benutzeraktionen.
    /// </summary>
    public interface IUserApi
    {
        /// <summary>
        /// Ruft die aktuellen Benutzerdaten vom Backend ab.
        /// </summary>
        /// <returns>
        /// Ein <see cref="UserEditViewModel"/> mit den Benutzerdaten oder <c>null</c>, wenn kein Benutzer angemeldet ist.
        /// </returns>
        Task<UserEditViewModel?> GetCurrentUserAsync();

        /// <summary>
        /// Aktualisiert die Benutzerdaten im Backend.
        /// </summary>
        /// <param name="model">Das geänderte ViewModel mit Benutzerinformationen.</param>
        /// <returns><c>true</c>, wenn das Update erfolgreich war, sonst <c>false</c>.</returns>
        Task<bool> UpdateUserAsync(UserEditViewModel model);

        /// <summary>
        /// Ruft die Mitgliedschaften (Accounts in Organisationen) des Benutzers ab.
        /// </summary>
        /// <returns>Eine Liste von <see cref="MembershipViewModel"/>.</returns>
        Task<List<MembershipViewModel>> GetMembershipsAsync();

        /// <summary>
        /// Aktualisiert die E-Mail-Adresse für ein bestimmtes Konto.
        /// </summary>
        /// <param name="accountId">Die ID des zu aktualisierenden Kontos.</param>
        /// <param name="newEmail">Die neue E-Mail-Adresse.</param>
        /// <returns><c>true</c>, wenn erfolgreich, sonst <c>false</c>.</returns>
        Task<bool> UpdateEmailAsync(int accountId, string newEmail);

        /// <summary>
        /// Löscht eine Mitgliedschaft (Konto in einer Organisation) des Benutzers.
        /// </summary>
        /// <param name="accountId">Die ID des Accounts.</param>
        /// <param name="orgId">Die ID der Organisation.</param>
        /// <returns><c>true</c>, wenn erfolgreich, sonst <c>false</c>.</returns>
        Task<bool> DeleteMembershipAsync(int accountId, int orgId);

        /// <summary>
        /// Wechselt den aktuell aktiven Account des Benutzers.
        /// </summary>
        /// <param name="accountId">Die ID des neuen aktiven Accounts.</param>
        /// <returns>Das neue JWT-Token oder <c>null</c>, wenn der Wechsel fehlschlägt.</returns>
        Task<string?> SwitchAccountAsync(int accountId);

        /// <summary>
        /// Fügt eine neue Mitgliedschaft per E-Mail hinzu.
        /// </summary>
        /// <param name="email">Die E-Mail des Ziel-Accounts (innerhalb der aktuellen Organisation).</param>
        /// <returns><c>true</c>, wenn erfolgreich, sonst <c>false</c>.</returns>
        Task<bool> AddMembershipAsync(string email);

        /// <summary>
        /// Löscht den aktuell eingeloggten Benutzer inklusive aller zugehörigen Konten.
        /// </summary>
        Task DeleteCurrentUserAsync();
    }
}
