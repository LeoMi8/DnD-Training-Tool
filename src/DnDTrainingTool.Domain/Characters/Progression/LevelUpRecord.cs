using DnDTrainingTool.Domain.Characters.Progression;

namespace DndTrainingTool.Domain.Characters.Progression;

public sealed class LevelUpRecord
{
    private readonly List<CharacterChoice> _choices = new();
    public int CharacterLevel {get; }
    public string ClassId {get; }

    public IReadOnlyList<CharacterChoice> Choices =>
        _choices.AsReadOnly();

    public LevelUpRecord (
            int characterLevel,
            string classId
        )
    {
        if (characterLevel < 1)
            throw new ArgumentOutOfRangeException(
                nameof(characterLevel)
            );

        if (string.IsNullOrWhiteSpace(classId))
            throw new ArgumentException(
                "Class ID cannot be empty",
                nameof(classId)
            );
        
        CharacterLevel = characterLevel;
        ClassId = classId.Trim();
    }

    public void AddChoice (CharacterChoice choice)
    {
        ArgumentNullException.ThrowIfNull(choice);

        if(_choices.Any(c => c.ChoiceId == choice.ChoiceId))
            throw new InvalidOperationException(
                "A choice with this ID already exists at this level."
            );
        _choices.Add(choice);
    }
}