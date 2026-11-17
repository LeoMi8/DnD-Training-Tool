
namespace DnDTrainingTool.Domain.Rules.Calculators;

public static class ProficiencyBonusCalculator
{
    public static int Calculate(int characterLevel)
    {
        if (characterLevel < 1 || characterLevel > 20)
        {
            throw new ArgumentOutOfRangeException(
                nameof(characterLevel),
                "Character level must be between 1 and 20.");
        }

        return 2 + (characterLevel - 1) / 4;
    }
}
