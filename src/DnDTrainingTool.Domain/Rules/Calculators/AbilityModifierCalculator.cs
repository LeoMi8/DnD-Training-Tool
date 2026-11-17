namespace DnDTrainingTool.Domain.Rules.Calculators;

public static class AbilityModifierCalculator
{
    public static int Calculate(int abilityScore)
    {
         if (abilityScore < 0 || abilityScore > 30)
        {
            throw new ArgumentOutOfRangeException(
                nameof(abilityScore),
                "Ability score must be between 0 and 30.");
        }
        return (int)Math.Floor((abilityScore - 10)/2.0);
    }
}