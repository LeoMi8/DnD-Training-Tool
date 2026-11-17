
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class SpeciesOptionDefinition : ContentDefinition
{
    private readonly List<GrantDefinition> _grants = new();

    public IReadOnlyList<GrantDefinition> Grants =>
        _grants.AsReadOnly();

    public SpeciesOptionDefinition(ContentId id, string name)
        : base(id, name)
    {
    }

    public void AddGrant(GrantDefinition grant)
    {
        ArgumentNullException.ThrowIfNull(grant);
        _grants.Add(grant);
    }
}
