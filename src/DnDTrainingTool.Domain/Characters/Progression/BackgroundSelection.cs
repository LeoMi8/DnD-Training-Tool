namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class BackgroundSelection
{
    public string BackgroundId {get; }

    public BackgroundSelection(string backgroundId)
    {
        if (string.IsNullOrWhiteSpace(backgroundId))
            throw new ArgumentException(
                "Background ID cannot be empty.",
                nameof(backgroundId)
            );
        BackgroundId = backgroundId.Trim();
    }
}