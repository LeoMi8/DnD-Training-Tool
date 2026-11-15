using System.ComponentModel;
using DndTrainingTool.Domain.Characters.Progression;

namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class CharacterProgression
{
    private readonly List<LevelUpRecord> _levelHistory = new();

    public IReadOnlyList<LevelUpRecord> LevelHistory =>
        _levelHistory.AsReadOnly();

        public int TotalLevel => _levelHistory.Count;
        public string InitialClassId => _levelHistory[0].ClassId;
        public CharacterProgression(string initialClassId)
    {
        AddLevel(initialClassId);
    }

    public void AddLevel(string classId)
    {
        var record = new LevelUpRecord(
                TotalLevel +1,
                classId
            );
        
        _levelHistory.Add(record);
    }

    public int GetClassLevel(string classId)
    {
        if (string.IsNullOrWhiteSpace(classId))
            throw new ArgumentException(
                "Class ID cannot be empty.",
                nameof(classId)
            );

        return _levelHistory.Count(record =>
            record.ClassId == classId.Trim());
    }

}