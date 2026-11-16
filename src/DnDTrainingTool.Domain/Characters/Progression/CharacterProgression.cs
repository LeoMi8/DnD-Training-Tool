
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class CharacterProgression
{
    private readonly List<LevelUpRecord> _levelHistory = new();

    public IReadOnlyList<LevelUpRecord> LevelHistory =>
        _levelHistory.AsReadOnly();

    public int TotalLevel => _levelHistory.Count;

    public ContentId InitialClassId => _levelHistory[0].ClassId;

    public CharacterProgression(ContentId initialClassId)
    {
        AddLevel(initialClassId);
    }

    public void AddLevel(ContentId classId)
    {
        var record = new LevelUpRecord(
            TotalLevel + 1,
            classId);

        _levelHistory.Add(record);
    }

    public int GetClassLevel(ContentId classId)
    {
        if (string.IsNullOrWhiteSpace(classId.PackId) ||
            string.IsNullOrWhiteSpace(classId.LocalId))
        {
            throw new ArgumentException(
                "Class ID must be valid.",
                nameof(classId));
        }

        return _levelHistory.Count(record =>
            record.ClassId == classId);
    }
}
