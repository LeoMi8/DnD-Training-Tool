
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Rules.Calculators;
using DnDTrainingTool.Domain.Rules.Results;

namespace DnDTrainingTool.Domain.Rules.ArmorClass;

public sealed class ArmorClassResolver
{
    public int Calculate(
        AbilityScores abilities,
        ArmorClassFormula formula,
        int shieldBonus = 0)
    {
        return CalculateDetailed(
            abilities, formula, shieldBonus).Total;
    }

    public CalculationResult CalculateDetailed(
        AbilityScores abilities,
        ArmorClassFormula formula,
        int shieldBonus = 0)
    {
        ArgumentNullException.ThrowIfNull(abilities);
        ArgumentNullException.ThrowIfNull(formula);

        if (shieldBonus < 0)
            throw new ArgumentOutOfRangeException(
                nameof(shieldBonus));

        if (shieldBonus > 0 && !formula.AllowsShield)
            throw new InvalidOperationException(
                $"Formula '{formula.Name}' does not allow shields.");

        var contributions = new List<ModifierContribution>
        {
            new(formula.Name, formula.BaseValue)
        };

        if (formula.IncludeDexterity)
        {
            int dexterityModifier =
                AbilityModifierCalculator.Calculate(
                    abilities.GetScore(Ability.Dexterity));

            if (formula.DexterityCap.HasValue)
            {
                dexterityModifier = Math.Min(
                    dexterityModifier,
                    formula.DexterityCap.Value);
            }

            contributions.Add(new ModifierContribution(
                "Dexterity Modifier",
                dexterityModifier));
        }

        if (formula.AdditionalAbility.HasValue)
        {
            Ability ability = formula.AdditionalAbility.Value;

            int modifier =
                AbilityModifierCalculator.Calculate(
                    abilities.GetScore(ability));

            contributions.Add(new ModifierContribution(
                $"{ability} Modifier",
                modifier));
        }

        if (shieldBonus > 0)
        {
            contributions.Add(new ModifierContribution(
                "Shield",
                shieldBonus));
        }

        return new CalculationResult(contributions);
    }
}
