namespace ET_Frontend.Models;

/// <summary>
/// Repräsentiert einen Prozess, der aus mehreren Prozessschritten besteht.
/// </summary>
public class ProcessViewModel
{
    /// <summary>
    /// Die eindeutige ID des Prozesses.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Liste der zugehörigen Prozessschritte.
    /// </summary>
    public List<ProcessStepViewModel> ProcessSteps { get; set; }

    /// <summary>
    /// Parameterloser Konstruktor. Wird z. B. für Datenbindung oder Initialisierung verwendet.
    /// </summary>
    public ProcessViewModel() { }

    /// <summary>
    /// Erstellt eine neue Instanz von <see cref="ProcessViewModel"/>.
    /// </summary>
    /// <param name="id">Die ID des Prozesses.</param>
    /// <param name="ProcessSteps">Die Liste der zugehörigen Prozessschritte.</param>
    public ProcessViewModel(int id, List<ProcessStepViewModel> ProcessSteps)
    {
        this.Id = id;
        this.ProcessSteps = ProcessSteps;
    }
}
