using FluentResults;
using System.Data;

namespace ET_Backend.Repository.Authentication;
/// <summary>
/// Definiert Methoden zum Verwalten von E-Mail-Verifizierungstoken.
/// </summary>
public interface IEmailVerificationTokenRepository
{
    /// <summary>
    /// Erstellt ein neues Verifizierungstoken für einen Account.
    /// </summary>
    /// <param name="accountId">Die ID des Accounts.</param>
    /// <param name="token">Der zu speichernde Token.</param>
    /// <returns>Ein <see cref="Result"/>, der Erfolg oder Fehler angibt.</returns>
    Task<Result> CreateAsync(int accountId, string token);
    /// <summary>
    /// Ruft einen vorhandenen Verifizierungstoken ab.
    /// </summary>
    /// <param name="token">Der zu suchende Token.</param>
    /// <returns>
    /// Ein <see cref="Result{T}"/> mit der Account-ID und dem Ablaufdatum des Tokens.
    /// </returns>
    Task<Result<(int AccountId, DateTime ExpiresAt)>> GetAsync(string token);
    /// <summary>
    /// Verbraucht (löscht) einen Token innerhalb einer laufenden Transaktion.
    /// </summary>
    /// <param name="token">Der zu löschende Token.</param>
    /// <param name="conn">Die aktive Datenbankverbindung.</param>
    /// <param name="tr">Die Datenbanktransaktion.</param>
    /// <returns>Ein <see cref="Result"/>, der angibt, ob der Löschvorgang erfolgreich war.</returns>
    Task<Result> ConsumeAsync(string token, IDbConnection conn, IDbTransaction tr);
}