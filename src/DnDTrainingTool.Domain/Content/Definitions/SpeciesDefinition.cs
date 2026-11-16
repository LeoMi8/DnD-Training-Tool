
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class SpeciesDefinition : ContentDefinition
{
    public string Description { get; }

    public SpeciesDefinition(
        ContentId id,
        string name,
        string description)
        : base(id, name)
    {
        Description = description
            ?? throw new ArgumentNullException(nameof(description)
        );
    }
}
