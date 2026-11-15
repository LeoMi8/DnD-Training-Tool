using System.Dynamic;

namespace DnDTrainingTool.Domain.Characters.Statistics;

public sealed class CreatureBaseStatistics
{
    public AbilityScores Abilities {get; }
    public HitPointProfile HitPoints{get; }
    public MovementProfile Movement{get; }
    public SenseProfile Senses{get; }
    public DefenseProfile Defenses{get; }
    public ProficiencyProfile Proficiencies{get; }

    public CreatureBaseStatistics
        (
        AbilityScores abilities,
        HitPointProfile hitPoints,
        MovementProfile movement,
        SenseProfile senses,
        DefenseProfile defenses,
        ProficiencyProfile proficiencies
        )
    {
        Abilities = abilities
            ?? throw new ArgumentNullException(nameof(abilities));
        HitPoints = hitPoints
            ?? throw new ArgumentNullException(nameof(hitPoints));
        Movement = movement
            ?? throw new ArgumentNullException(nameof(movement));
        Senses = senses
            ?? throw new ArgumentNullException(nameof(senses));
        Defenses = defenses
            ?? throw new ArgumentNullException(nameof(defenses));
        Proficiencies = proficiencies
            ?? throw new ArgumentNullException(nameof(proficiencies));
            
    }
}