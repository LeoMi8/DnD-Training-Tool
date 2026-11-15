namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class SpeciesSelection
{
    public string SpeciesId{get; }

    public SpeciesSelection(string speciesId)
    {
        if (string.IsNullOrWhiteSpace(speciesId))
            throw new ArgumentException(
                "Species ID cannot be empty.",
                nameof(speciesId)
            );

        SpeciesId = speciesId.Trim();
    }
}