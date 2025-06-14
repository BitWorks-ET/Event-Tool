﻿using System.Security.Claims;
using ET_Backend.Models;
using ET_Backend.Repository.Person;
using ET_Backend.Services.Mapping;
using ET.Shared.DTOs;
using FluentResults;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ET_Backend.Services.Person
{
    /// <summary>
    /// Implementiert die Geschäftslogik für Benutzeroperationen wie Profilverwaltung,
    /// Mitgliedschaften, E-Mail-Änderungen und Benutzerlöschung.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepo;

        /// <summary>
        /// Erstellt eine neue Instanz des <see cref="UserService"/>.
        /// </summary>
        public UserService(IUserRepository userRepository, IAccountRepository accountRepo)
        {
            _userRepository = userRepository;
            _accountRepo = accountRepo;
        }

        /// <inheritdoc />
        public async Task<Account?> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(idClaim, out var accountId))
                return null;

            var result = await _accountRepo.GetAccount(accountId);
            return result.IsSuccess ? result.Value : null;
        }

        /// <inheritdoc />
        public async Task<Result> UpdateUserAsync(UserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
                return Result.Fail("Name und Nachname dürfen nicht leer sein.");

            var getResult = await _userRepository.GetUser(dto.Id);
            if (getResult.IsFailed || getResult.Value is null)
                return Result.Fail("Benutzer nicht gefunden.");

            var user = getResult.Value;

            user.Firstname = dto.FirstName;
            user.Lastname = dto.LastName;

            // Nur aktualisieren, wenn Passwort gesetzt wurde
            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.Password = dto.Password;

            return await _userRepository.EditUser(user);
        }

        /// <inheritdoc />
        public async Task<Result<UserDto>> GetUserAsync(int id)
        {
            var get = await _userRepository.GetUser(id);
            if (get.IsFailed) return Result.Fail(get.Errors);

            return Result.Ok(UserMapper.ToDto(get.Value));
        }

        /// <inheritdoc />
        public async Task<Result<List<MembershipDto>>> GetMembershipsAsync(int id)
        {
            var accs = await _accountRepo.GetAccountsByUser(id);
            if (accs.IsFailed) return Result.Fail(accs.Errors);

            var list = accs.Value.Select(a => new MembershipDto(
                a.Id,
                a.Organization.Id,
                a.Organization.Name,
                a.EMail)).ToList();

            return Result.Ok(list);
        }

        /// <inheritdoc />
        public async Task<Result> UpdateEmailAsync(int accountId, string newEmail)
        {
            // einfache E-Mail-Validierung
            if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains('@'))
                return Result.Fail("Ungültige E-Mail-Adresse.");

            return await _accountRepo.UpdateEmail(accountId, newEmail);
        }

        /// <inheritdoc />
        public async Task<Result> DeleteMembershipAsync(int accountId, int orgId)
            => await _accountRepo.RemoveFromOrganization(accountId, orgId);

        /// <inheritdoc />
        public async Task<Result> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteUser(userId);
        }

        public async Task<Account?> GetCurrentAccountAsync(ClaimsPrincipal user)
        {
            var accIdStr = user.FindFirst("accountId")?.Value;
            if (!int.TryParse(accIdStr, out var accId))
                return null;

            var result = await _accountRepo.GetAccount(accId);
            return result.IsSuccess ? result.Value : null;
        }
    }
}
