
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class SubclassDefinition : ContentDefinition
{
    private readonly Dictionary<int, List<GrantDefinition>> _levelGrants = new();

    public ContentId ParentClassId { get; }
    public string Description { get; }

    public SubclassDefinition(
        ContentId id,
        string name,
        string description,
        ContentId parentClassId)
        : base(id, name)
    {
        if (string.IsNullOrWhiteSpace(parentClassId.PackId) ||
            string.IsNullOrWhiteSpace(parentClassId.LocalId))
        {
            throw new ArgumentException(
                "Parent class ID must be valid.",
                nameof(parentClassId));
        }

        Description = description
            ?? throw new ArgumentNullException(nameof(description));

        ParentClassId = parentClassId;
    }

    public void AddLevelGrant(int level, GrantDefinition grant)
    {
        if (level < 1 || level > 20)
            throw new ArgumentOutOfRangeException(nameof(level));

        ArgumentNullException.ThrowIfNull(grant);

        if (!_levelGrants.TryGetValue(level, out var grants))
        {
            grants = new List<GrantDefinition>();
            _levelGrants[level] = grants;
        }

        grants.Add(grant);
    }

    public IReadOnlyList<GrantDefinition> GetLevelGrants(int level)
    {
        if (level < 1 || level > 20)
            throw new ArgumentOutOfRangeException(nameof(level));

        return _levelGrants.TryGetValue(level, out var grants)
            ? grants.AsReadOnly()
            : Array.Empty<GrantDefinition>();
    }
}
