
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class SpeciesOptionSelection
{
    public ContentId OptionId { get; }
    public ContentId SelectedValueId { get; }

    public SpeciesOptionSelection(
        ContentId optionId,
        ContentId selectedValueId)
    {
        ValidateId(optionId, nameof(optionId));
        ValidateId(selectedValueId, nameof(selectedValueId));

        OptionId = optionId;
        SelectedValueId = selectedValueId;
    }

    private static void ValidateId(ContentId id, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(id.PackId) ||
            string.IsNullOrWhiteSpace(id.LocalId))
        {
            throw new ArgumentException(
                "Content ID must be valid.",
                parameterName);
        }
    }
}
