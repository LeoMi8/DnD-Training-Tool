using DnDTrainingTool.Domain.Characters.Creatures;
using DnDTrainingTool.Domain.Characters.State;
namespace DndTrainingTool.Domain.Characters.State;

public static class CreatureInstanceFactory
{
    public static CreatureInstance Create(CreatureSheet sheet)
    {
        ArgumentNullException.ThrowIfNull(sheet);

        var snapshot = new CreatureSnapshot(
            sheet.Id,
            sheet.Name,
            sheet.Statistics
            );

            var state = new CreatureState(
                snapshot.Statistics.HitPoints.BaseHitPoints
            );

            return new CreatureInstance(
                Guid.NewGuid(),
                snapshot,
                state
            );
    }
}