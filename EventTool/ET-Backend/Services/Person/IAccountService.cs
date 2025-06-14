using ET_Backend.Models;
using FluentResults;

namespace ET_Backend.Services.Person;
/// <summary>
/// Definiert die Schnittstelle für Services zur Verwaltung von Benutzerkonten.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Ruft ein Benutzerkonto anhand der E-Mail-Adresse ab.
    /// </summary>
    /// <param name="accountEMail">Die E-Mail-Adresse des gesuchten Kontos.</param>
    /// <returns>
    /// Ein <see cref="Result{T}"/> mit dem <see cref="Account"/>,
    /// oder ein Fehler, falls das Konto nicht gefunden wurde.
    /// </returns>
    public Task<Result<Account>> GetAccountByMail(string accountEMail);
    public Task<Result<AccountService.ResolveResult>> ResolveEmailsAsync(
        List<string> organizerMails,
        List<string> contactMails,
        List<string> participantMails);
}