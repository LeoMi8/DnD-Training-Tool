using DnDTraining.Domain.Characters.Statistics;

namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class EquipmentProficiencyGrantDefinition : GrantDefinition
{
    public EquipmentProficiencyType Category { get; }

    public EquipmentProficiencyGrantDefinition(EquipmentProficiencyType category)
    {
        if (!Enum.IsDefined(category))
            throw new ArgumentOutOfRangeException(nameof(category));

        Category = category;
    }
}