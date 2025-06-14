using ET_Backend.Models;
using ET_Backend.Repository.Person;
using FluentResults;

namespace ET_Backend.Services.Person;
/// <summary>
/// Service-Klasse zur Kapselung der Geschäftslogik rund um Benutzerkonten.
/// </summary>
public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    /// <summary>
    /// Initialisiert eine neue Instanz des <see cref="AccountService"/>.
    /// </summary>
    /// <param name="accountRepository">Das zugrunde liegende Repository zur Account-Verwaltung.</param>
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    /// <summary>
    /// Ruft ein Benutzerkonto anhand der E-Mail-Adresse ab.
    /// </summary>
    /// <param name="accountEMail">Die E-Mail-Adresse des gesuchten Kontos.</param>
    /// <returns>
    /// Ein <see cref="Result{T}"/> mit dem <see cref="Account"/>, 
    /// oder ein Fehler, falls das Konto nicht gefunden wurde.
    /// </returns>
    public async Task<Result<Account>> GetAccountByMail(string accountEMail)
    {
        return await _accountRepository.GetAccount(accountEMail);
    }

    public async Task<Result<ResolveResult>> ResolveEmailsAsync(
        List<string> organizerMails,
        List<string> contactMails,
        List<string> participantMails)
    {
        var allMails = organizerMails
            .Concat(contactMails)
            .Concat(participantMails)
            .Distinct()
            .ToList();

        var accRes = await _accountRepository.GetAccountsByMail(allMails);
        if (accRes.IsFailed) return Result.Fail(accRes.Errors);

        var dict = accRes.Value.ToDictionary(a => a.EMail, a => a);

        List<Account> Pick(IEnumerable<string> emails) =>
            emails.Where(dict.ContainsKey).Select(m => dict[m]).ToList();

        return Result.Ok(new ResolveResult(
            Pick(organizerMails),
            Pick(contactMails),
            Pick(participantMails)));
    }
    public record ResolveResult(
        List<Account> Organizers,
        List<Account> ContactPersons,
        List<Account> Participants);
}