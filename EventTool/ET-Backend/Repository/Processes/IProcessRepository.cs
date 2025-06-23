using ET.Shared.DTOs;
using ET_Backend.Models;
using FluentResults;

namespace ET_Backend.Repository.Processes;

/// <summary>
/// Definiert Zugriffsmethoden auf Prozessdaten in der Datenbank.
/// </summary>

public interface IProcessRepository
{
    Task<Result<Process>> CreateAsync(int eventId, Process proc);
    Task<Result<Process>> UpdateAsync(int eventId, Process proc);
    Task<Result<Process>> GetByEvent   (int eventId);
}