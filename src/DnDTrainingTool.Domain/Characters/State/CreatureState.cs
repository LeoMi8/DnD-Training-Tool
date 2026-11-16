namespace DnDTrainingTool.Domain.Characters.State;

public sealed class CreatureState
{
    public int CurrentHitPoints {get; private set; }
    public int TemporaryHitPoints {get; private set; }

    public CreatureState(int maximumHitPoints)
    {
        if (maximumHitPoints < 1)
            throw new ArgumentOutOfRangeException(
                nameof(maximumHitPoints),
                "Maximum hit points must be at least 1."
            );

        CurrentHitPoints = maximumHitPoints;
        TemporaryHitPoints = 0;
    }

    public void SetCurrentHitPoints (int value, int maximumHitPoints)
    {
        if (maximumHitPoints < 1)
            throw new ArgumentOutOfRangeException(
            nameof(maximumHitPoints)
            );

        CurrentHitPoints = Math.Clamp(
            value, 0, maximumHitPoints);
    }

    public void SetTemporaryHitPoints(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        TemporaryHitPoints = value;
    }
}