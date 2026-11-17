
using DnDTrainingTool.Domain.Rules.Calculators;
using Xunit;

namespace DnDTrainingTool.Tests.Rules;

public class CalculatorTests
{
    [Theory]
    [InlineData(8, -1)]
    [InlineData(9, -1)]
    [InlineData(10, 0)]
    [InlineData(13, 1)]
    [InlineData(16, 3)]
    [InlineData(20, 5)]
    public void AbilityModifier_ShouldBeCorrect(
        int score,
        int expected)
    {
        Assert.Equal(
            expected,
            AbilityModifierCalculator.Calculate(score));
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(4, 2)]
    [InlineData(5, 3)]
    [InlineData(8, 3)]
    [InlineData(9, 4)]
    [InlineData(13, 5)]
    [InlineData(17, 6)]
    [InlineData(20, 6)]
    public void ProficiencyBonus_ShouldBeCorrect(
        int level,
        int expected)
    {
        Assert.Equal(
            expected,
            ProficiencyBonusCalculator.Calculate(level));
    }
}
