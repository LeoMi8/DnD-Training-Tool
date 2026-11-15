namespace DnDTrainingTool.Domain.Characters.Statistics;

public sealed class SenseProfile
{
    private readonly Dictionary<SenseType, int> _senses = new();
    
    public int GetRange(SenseType type)
    {
        if (!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(nameof(type));

            return _senses.GetValueOrDefault(type);
    }

    public void SetRange(SenseType type, int range)
    {
        if (!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(nameof(type));
        if (range < 0)
            throw new ArgumentOutOfRangeException(
                nameof(type),
                "sense range cannot be negative."
            );
        if (range == 0)
            _senses.Remove(type);
        else
            _senses[type] = range;
    }
}