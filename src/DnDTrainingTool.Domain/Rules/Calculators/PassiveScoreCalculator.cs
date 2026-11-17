
namespace DnDTrainingTool.Domain.Rules.Calculators;

public static class PassiveScoreCalculator
{
    public static int Calculate(
        int checkBonus,
        bool advantage = false,
        bool disadvantage = false)
    {
        int result = 10 + checkBonus;

        if (advantage && !disadvantage)
            result += 5;

        if (disadvantage && !advantage)
            result -= 5;

        return result;
    }
}
