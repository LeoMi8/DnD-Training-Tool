
using System.ComponentModel;
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class ClassLevelDefinition
{
    private readonly List<GrantDefinition> _grants = new();
    public IReadOnlyList<GrantDefinition> Grants =>
        _grants.AsReadOnly();
    public int Level { get; }
   
    private readonly List<ContentId> _choiceIds = new();
    public IReadOnlyList<ContentId> ChoiceIds =>
        _choiceIds.AsReadOnly();


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

        public void AddChoice(ContentId choiceId)
    {
        if (string.IsNullOrWhiteSpace(choiceId.PackId) ||
            string.IsNullOrWhiteSpace(choiceId.LocalId))
        {
            throw new ArgumentException(
                "Choice ID must be valid.",
                nameof(choiceId));
        }

        if (_choiceIds.Contains(choiceId))
        {
            throw new InvalidOperationException(
                "This choice is already assigned to this class level.");
        }

        _choiceIds.Add(choiceId);
    }
}
