
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Rules.Calculators;

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
        ArgumentNullException.ThrowIfNull(abilities);
        ArgumentNullException.ThrowIfNull(proficiencies);

        Ability ability = abilityOverride ??
            SkillAbilityMapping.GetDefaultAbility(skill);

        int abilityModifier =
            AbilityModifierCalculator.Calculate(
                abilities.GetScore(ability));

        int proficiencyBonus =
            ProficiencyBonusCalculator.Calculate(characterLevel);

        var level =
            proficiencies.GetSkillProficiency(skill);

        int multiplier = level switch
        {
            ProficiencyLevel.None => 0,
            ProficiencyLevel.Proficient => 1,
            ProficiencyLevel.Expertise => 2,

            _ => throw new ArgumentOutOfRangeException(
                nameof(level))
        };

        return abilityModifier + proficiencyBonus * multiplier;
    }
}
