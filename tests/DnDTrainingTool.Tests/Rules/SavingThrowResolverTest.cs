
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

    [Fact]
    public void CalculateDetailed_ExplainsSavingThrowBonus()
    {
        var abilities = new AbilityScores(
            10, 16, 12, 10, 10, 10);

        var proficiencies = new ProficiencyProfile();

        proficiencies.SetSavingThrowProficiency(
            Ability.Dexterity, true);

        var resolver = new SavingThrowResolver();

        var result = resolver.CalculateDetailed(
            abilities,
            proficiencies,
            Ability.Dexterity,
            5);

        Assert.Equal(6, result.Total);
        Assert.Equal(2, result.Contributions.Count);

        Assert.Contains(result.Contributions,
            contribution =>
                contribution.Source == "Dexterity Modifier" &&
                contribution.Value == 3);

        Assert.Contains(result.Contributions,
            contribution =>
                contribution.Source == "Saving Throw Proficiency" &&
                contribution.Value == 3);
    }

    [Fact]
    public void CalculateDetailed_WithExpertise_ReturnsCorrectContributions()
    {
        var abilities = new AbilityScores(
            10, 16, 12, 10, 10, 10);

        var proficiencies = new ProficiencyProfile();

        proficiencies.SetSkillProficiency(
            Skill.Stealth,
            ProficiencyLevel.Expertise);

        var resolver = new SkillCheckResolver();

        var result = resolver.CalculateDetailed(
            abilities,
            proficiencies,
            Skill.Stealth,
            5);

        Assert.Equal(9, result.Total);
        Assert.Equal(2, result.Contributions.Count);

        Assert.Contains(result.Contributions,
            contribution =>
                contribution.Source == "Dexterity Modifier" &&
                contribution.Value == 3);

        Assert.Contains(result.Contributions,
            contribution =>
                contribution.Source == "Skill Expertise" &&
                contribution.Value == 6);
    }

}
