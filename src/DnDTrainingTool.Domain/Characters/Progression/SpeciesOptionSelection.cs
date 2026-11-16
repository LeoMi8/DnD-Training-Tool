namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class SpeciesOptionSelection
{
    public string OptionId {get; }
    public string SelectedValueId {get; }

    public SpeciesOptionSelection(
            string optionId,
            string selectedValueId
        )
    {
        if (string.IsNullOrWhiteSpace(optionId))
            throw new ArgumentException(
                "Option ID cannot be empty.",
                nameof(optionId));

        if (string.IsNullOrWhiteSpace(selectedValueId))
            throw new ArgumentException(
                "Selected value ID cannot be empty.",
                nameof(selectedValueId));

        OptionId = optionId.Trim();
        SelectedValueId = selectedValueId.Trim();
    }   
}