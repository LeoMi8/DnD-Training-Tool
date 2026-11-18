
using DnDTrainingTool.Domain.Characters.Equipment;

namespace DnDTrainingTool.Domain.Rules.ArmorClass;

public sealed class ArmorClassFormulaSelector
{
    public IReadOnlyList<ArmorClassOption> GetApplicable(
        IEnumerable<ArmorClassOption> options,
        EquipmentLoadout loadout)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(loadout);

        var applicable = new List<ArmorClassOption>();

        foreach (var option in options)
        {
            if (option is null)
                throw new ArgumentException(
                    "Options cannot contain null values.",
                    nameof(options));

            bool allRequirementsMet = option.Requirements.All(
                requirement => IsSatisfied(requirement, loadout));

            if (allRequirementsMet)
                applicable.Add(option);
        }

        return applicable.AsReadOnly();
    }

    private static bool IsSatisfied(
        EquipmentRequirement requirement,
        EquipmentLoadout loadout)
    {
        return requirement switch
        {
            EquipmentRequirement.NoArmor =>
                !loadout.IsWearingArmor,

            EquipmentRequirement.NoShield =>
                !loadout.IsUsingShield,

            EquipmentRequirement.HasArmor =>
                loadout.IsWearingArmor,

            EquipmentRequirement.HasShield =>
                loadout.IsUsingShield,

            _ => throw new ArgumentOutOfRangeException(
                nameof(requirement))
        };
    }
}
