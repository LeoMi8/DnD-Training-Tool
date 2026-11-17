
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Rules.Resolvers;
using Xunit;

namespace DnDTrainingTool.Tests.Rules;

public class InitiativeResolverTests
{
    [Fact]
    public void CalculateBonus_WithoutProficiency_UsesDexterity()
    {
        var abilities = new AbilityScores(
            10, 16, 12, 10, 10, 10);

        var resolver = new InitiativeResolver();

        int result = resolver.CalculateBonus(
            abilities,
            5);

        Assert.Equal(3, result);
    }

    [Fact]
    public void CalculateDetailed_WithProficiency_ExplainsBonus()
    {
        var abilities = new AbilityScores(
            10, 16, 12, 10, 10, 10);

        var resolver = new InitiativeResolver();

        var result = resolver.CalculateDetailed(
            abilities,
            5,
            addProficiencyBonus: true);

        Assert.Equal(6, result.Total);
        Assert.Equal(2, result.Contributions.Count);

        Assert.Contains(result.Contributions,
            contribution =>
                contribution.Source == "Dexterity Modifier"
                && contribution.Value == 3);

        Assert.Contains(result.Contributions,
            contribution =>
                contribution.Source == "Initiative Proficiency"
                && contribution.Value == 3);
    }
}
