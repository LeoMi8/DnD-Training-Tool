
using DnDTrainingTool.Domain.Rules.Calculators;
using Xunit;

namespace DnDTrainingTool.Tests.Rules;

public class PassiveScoreCalculatorTests
{
    [Theory]
    [InlineData(6, false, false, 16)]
    [InlineData(6, true, false, 21)]
    [InlineData(6, false, true, 11)]
    [InlineData(6, true, true, 16)]
    [InlineData(-2, false, false, 8)]
    public void Calculate_ReturnsExpectedPassiveScore(
        int checkBonus,
        bool advantage,
        bool disadvantage,
        int expected)
    {
        int result = PassiveScoreCalculator.Calculate(
            checkBonus,
            advantage,
            disadvantage);

        Assert.Equal(expected, result);
    }
}
