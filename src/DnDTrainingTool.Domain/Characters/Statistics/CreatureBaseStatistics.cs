namespace DnDTrainingTool.Domain.Characters.Statistics;

public sealed class CreatureBaseStatistics
{
    public AbilityScores Abilities {get; }
    public CreatureBaseStatistics(AbilityScores abilities)
    {
        Abilities = abilities
        ?? throw new ArgumentNullException(nameof(abilities));
    }
}