using System.ComponentModel;

namespace DnDTrainingTool.Domain.Characters.Statistics;

public sealed class MovementProfile
{
    private readonly Dictionary<MovementType, int> _speeds = new();

    public MovementProfile(int walkingSpeed)
    {
        SetSpeed(MovementType.Walking, walkingSpeed);
    }
    
    public int GetSpeed(MovementType type)
    {
        return _speeds.GetValueOrDefault(type);
    }

    public void SetSpeed(MovementType type, int speed)
    {
        if (!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(nameof(type));

        if (speed < 0)
            throw new ArgumentOutOfRangeException(
                nameof(speed),
                "Movement speed cannot be negative."
            );
        
        _speeds[type] = speed;
    }
}