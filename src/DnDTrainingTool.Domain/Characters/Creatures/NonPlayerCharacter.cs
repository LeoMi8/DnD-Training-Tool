using DnDTrainingTool.Domain.Characters.Creatures;
using DnDTrainingTool.Domain.Characters.Statistics;
namespace DndTrainingTool.Domain.Characters.Creatures;

public sealed class NonPlayerCharacter : CreatureSheet
{
    public StatBlockReference StatBlock {get; }
    public NonPlayerCharacter(
            Guid id,
            string name,
            CreatureBaseStatistics statistics,
            StatBlockReference statBlock)
            : base (id, name, statistics)
    {
        StatBlock = statBlock
            ?? throw new ArgumentNullException(nameof(statBlock));
    }
}