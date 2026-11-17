using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Resolution;

public enum ChoiceValidationStatus
{
    Valid,
    Missing,
    Invalid
}

public sealed record ChoiceValidationResult(
    ContentId ChoiceId,
    int? CharacterLevel,
    ChoiceValidationStatus Status,
    string Message
);