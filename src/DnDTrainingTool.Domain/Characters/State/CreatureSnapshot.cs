using DnDTrainingTool.Domain.Characters.Statistics;
namespace DnDTrainingTool.Domain.Characters.State;

public sealed class CreatureSnapshot
{
    public Guid SourceCreatureId {get; }
    public string Name {get; }
    public CreatureBaseStatistics Statistics {get; }

    public CreatureSnapshot(
            Guid sourceCreatureId,
            string name,
            CreatureBaseStatistics statistics
        )
    {
        if (sourceCreatureId == Guid.Empty)
            throw new ArgumentException(
                "Source creature ID cannot be empty.",
                nameof(sourceCreatureId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Creature name cannot be empty.",
                nameof(name));

        SourceCreatureId = sourceCreatureId;
        Name = name.Trim();

        Statistics = statistics
            ?? throw new ArgumentNullException(nameof(statistics));
    }

}
