
using DnDTrainingTool.Domain.Characters.Equipment;
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class ArmorDefinition : ContentDefinition
{
    public ArmorCategory Category { get; }
    public int BaseArmorClass { get; }

    public ArmorDefinition(
        ContentId id,
        string name,
        ArmorCategory category,
        int baseArmorClass)
        : base(id, name)
    {
        if (!Enum.IsDefined(category))
            throw new ArgumentOutOfRangeException(nameof(category));

        if (baseArmorClass < 0)
            throw new ArgumentOutOfRangeException(nameof(baseArmorClass));

        Category = category;
        BaseArmorClass = baseArmorClass;
    }
}
