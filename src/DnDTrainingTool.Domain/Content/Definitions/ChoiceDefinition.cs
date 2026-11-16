using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class ChoiceDefinition : ContentDefinition
{
    private readonly List<ContentId> _availableOptions = new();
    public IReadOnlyList<ContentId> AvailableOptions =>
        _availableOptions.AsReadOnly();
    
    public int MinimumSelections {get; }
    public int MaximumSelections {get; }

    public ChoiceDefinition(
        ContentId id,
        string name,
        int minimumSelections = 1,
        int maximumSelections = 1
        ) : base (id, name)
    {
        if (minimumSelections < 0)
            throw new ArgumentOutOfRangeException(
                nameof(minimumSelections));

        if (maximumSelections < 1 ||
            maximumSelections < minimumSelections)
            throw new ArgumentOutOfRangeException(
                nameof(maximumSelections));

        MinimumSelections = minimumSelections;
        MaximumSelections = maximumSelections;
    } 

    public void AddOption(ContentId optionId)
    {
        if (string.IsNullOrWhiteSpace(optionId.PackId) ||
            string.IsNullOrWhiteSpace(optionId.LocalId))
            throw new ArgumentException(
                "Option ID must be valid.",
                nameof(optionId));

        if (_availableOptions.Contains(optionId))
            throw new InvalidOperationException(
                "This option is already available.");

        _availableOptions.Add(optionId);
    }

}