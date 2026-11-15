namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class CharacterChoice
{
    public string ChoiceId {get; }
    public string SelectedOptionId {get; }

    public CharacterChoice(
            string choiceId,
            string selectedOptionId
        )
    {
        if (string.IsNullOrWhiteSpace(choiceId))
            throw new ArgumentException(
                "Choice ID cannot be empty.",
                nameof(choiceId));

        if (string.IsNullOrWhiteSpace(selectedOptionId))
            throw new ArgumentException(
                "Selected option ID cannot be empty.",
                nameof(selectedOptionId));
        
        ChoiceId = choiceId.Trim();
        SelectedOptionId = selectedOptionId.Trim();
    }
}