
namespace DnDTrainingTool.Domain.Rules.ArmorClass;

public sealed class ArmorClassOption
{
    public ArmorClassFormula Formula { get; }

    public IReadOnlyList<EquipmentRequirement> Requirements { get; }

    public ArmorClassOption(
        ArmorClassFormula formula,
        params EquipmentRequirement[] requirements)
    {
        Formula = formula
            ?? throw new ArgumentNullException(nameof(formula));

        ArgumentNullException.ThrowIfNull(requirements);

        if (requirements.Any(requirement =>
            !Enum.IsDefined(requirement)))
        {
            throw new ArgumentException(
                "Invalid equipment requirement.",
                nameof(requirements));
        }

        Requirements = Array.AsReadOnly(
            requirements.Distinct().ToArray());
    }
}
