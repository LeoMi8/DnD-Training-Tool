
namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class ClassLevelDefinition
{
    private readonly List<GrantDefinition> _grants = new();

    public int Level { get; }

    public IReadOnlyList<GrantDefinition> Grants =>
        _grants.AsReadOnly();

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

    public void AddGrant(GrantDefinition grant)
    {
        ArgumentNullException.ThrowIfNull(grant);
        _grants.Add(grant);
    }
}
