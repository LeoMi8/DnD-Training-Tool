
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class SpeciesDefinition : ContentDefinition
{
    public string Description { get; }
    private readonly List<GrantDefinition> _grants = new();
    public IReadOnlyList<GrantDefinition> Grants =>
        _grants.AsReadOnly();
    private readonly List<ContentId> _choiceIds = new();
    public IReadOnlyList<ContentId> ChoiceIds =>
        _choiceIds.AsReadOnly();


    public SpeciesDefinition(
        ContentId id,
        string name,
        string description)
        : base(id, name)
    {
        Description = description
            ?? throw new ArgumentNullException(nameof(description)
        );
    }

    public void AddGrant(GrantDefinition grant)
    {
        ArgumentNullException.ThrowIfNull(grant);
        _grants.Add(grant);
    }

    public void AddChoice(ContentId choiceId)
    {
        if (string.IsNullOrWhiteSpace(choiceId.PackId) ||
            string.IsNullOrWhiteSpace(choiceId.LocalId))
            throw new ArgumentException(
                "Choice ID must be valid.",
                nameof(choiceId));

        if (_choiceIds.Contains(choiceId))
            throw new InvalidOperationException(
                "This choice is already assigned to the species.");

        _choiceIds.Add(choiceId);
    }
}
