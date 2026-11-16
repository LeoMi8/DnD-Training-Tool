using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class SpeciesSelection
{
    private readonly List<SpeciesOptionSelection> _options = new();
    public ContentId SpeciesId{get; }
    public IReadOnlyList<SpeciesOptionSelection> Options =>
        _options.AsReadOnly();

    public SpeciesSelection(ContentId speciesId)
    {
        if (string.IsNullOrWhiteSpace(speciesId.PackId) ||
            string.IsNullOrWhiteSpace(speciesId.LocalId))
            throw new ArgumentException(
                "Species ID cannot be empty.",
                nameof(speciesId)
            );

        SpeciesId = speciesId;
    }

    public void AddOption(SpeciesOptionSelection option)
    {
        ArgumentNullException.ThrowIfNull(option);

        if (_options.Any(o => o.OptionId == option.OptionId))
            throw new InvalidOperationException( 
                "An option with this ID has already been selected"
            );

        _options.Add(option);
    }
}