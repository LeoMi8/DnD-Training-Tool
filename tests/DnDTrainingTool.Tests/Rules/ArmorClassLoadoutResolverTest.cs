
using DnDTrainingTool.Domain.Content;
using DnDTrainingTool.Domain.Characters.Equipment;
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Content.Definitions;
using DnDTrainingTool.Domain.Content.Identifiers;
using DnDTrainingTool.Domain.Rules.ArmorClass;
using Xunit;

namespace DnDTrainingTool.Tests.Rules;

public class ArmorClassLoadoutResolverTests
{
    [Fact]
    public void EquippingAndRemovingArmor_UpdatesArmorClass()
    {
        var registry = new ContentRegistry();

        var armorId = new ContentId("test", "chain-mail");

        registry.Register(new ArmorDefinition(
            armorId, "Chain Mail", ArmorCategory.Heavy, 16));

        var abilities = new AbilityScores(
            10, 16, 12, 10, 14, 10);

        var loadout = new EquipmentLoadout();
        var resolver = new ArmorClassLoadoutResolver(registry);

        // No armor: 10 + 3
        Assert.Equal(13, resolver.CalculateDetailed(
            abilities, loadout).Total);

        // Chain Mail: fixed AC 16
        loadout.EquipArmor(armorId);

        Assert.Equal(16, resolver.CalculateDetailed(
            abilities, loadout).Total);

        // Remove armor: return to normal AC
        loadout.UnequipArmor();

        Assert.Equal(13, resolver.CalculateDetailed(
            abilities, loadout).Total);
    }

    [Fact]
    public void MonkDefense_RequiresNoArmorAndNoShield()
    {
        var registry = new ContentRegistry();

        var abilities = new AbilityScores(
            10, 16, 12, 10, 14, 10);

        var loadout = new EquipmentLoadout();

        var monkOption = new ArmorClassOption(
            new ArmorClassFormula(
                "Monk Unarmored Defense",
                10,
                true,
                additionalAbility: Ability.Wisdom,
                allowsShield: false),
            EquipmentRequirement.NoArmor,
            EquipmentRequirement.NoShield);

        var resolver = new ArmorClassLoadoutResolver(registry);
        var specialOptions = new[] { monkOption };

        // Monk formula: 10 + 3 + 2
        var result = resolver.CalculateDetailed(
            abilities,
            loadout,
            specialOptions: specialOptions,
            selectedSpecialOption: monkOption);

        Assert.Equal(15, result.Total);

        // Equipping a shield makes Monk defense unavailable.
        loadout.EquipShield(
            new ContentId("test", "shield"));

        Assert.Throws<InvalidOperationException>(() =>
            resolver.CalculateDetailed(
                abilities,
                loadout,
                specialOptions: specialOptions,
                selectedSpecialOption: monkOption));
    }

    [Fact]
    public void Shield_RequiresTrainingToGrantBonus()
    {
        var registry = new ContentRegistry();

        var shieldId = new ContentId("test", "shield");

        registry.Register(new ShieldDefinition(
            shieldId, "Shield", 2));

        var abilities = new AbilityScores(
            10, 16, 12, 10, 14, 10);

        var loadout = new EquipmentLoadout();
        loadout.EquipShield(shieldId);

        var resolver = new ArmorClassLoadoutResolver(registry);

        Assert.Equal(13, resolver.CalculateDetailed(
            abilities, loadout, hasShieldTraining: false).Total);

        Assert.Equal(15, resolver.CalculateDetailed(
            abilities, loadout, hasShieldTraining: true).Total);
    }
}
