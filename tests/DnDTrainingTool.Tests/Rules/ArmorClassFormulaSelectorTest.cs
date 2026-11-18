
using DnDTrainingTool.Domain.Characters.Equipment;
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Content.Identifiers;
using DnDTrainingTool.Domain.Rules.ArmorClass;
using Xunit;

namespace DnDTrainingTool.Tests.Rules;

public class ArmorClassFormulaSelectorTests
{
    [Fact]
    public void MonkDefense_BecomesUnavailableWithArmor()
    {
        var loadout = new EquipmentLoadout();

        var monkFormula = new ArmorClassFormula(
            "Monk Unarmored Defense",
            10,
            true,
            additionalAbility: Ability.Wisdom,
            allowsShield: false);

        var monkOption = new ArmorClassOption(
            monkFormula,
            EquipmentRequirement.NoArmor,
            EquipmentRequirement.NoShield);

        var selector = new ArmorClassFormulaSelector();
        var options = new[] { monkOption };

        // Initially available
        Assert.Single(selector.GetApplicable(
            options, loadout));

        // Equip armor
        loadout.EquipArmor(
            new ContentId("core", "leather-armor"));

        Assert.Empty(selector.GetApplicable(
            options, loadout));

        // Remove armor
        loadout.UnequipArmor();

        Assert.Single(selector.GetApplicable(
            options, loadout));
    }

    [Fact]
    public void MonkDefense_BecomesUnavailableWithShield()
    {
        var loadout = new EquipmentLoadout();

        var monkFormula = new ArmorClassFormula(
            "Monk Unarmored Defense",
            10,
            true,
            additionalAbility: Ability.Wisdom,
            allowsShield: false);

        var monkOption = new ArmorClassOption(
            monkFormula,
            EquipmentRequirement.NoArmor,
            EquipmentRequirement.NoShield);

        var selector = new ArmorClassFormulaSelector();

        loadout.EquipShield(
            new ContentId("core", "shield"));

        Assert.Empty(selector.GetApplicable(
            new[] { monkOption }, loadout));

        loadout.UnequipShield();

        Assert.Single(selector.GetApplicable(
            new[] { monkOption }, loadout));
    }

    [Fact]
    public void NormalUnarmoredFormula_AllowsShield()
    {
        var loadout = new EquipmentLoadout();

        var normalFormula = new ArmorClassFormula(
            "Unarmored",
            10,
            true);

        var normalOption = new ArmorClassOption(
            normalFormula,
            EquipmentRequirement.NoArmor);

        loadout.EquipShield(
            new ContentId("core", "shield"));

        var selector = new ArmorClassFormulaSelector();

        Assert.Single(selector.GetApplicable(
            new[] { normalOption }, loadout));
    }
}

