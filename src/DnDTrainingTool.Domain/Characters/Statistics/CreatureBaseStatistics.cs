namespace DnDTrainingTool.Domain.Characters.Statistics;

public sealed class CreatureBaseStatistics
{
    public AbilityScores Abilities {get; }
    public HitPointProfile HitPoints{get; }

    public CreatureBaseStatistics(AbilityScores abilities, HitPointProfile hitPoints)
    {
        Abilities = abilities
            ?? throw new ArgumentNullException(nameof(abilities));
        HitPoints = hitPoints
            ?? throw new ArgumentNullException(nameof(hitPoints));
    }
}