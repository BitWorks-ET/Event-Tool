using System.Data;
using Dapper;
using ET_Backend.Models;
using FluentResults;
using Microsoft.Data.Sqlite;

namespace ET_Backend.Repository.Person;

/// <summary>
/// Implementierung des Repositories für den Zugriff auf Benutzerdaten.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly IDbConnection _db;
    /// <summary>
    /// Erstellt eine neue Instanz des <see cref="UserRepository"/> mit gegebener Datenbankverbindung.
    /// </summary>
    /// <param name="db">Die zu verwendende Datenbankverbindung.</param>
    public UserRepository(IDbConnection db)
    {
        _db = db;
    }
    /// <summary>
    /// Prüft, ob ein Benutzer mit der angegebenen ID existiert.
    /// </summary>
    /// <param name="userId">Die ID des Benutzers.</param>
    /// <returns>True, wenn der Benutzer existiert; andernfalls false.</returns>
    public async Task<Result<bool>> UserExists(int userId)
    {
        try
        {
            var exists = await _db.ExecuteScalarAsync<bool>(
                "SELECT COUNT(1) FROM Users WHERE Id = @Id",
                new { Id = userId });

            return Result.Ok(exists);
        }
        catch(Exception ex)
        {
            return Result.Fail($"DBError: {ex.Message}");
        }
    }
    /// <summary>
    /// Erstellt einen neuen Benutzer mit Vorname, Nachname und Passwort.
    /// </summary>
    /// <param name="firstname">Vorname des Benutzers.</param>
    /// <param name="lastname">Nachname des Benutzers.</param>
    /// <param name="password">Passwort (idealerweise gehasht).</param>
    /// <returns>Der erstellte Benutzer oder ein Fehler.</returns>
    public async Task<Result<User>> CreateUser(string firstname, string lastname, string password)
    {
        try
        {
            var insertSql = @"
            INSERT INTO Users (Firstname, Lastname, Password)
            VALUES (@Firstname, @Lastname, @Password);";

            // Unterscheide automatisch SQLite vs. SQL Server
            var idQuery = _db is SqliteConnection
                ? "SELECT last_insert_rowid();"
                : "SELECT CAST(SCOPE_IDENTITY() AS int);";

            var userId = await _db.ExecuteScalarAsync<int>(
                insertSql + idQuery,
                new
                {
                    Firstname = firstname,
                    Lastname = lastname,
                    Password = password
                });

            return await GetUser(userId);
        }
        catch (Exception ex)
        {
            return Result.Fail($"DBError: {ex.Message}");
        }
    }
    /// <summary>
    /// Löscht einen Benutzer und alle zugehörigen Accounts, Event-Mitgliedschaften und Organisationszugehörigkeiten.
    /// </summary>
    /// <param name="userId">Die ID des zu löschenden Benutzers.</param>
    /// <returns>Ergebnis der Löschoperation.</returns>
    public async Task<Result> DeleteUser(int userId)
    {
        using var tx = _db.BeginSafeTransaction();    

        try
        {
            // Alle Accounts des Users ermitteln
            var accountIds = (await _db.QueryAsync<int>(
                $"SELECT Id FROM {_db.Tbl("Accounts")} WHERE UserId = @UserId",
                new { UserId = userId }, tx)).ToList();

            if (!accountIds.Any())
            {
                tx.Rollback();
                return Result.Fail("NotFound");
            }

            // Mitgliedschaften entfernen
            await _db.ExecuteAsync(
                $"DELETE FROM {_db.Tbl("OrganizationMembers")} WHERE AccountId IN @AccIds",
                new { AccIds = accountIds }, tx);

            // Event-Teilnahmen entfernen
            await _db.ExecuteAsync(
                $"DELETE FROM {_db.Tbl("EventMembers")} WHERE AccountId IN @AccIds",
                new { AccIds = accountIds }, tx);

            // Accounts löschen
            await _db.ExecuteAsync(
                $"DELETE FROM {_db.Tbl("Accounts")} WHERE UserId = @UserId",
                new { UserId = userId }, tx);

            // User löschen
            await _db.ExecuteAsync(
                $"DELETE FROM {_db.Tbl("Users")} WHERE Id = @UserId",
                new { UserId = userId }, tx);

            tx.Commit();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            tx.Rollback();
            return Result.Fail($"DBError: {ex.Message}");
        }
    }
    /// <summary>
    /// Ruft die Daten eines Benutzers anhand seiner ID ab.
    /// </summary>
    /// <param name="userId">Die ID des Benutzers.</param>
    /// <returns>Ein <see cref="User"/>-Objekt oder ein Fehler.</returns>
    public async Task<Result<User>> GetUser(int userId)
    {
        try
        {
            var user = await _db.QueryFirstOrDefaultAsync<User>(
                "SELECT Id, Firstname, Lastname, Password FROM Users WHERE Id = @Id",
                new { Id = userId });

            return user == null ? Result.Fail("NotFound") : Result.Ok(user);
        }
        catch(Exception ex)
        {
            return Result.Fail($"DBError: {ex.Message}");
        }
    }
    /// <summary>
    /// Aktualisiert die Daten eines Benutzers.
    /// </summary>
    /// <param name="user">Das Benutzerobjekt mit aktualisierten Feldern.</param>
    /// <returns>Ergebnis der Aktualisierung.</returns>
    public async Task<Result> EditUser(User user)
    {
        try
        {
            var rows = await _db.ExecuteAsync(@"
                UPDATE Users
                SET Firstname = @Firstname,
                    Lastname = @Lastname,
                    Password = @Password
                WHERE Id = @Id;",
                new
                {
                    user.Firstname,
                    user.Lastname,
                    user.Password,
                    user.Id
                });

            return rows > 0 ? Result.Ok() : Result.Fail("NotFound");
        }
        catch(Exception ex)
        {
            return Result.Fail($"DBError: {ex.Message}");
        }
    }
}
