namespace DnDTrainingTool.Domain.Characters.Statistics;

public sealed class ProficiencyProfile
{
    private readonly Dictionary<Skill, ProficiencyLevel> _skills = new();

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
}