namespace ET.Shared.DTOs;

/// <summary>
/// Repräsentiert einen einzelnen Schritt innerhalb eines Prozesses.
/// </summary>
/// <param name="Id">Die ID des Prozessschritts.</param>
/// <param name="TypeName">Der Name des Schritttyps.</param>
/// <param name="TypeE">Typ-Kennung (Enum-Wert).</param>
/// <param name="TriggerE">Trigger-Kennung (Enum-Wert).</param>
/// <param name="ConditionE">Bedingungs-Kennung (Enum-Wert).</param>
/// <param name="OffsetInHours">Zeitversatz (z. B. in Stunden).</param>
public record ProcessStepDto(int Id, string TypeName, int TypeE, int TriggerE, int ConditionE, int OffsetInHours);
