using ET_Backend.Models;
using FluentResults;

namespace ET_Backend.Repository.Person;

/// <summary>
/// Definiert Methoden für den Zugriff auf Kontodaten in der Datenquelle.
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Prüft, ob ein Account mit der angegebenen E-Mail-Adresse existiert.
    /// </summary>
    /// <param name="accountEMail">E-Mail-Adresse des Accounts.</param>
    /// <returns>True, wenn der Account existiert; andernfalls false.</returns>
    public Task<Result<bool>> AccountExists(string accountEMail);
    /// <summary>
    /// Prüft, ob ein Account mit der angegebenen ID existiert.
    /// </summary>
    /// <param name="accountId">ID des Accounts.</param>
    /// <returns>True, wenn der Account existiert; andernfalls false.</returns>
    public Task<Result<bool>> AccountExists(int accountId);
    /// <summary>
    /// Erstellt einen neuen Account und verknüpft ihn mit einem Benutzer und einer Organisation.
    /// </summary>
    /// <param name="accountEMail">E-Mail-Adresse des neuen Kontos.</param>
    /// <param name="organization">Zugehörige Organisation.</param>
    /// <param name="role">Rolle innerhalb der Organisation.</param>
    /// <param name="user">Benutzerinformationen.</param>
    /// <returns>Das erstellte Konto oder ein Fehler.</returns>
    public Task<Result<Account>> CreateAccount(string accountEMail, Models.Organization organization, Role role, User user);
    /// <summary>
    /// Löscht einen Account anhand der E-Mail-Adresse.
    /// </summary>
    /// <param name="accountEMail">E-Mail-Adresse des zu löschenden Accounts.</param>
    /// <returns>Ergebnis der Operation.</returns>
    public Task<Result> DeleteAccount(string accountEMail);
    /// <summary>
    /// Löscht einen Account anhand der Account-ID.
    /// </summary>
    /// <param name="accountId">ID des zu löschenden Accounts.</param>
    /// <returns>Ergebnis der Operation.</returns>
    public Task<Result> DeleteAccount(int accountId);
    /// <summary>
    /// Ruft einen Account anhand der E-Mail-Adresse ab.
    /// </summary>
    /// <param name="accountEMail">E-Mail-Adresse des Accounts.</param>
    /// <returns>Gefundener Account oder ein Fehler.</returns>
    public Task<Result<Account>> GetAccount(string accountEMail);
    /// <summary>
    /// Ruft einen Account anhand der Account-ID ab.
    /// </summary>
    /// <param name="accountId">ID des Accounts.</param>
    /// <returns>Gefundener Account oder ein Fehler.</returns>
    public Task<Result<Account>> GetAccount(int accountId);
    /// <summary>
    /// Aktualisiert Account- und Userdaten sowie die Rolle.
    /// </summary>
    /// <param name="account">Der zu aktualisierende Account.</param>
    /// <returns>Ergebnis der Operation.</returns>
    public Task<Result> EditAccount(Account account);
    /// <summary>
    /// Entfernt einen Account aus einer Organisation.
    /// </summary>
    /// <param name="accountId">ID des Accounts.</param>
    /// <param name="orgId">ID der Organisation.</param>
    /// <returns>Ergebnis der Operation.</returns>
    public Task<Result> RemoveFromOrganization(int accountId, int orgId);
    /// <summary>
    /// Ruft alle Accounts eines Benutzers anhand seiner User-ID ab.
    /// </summary>
    /// <param name="userId">ID des Benutzers.</param>
    /// <returns>Liste der zugehörigen Accounts oder ein Fehler.</returns>
    public Task<Result<List<Account>>> GetAccountsByUser(int userId);
    /// <summary>
    /// Aktualisiert die E-Mail-Adresse eines Accounts.
    /// </summary>
    /// <param name="accountId">ID des Accounts.</param>
    /// <param name="email">Neue E-Mail-Adresse.</param>
    /// <returns>Ergebnis der Operation.</returns>
    public Task<Result> UpdateEmail(int accountId, string email);
    /// <summary>
    /// Aktualisiert die E-Mail-Domain aller Accounts einer Organisation.
    /// </summary>
    /// <param name="orgId">ID der Organisation.</param>
    /// <param name="oldDomain">Bisherige Domain (ohne @).</param>
    /// <param name="newDomain">Neue Domain (ohne @).</param>
    /// <returns>Ergebnis der Operation.</returns>
    public Task<Result> UpdateEmailDomainsForOrganization(int orgId, string oldDomain, string newDomain);
}