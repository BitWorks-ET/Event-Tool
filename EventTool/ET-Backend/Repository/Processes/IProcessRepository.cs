using ET_Backend.Models;
using FluentResults;

namespace ET_Backend.Repository.Processes;
/// <summary>
/// Definiert Methoden für den Zugriff auf Prozessdaten.
/// </summary>
public interface IProcessRepository
{
    Task<Result<Process>> GetProcess(int id);
}