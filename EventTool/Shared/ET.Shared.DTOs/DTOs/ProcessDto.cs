namespace ET.Shared.DTOs;
/// <summary>
/// Repräsentiert einen Prozess mit einer Liste von Prozessschritten.
/// </summary>
/// <param name="Id">Die eindeutige ID des Prozesses.</param>
/// <param name="ProcessSteps">Die Liste der zugehörigen Prozessschritte.</param>
public record ProcessDto(int Id, List<ProcessStepDto> ProcessSteps);
