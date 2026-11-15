namespace DndTrainingTool.Domain.Characters.Progression;

public sealed class LevelUpRecord
{
    public int CharacterLevel {get; }
    public string ClassId {get; }

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
}