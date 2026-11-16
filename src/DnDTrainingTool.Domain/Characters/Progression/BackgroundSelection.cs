
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class BackgroundSelection
{
    public ContentId BackgroundId { get; }

    public BackgroundSelection(ContentId backgroundId)
    {
        if (string.IsNullOrWhiteSpace(backgroundId.PackId) ||
            string.IsNullOrWhiteSpace(backgroundId.LocalId))
        {
            throw new ArgumentException(
                "Background ID must be valid.",
                nameof(backgroundId));
        }

        BackgroundId = backgroundId;
    }
}
