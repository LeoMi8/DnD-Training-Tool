
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Rules.Resolvers;
using Xunit;

namespace DnDTrainingTool.Tests.Rules;

public class SavingThrowResolverTests
{
    [Fact]
    public void CalculateBonus_WithProficiency_IncludesProficiencyBonus()
    {
        var abilities = new AbilityScores(
            10, 16, 12, 10, 10, 10);

        var proficiencies = new ProficiencyProfile();

        proficiencies.SetSavingThrowProficiency(
            Ability.Dexterity, true);

        var resolver = new SavingThrowResolver();

        int result = resolver.CalculateBonus(
            abilities,
            proficiencies,
            Ability.Dexterity,
            5);

        Assert.Equal(6, result);
    }

    [Fact]
    public void CalculateBonus_WithoutProficiency_UsesOnlyAbilityModifier()
    {
        var abilities = new AbilityScores(
            10, 16, 12, 10, 10, 10);

        var proficiencies = new ProficiencyProfile();
        var resolver = new SavingThrowResolver();

        int result = resolver.CalculateBonus(
            abilities,
            proficiencies,
            Ability.Dexterity,
            5);

        Assert.Equal(3, result);
    }
}
