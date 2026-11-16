
namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class ClassLevelDefinition
{
    public int Level { get; }

    public ClassLevelDefinition(int level)
    {
        if (level < 1 || level > 20)
        {
            throw new ArgumentOutOfRangeException(
                nameof(level),
                "Class level must be between 1 and 20.");
        }

        Level = level;
    }
}
