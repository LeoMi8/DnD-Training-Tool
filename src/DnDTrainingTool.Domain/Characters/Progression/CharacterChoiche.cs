
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class CharacterChoice
{
    public ContentId ChoiceId { get; }
    public ContentId SelectedOptionId { get; }

    public CharacterChoice(
        ContentId choiceId,
        ContentId selectedOptionId)
    {
        ValidateId(choiceId, nameof(choiceId));
        ValidateId(selectedOptionId, nameof(selectedOptionId));

        ChoiceId = choiceId;
        SelectedOptionId = selectedOptionId;
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
