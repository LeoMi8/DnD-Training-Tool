
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class ShieldDefinition : ContentDefinition
{
    public int ArmorClassBonus { get; }

    public ShieldDefinition(
        ContentId id,
        string name,
        int armorClassBonus)
        : base(id, name)
    {
        if (armorClassBonus < 0)
            throw new ArgumentOutOfRangeException(nameof(armorClassBonus));

        ArmorClassBonus = armorClassBonus;
    }
}
