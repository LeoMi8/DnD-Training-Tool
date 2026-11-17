
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Rules.Calculators;
using DnDTrainingTool.Domain.Rules.Results;

namespace DnDTrainingTool.Domain.Rules.Resolvers;

public sealed class SavingThrowResolver
{
    public int CalculateBonus(
        AbilityScores abilities,
        ProficiencyProfile proficiencies,
        Ability ability,
        int characterLevel)
    {
        return CalculateDetailed(
            abilities,
            proficiencies,
            ability,
            characterLevel).Total;
    }

    public CalculationResult CalculateDetailed(
        AbilityScores abilities,
        ProficiencyProfile proficiencies,
        Ability ability,
        int characterLevel)
    {
        ArgumentNullException.ThrowIfNull(abilities);
        ArgumentNullException.ThrowIfNull(proficiencies);

        int abilityModifier =
            AbilityModifierCalculator.Calculate(
                abilities.GetScore(ability));

        int proficiencyBonus =
            ProficiencyBonusCalculator.Calculate(characterLevel);

        var contributions = new List<ModifierContribution>
        {
            new($"{ability} Modifier", abilityModifier)
        };

        if (proficiencies.HasSavingThrowProficiency(ability))
        {
            contributions.Add(
                new ModifierContribution(
                    "Saving Throw Proficiency",
                    proficiencyBonus));
        }

        return new CalculationResult(contributions);
    }
}
