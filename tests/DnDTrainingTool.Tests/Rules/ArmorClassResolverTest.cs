
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Rules.ArmorClass;
using Xunit;

namespace DnDTrainingTool.Tests.Rules;

public class ArmorClassResolverTests
{
    [Fact]
    public void MonkDefense_UsesDexterityAndWisdom()
    {
        var abilities = new AbilityScores(
            10, 16, 12, 10, 14, 10);

        var formula = new ArmorClassFormula(
            "Monk Unarmored Defense",
            10,
            true,
            additionalAbility: Ability.Wisdom,
            allowsShield: false);

        var resolver = new ArmorClassResolver();

        var result = resolver.CalculateDetailed(
            abilities, formula);

        Assert.Equal(15, result.Total);
        Assert.Equal(3, result.Contributions.Count);
    }

    [Fact]
    public void MediumArmor_CapsDexterityBonus()
    {
        var abilities = new AbilityScores(
            10, 18, 12, 10, 10, 10);

        var formula = new ArmorClassFormula(
            "Chain Shirt",
            13,
            true,
            dexterityCap: 2);

        var resolver = new ArmorClassResolver();

        Assert.Equal(15,
            resolver.Calculate(abilities, formula));
    }

    [Fact]
    public void HeavyArmor_IgnoresDexterity()
    {
        var abilities = new AbilityScores(
            10, 8, 12, 10, 10, 10);

        var formula = new ArmorClassFormula(
            "Chain Mail",
            16,
            false);

        var resolver = new ArmorClassResolver();

        Assert.Equal(16,
            resolver.Calculate(abilities, formula));
    }

    [Fact]
    public void MonkDefense_RejectsShieldBonus()
    {
        var abilities = new AbilityScores(
            10, 16, 12, 10, 14, 10);

        var formula = new ArmorClassFormula(
            "Monk Unarmored Defense",
            10,
            true,
            additionalAbility: Ability.Wisdom,
            allowsShield: false);

        var resolver = new ArmorClassResolver();

        Assert.Throws<InvalidOperationException>(() =>
            resolver.Calculate(
                abilities,
                formula,
                shieldBonus: 2));
    }
}
