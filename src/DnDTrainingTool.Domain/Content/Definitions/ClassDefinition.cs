
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class ClassDefinition : ContentDefinition
{
    public string Description { get; }
    public int HitDie { get; }
    private readonly Dictionary<int, ClassLevelDefinition> _levels = new();

    public IReadOnlyCollection<ClassLevelDefinition> Levels =>
        _levels.Values.ToList().AsReadOnly();

    public ClassDefinition(
        ContentId id,
        string name,
        string description,
        int hitDie)
        : base(id, name)
    {
        if (hitDie is not (6 or 8 or 10 or 12))
        {
            throw new ArgumentOutOfRangeException(
                nameof(hitDie),
                "Class hit die must be d6, d8, d10 or d12.");
        }

        Description = description
            ?? throw new ArgumentNullException(nameof(description));

        HitDie = hitDie;
    }

    public void AddLevelDefinition(ClassLevelDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        if (!_levels.TryAdd(definition.Level, definition))
        {
            throw new InvalidOperationException(
                $"Level '{definition.Level}' is already defined for this class."
            );
        }
    }

    public ClassLevelDefinition GetLevelDefinition(int level)
    {
        if (!_levels.TryGetValue(level, out var definition))
        {
            throw new KeyNotFoundException(
                $"Level '{level}' is not defined for class '{Name}'."
            );
        }
        return definition;
    }
}
