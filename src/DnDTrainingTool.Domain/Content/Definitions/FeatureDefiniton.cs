using DnDTrainingTool.Domain.Content.Identifiers;
namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class FeatureDefinition : ContentDefinition
{
    public string Description {get; }

    public FeatureDefinition(
            ContentId id,
            string name,
            string description
        ): base (id, name)
    {
        Description = description
            ?? throw new ArgumentNullException(nameof(description));
    }
}