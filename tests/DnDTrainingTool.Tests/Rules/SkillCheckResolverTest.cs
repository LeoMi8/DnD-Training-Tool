
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Rules.Resolvers;
using Xunit;

namespace DnDTrainingTool.Tests.Rules;

public class SkillCheckResolverTests
{
    [Theory]
    [InlineData(ProficiencyLevel.None, 3)]
    [InlineData(ProficiencyLevel.Proficient, 6)]
    [InlineData(ProficiencyLevel.Expertise, 9)]
    public void CalculateBonus_UsesCorrectProficiency(
        ProficiencyLevel level,
        int expected)
    {
        var abilities = new AbilityScores(
            10, 16, 12, 10, 10, 10);

        var proficiencies = new ProficiencyProfile();

        proficiencies.SetSkillProficiency(
            Skill.Stealth, level);

        var resolver = new SkillCheckResolver();

        int result = resolver.CalculateBonus(
            abilities,
            proficiencies,
            Skill.Stealth,
            5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculateBonus_AllowsAlternativeAbility()
    {
        var abilities = new AbilityScores(
            18, 10, 12, 10, 10, 12);

        var proficiencies = new ProficiencyProfile();

        var resolver = new SkillCheckResolver();

        int result = resolver.CalculateBonus(
            abilities,
            proficiencies,
            Skill.Intimidation,
            5,
            Ability.Strength);

        Assert.Equal(4, result);
    }
}
