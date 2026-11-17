
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Rules.Calculators;

namespace DnDTrainingTool.Domain.Rules.Resolvers;

public sealed class SavingThrowResolver
{
    public int CalculateBonus(
        AbilityScores abilities,
        ProficiencyProfile proficiencies,
        Ability ability,
        int characterLevel)
    {
        ArgumentNullException.ThrowIfNull(abilities);
        ArgumentNullException.ThrowIfNull(proficiencies);

        int abilityScore = abilities.GetScore(ability);

        int abilityModifier =
            AbilityModifierCalculator.Calculate(abilityScore);

        int proficiencyBonus =
            proficiencies.HasSavingThrowProficiency(ability)
                ? ProficiencyBonusCalculator.Calculate(characterLevel)
                : 0;

        return abilityModifier + proficiencyBonus;
    }
}
