using System.Net;

namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class SpeciesSelection
{
    private readonly List<SpeciesOptionSelection> _options = new();
    public string SpeciesId{get; }
    public IReadOnlyList<SpeciesOptionSelection> Options =>
        _options.AsReadOnly();

    public SpeciesSelection(string speciesId)
    {
        if (string.IsNullOrWhiteSpace(speciesId))
            throw new ArgumentException(
                "Species ID cannot be empty.",
                nameof(speciesId)
            );

        SpeciesId = speciesId.Trim();
    }

    public void AddOption(SpeciesOptionSelection option)
    {
        ArgumentNullException.ThrowIfNull(option);

        if (_options.Any(o => o.OptionId == option.OptionId))
            throw new InvalidOperationException( 
                "An option with this ID has already been selected"
            );
    }
}