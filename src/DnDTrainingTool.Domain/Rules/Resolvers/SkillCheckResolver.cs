
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Rules.Calculators;
using DnDTrainingTool.Domain.Rules.Results;

namespace DnDTrainingTool.Domain.Rules.Resolvers;

public sealed class SkillCheckResolver
{
    public int CalculateBonus(
        AbilityScores abilities,
        ProficiencyProfile proficiencies,
        Skill skill,
        int characterLevel,
        Ability? abilityOverride = null)
    {
        return CalculateDetailed(
            abilities,
            proficiencies,
            skill,
            characterLevel,
            abilityOverride).Total;
    }

    public CalculationResult CalculateDetailed(
        AbilityScores abilities,
        ProficiencyProfile proficiencies,
        Skill skill,
        int characterLevel,
        Ability? abilityOverride = null)
    {
        ArgumentNullException.ThrowIfNull(abilities);
        ArgumentNullException.ThrowIfNull(proficiencies);

        Ability ability = abilityOverride ??
            SkillAbilityMapping.GetDefaultAbility(skill);

        int abilityModifier =
            AbilityModifierCalculator.Calculate(
                abilities.GetScore(ability));

        int proficiencyBonus =
            ProficiencyBonusCalculator.Calculate(characterLevel);

        var proficiencyLevel =
            proficiencies.GetSkillProficiency(skill);

        int multiplier = proficiencyLevel switch
        {
            ProficiencyLevel.None => 0,
            ProficiencyLevel.Proficient => 1,
            ProficiencyLevel.Expertise => 2,
            _ => throw new ArgumentOutOfRangeException(
                nameof(proficiencyLevel))
        };

        var contributions = new List<ModifierContribution>
        {
            new($"{ability} Modifier", abilityModifier)
        };

        if (multiplier > 0)
        {
            string source = proficiencyLevel switch
            {
                ProficiencyLevel.Proficient => "Skill Proficiency",
                ProficiencyLevel.Expertise => "Skill Expertise",
                _ => throw new InvalidOperationException()
            };

            contributions.Add(
                new ModifierContribution(
                    source,
                    proficiencyBonus * multiplier));
        }

        return new CalculationResult(contributions);
    }
}
