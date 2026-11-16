using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Definitions;

public abstract class GrantDefinition
{
    protected GrantDefinition()
    {
    }
}

public sealed class FeatureGrantDefinition : GrantDefinition
{
    public ContentId FeatureId {get; }
    public FeatureGrantDefinition(ContentId featureId)
    {
        if (string.IsNullOrWhiteSpace(featureId.PackId) ||
            string.IsNullOrWhiteSpace(featureId.LocalId))
        {
            throw new ArgumentException(
                "Feature ID must be valid.",
                nameof(featureId)
            );
        }

        FeatureId = featureId;
    }
}