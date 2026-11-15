using DnDTrainingTool.Domain.Characters.Progression;
using DnDTrainingTool.Domain.Characters.Statistics;

namespace DnDTrainingTool.Domain.Characters.Creatures;

public sealed class PlayerCharacter : CreatureSheet
{
    public CharacterBuild Build {get; }

    public PlayerCharacter(
            Guid id,
            string name,
            CreatureBaseStatistics statistics,
            CharacterBuild build    
        ) : base(id, name, statistics)
    {
        Build = build
            ?? throw new ArgumentNullException(nameof(build));
    }
}