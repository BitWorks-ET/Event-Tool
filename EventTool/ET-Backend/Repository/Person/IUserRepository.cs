using ET_Backend.Models;
using FluentResults;

namespace ET_Backend.Repository.Person;
/// <summary>
/// Definiert Methoden zum Zugriff auf Benutzerdaten in der Datenquelle.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Prüft, ob ein Benutzer mit der angegebenen ID existiert.
    /// </summary>
    /// <param name="userId">ID des Benutzers.</param>
    /// <returns>True, wenn der Benutzer existiert; andernfalls false.</returns>
    public Task<Result<bool>> UserExists(int userId);
    /// <summary>
    /// Erstellt einen neuen Benutzer mit Vorname, Nachname und Passwort.
    /// </summary>
    /// <param name="firstname">Vorname des Benutzers.</param>
    /// <param name="lastname">Nachname des Benutzers.</param>
    /// <param name="password">Passwort des Benutzers (idealerweise gehasht).</param>
    /// <returns>Der erstellte Benutzer oder ein Fehler.</returns>
    public Task<Result<User>> CreateUser(String firstname, String lastname, String password);
    /// <summary>
    /// Löscht einen Benutzer anhand seiner ID.
    /// </summary>
    /// <param name="userId">ID des zu löschenden Benutzers.</param>
    /// <returns>Ergebnis der Operation.</returns>
    public Task<Result> DeleteUser(int userId);
    /// <summary>
    /// Ruft einen Benutzer anhand seiner ID ab.
    /// </summary>
    /// <param name="userId">ID des Benutzers.</param>
    /// <returns>Gefundener Benutzer oder ein Fehler.</returns>
    public Task<Result<User>> GetUser(int userId);
    /// <summary>
    /// Aktualisiert die Daten eines vorhandenen Benutzers.
    /// </summary>
    /// <param name="user">Benutzerobjekt mit aktualisierten Informationen.</param>
    /// <returns>Ergebnis der Operation.</returns>
    public Task<Result> EditUser(User user);
}