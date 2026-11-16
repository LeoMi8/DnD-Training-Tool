
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class LevelUpRecord
{
    private readonly List<CharacterChoice> _choices = new();

    public int CharacterLevel { get; }
    public ContentId ClassId { get; }

    public IReadOnlyList<CharacterChoice> Choices =>
        _choices.AsReadOnly();

    public LevelUpRecord(int characterLevel, ContentId classId)
    {
        if (characterLevel < 1)
            throw new ArgumentOutOfRangeException(
                nameof(characterLevel));

        if (string.IsNullOrWhiteSpace(classId.PackId) ||
            string.IsNullOrWhiteSpace(classId.LocalId))
        {
            throw new ArgumentException(
                "Class ID must be valid.",
                nameof(classId));
        }

        CharacterLevel = characterLevel;
        ClassId = classId;
    }

    public void AddChoice(CharacterChoice choice)
    {
        ArgumentNullException.ThrowIfNull(choice);

        if (_choices.Any(c => c.ChoiceId == choice.ChoiceId))
        {
            throw new InvalidOperationException(
                "A choice with this ID already exists at this level.");
        }

        _choices.Add(choice);
    }
}
