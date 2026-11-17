
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Rules.Calculators;
using DnDTrainingTool.Domain.Rules.Results;

namespace DnDTrainingTool.Domain.Rules.Resolvers;

public sealed class InitiativeResolver
{
    public int CalculateBonus(
        AbilityScores abilities,
        int characterLevel,
        bool addProficiencyBonus = false)
    {
        return CalculateDetailed(
            abilities,
            characterLevel,
            addProficiencyBonus).Total;
    }

    public CalculationResult CalculateDetailed(
        AbilityScores abilities,
        int characterLevel,
        bool addProficiencyBonus = false)
    {
        ArgumentNullException.ThrowIfNull(abilities);

        int dexterityModifier =
            AbilityModifierCalculator.Calculate(
                abilities.GetScore(Ability.Dexterity));

        int proficiencyBonus =
            ProficiencyBonusCalculator.Calculate(characterLevel);

        var contributions = new List<ModifierContribution>
        {
            new("Dexterity Modifier", dexterityModifier)
        };

        if (addProficiencyBonus)
        {
            contributions.Add(
                new ModifierContribution(
                    "Initiative Proficiency",
                    proficiencyBonus));
        }

        return new CalculationResult(contributions);
    }
}
