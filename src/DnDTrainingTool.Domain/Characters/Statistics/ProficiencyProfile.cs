using System.Runtime.CompilerServices;

namespace DnDTrainingTool.Domain.Characters.Statistics;

public sealed class ProficiencyProfile
{
    private readonly Dictionary<Skill, ProficiencyLevel> _skills = new();
    private readonly HashSet<Ability> _savingThrows = new();
    public ProficiencyLevel GetSkillProficiency(Skill skill)
    {
        if (!Enum.IsDefined(skill))
            throw new ArgumentOutOfRangeException(nameof(skill));

        return _skills.GetValueOrDefault(skill);
    }

    public void SetSkillProficiency(
            Skill skill,
            ProficiencyLevel level
        )
    {
        if (!Enum.IsDefined(skill))
            throw new ArgumentOutOfRangeException(nameof(skill));
        if (!Enum.IsDefined(level))
            throw new ArgumentOutOfRangeException(nameof(level));

        if (level == ProficiencyLevel.None)
            _skills.Remove(skill);
        else
            _skills[skill] = level;
    }

    public bool HasSavingThrowProficiency(Ability ability)
    {
        if (!Enum.IsDefined(ability))
            throw new ArgumentOutOfRangeException(nameof(ability));

            return _savingThrows.Contains(ability);
    }

    public void SetSavingThrowProficiency(
            Ability ability,
            bool proficient
        )
    {
        if (!Enum.IsDefined(ability))
            throw new ArgumentOutOfRangeException(nameof(ability));

        if(proficient)
            _savingThrows.Add(ability);
        else
            _savingThrows.Remove(ability);
    }
}