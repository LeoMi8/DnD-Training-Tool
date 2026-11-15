namespace DnDTrainingTool.Domain.Characters.Statistics;

public sealed class HitPointProfile
{
    public int BaseHitPoints {get; }

    public HitPointProfile(int baseHitPoints)
    {
        if (baseHitPoints < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof (baseHitPoints),
                "Base hit points must be at least 1."
            );
        }

        BaseHitPoints = baseHitPoints;
    }
}