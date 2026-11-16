
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class ClassDefinition : ContentDefinition
{
    public string Description { get; }
    public int HitDie { get; }

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
}
