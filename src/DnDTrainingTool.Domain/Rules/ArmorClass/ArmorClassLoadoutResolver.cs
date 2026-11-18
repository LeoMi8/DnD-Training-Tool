
using DnDTrainingTool.Domain.Characters.Equipment;
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Content;
using DnDTrainingTool.Domain.Content.Definitions;
using DnDTrainingTool.Domain.Rules.Results;

namespace DnDTrainingTool.Domain.Rules.ArmorClass;

public sealed class ArmorClassLoadoutResolver
{
    private readonly ContentRegistry _registry;
    private readonly ArmorClassFormulaSelector _selector = new();
    private readonly ArmorClassResolver _calculator = new();

    public ArmorClassLoadoutResolver(ContentRegistry registry)
    {
        _registry = registry
            ?? throw new ArgumentNullException(nameof(registry));
    }

    public CalculationResult CalculateDetailed(
        AbilityScores abilities,
        EquipmentLoadout loadout,
        bool hasShieldTraining = false,
        IEnumerable<ArmorClassOption>? specialOptions = null,
        ArmorClassOption? selectedSpecialOption = null)
    {
        ArgumentNullException.ThrowIfNull(abilities);
        ArgumentNullException.ThrowIfNull(loadout);

        var options = new List<ArmorClassOption>();

        // Build the default formula from equipment.
        if (loadout.EquippedArmorId is { } armorId)
        {
            var armor = _registry.Get<ArmorDefinition>(armorId);

            var formula = armor.Category switch
            {
                ArmorCategory.Light => new ArmorClassFormula(
                    armor.Name, armor.BaseArmorClass, true),

                ArmorCategory.Medium => new ArmorClassFormula(
                    armor.Name, armor.BaseArmorClass, true,
                    dexterityCap: 2),

                ArmorCategory.Heavy => new ArmorClassFormula(
                    armor.Name, armor.BaseArmorClass, false),

                _ => throw new InvalidOperationException(
                    "Unsupported armor category.")
            };

            options.Add(new ArmorClassOption(
                formula, EquipmentRequirement.HasArmor));
        }
        else
        {
            options.Add(new ArmorClassOption(
                new ArmorClassFormula("Unarmored", 10, true),
                EquipmentRequirement.NoArmor));
        }

        var specialList = specialOptions?.ToList()
            ?? new List<ArmorClassOption>();

        options.AddRange(specialList);

        // Filter formulas according to equipment.
        var applicable = _selector.GetApplicable(options, loadout);

        // By default, use the equipment-based formula.
        var selected = applicable[0];

        // A special formula must be explicitly selected.
        if (selectedSpecialOption is not null)
        {
            if (!specialList.Any(option =>
                    ReferenceEquals(option, selectedSpecialOption)) ||
                !applicable.Any(option =>
                    ReferenceEquals(option, selectedSpecialOption)))
            {
                throw new InvalidOperationException(
                    "Selected armor class formula is not applicable.");
            }

            selected = selectedSpecialOption;
        }

        // Apply shield bonus only with training.
        int shieldBonus = 0;

        if (loadout.EquippedShieldId is { } shieldId)
        {
            var shield = _registry.Get<ShieldDefinition>(shieldId);

            if (hasShieldTraining)
                shieldBonus = shield.ArmorClassBonus;
        }

        return _calculator.CalculateDetailed(
            abilities, selected.Formula, shieldBonus);
    }
}
