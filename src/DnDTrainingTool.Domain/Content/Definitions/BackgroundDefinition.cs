using DnDTrainingTool.Domain.Content.Identifiers;
namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class BackgroundDefinition : ContentDefinition
{
    public string Description {get; }
    private readonly List<GrantDefinition> _grants = new();
    public IReadOnlyList<GrantDefinition> Grants =>
        _grants.AsReadOnly();


        public BackgroundDefinition(
                ContentId id,
                string name,
                string description
            ) : base(id, name)
    {
        Description = description
            ?? throw new ArgumentNullException(nameof(description));
    }

    public void AddGrant(GrantDefinition grant)
    {
        ArgumentNullException.ThrowIfNull(grant);
        _grants.Add(grant);
    }

}