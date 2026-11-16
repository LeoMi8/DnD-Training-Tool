using DnDTrainingTool.Domain.Content.Definitions;
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Resolution;

public sealed class ResolvedGrant
{
    public ContentId SourceId {get; }
    public int? SourceLevel {get; }
    public GrantDefinition Grant {get; }

    public ResolvedGrant(
            ContentId sourceId,
            int? sourceLevel,
            GrantDefinition grant
        )
    {
        if (string.IsNullOrWhiteSpace(sourceId.PackId) ||
            string.IsNullOrWhiteSpace(sourceId.LocalId))
            throw new ArgumentException(
                "Source ID must be valid.",
                nameof(sourceId)
            );

        if (sourceLevel is < 1 or > 20)
            throw new ArgumentOutOfRangeException(nameof(sourceLevel)
        );

        SourceId = sourceId;
        SourceLevel = sourceLevel;
        Grant = grant
            ?? throw new ArgumentNullException(nameof(grant));
    }
}