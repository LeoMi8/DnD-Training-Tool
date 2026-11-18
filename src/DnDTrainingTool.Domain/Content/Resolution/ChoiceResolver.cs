
using DnDTrainingTool.Domain.Characters.Progression;
using DnDTrainingTool.Domain.Content.Identifiers;
using DnDTrainingTool.Domain.Content;
using DnDTrainingTool.Domain.Content.Definitions;

namespace DnDTrainingTool.Domain.Content.Resolution;

public sealed class ChoiceResolver
{
    private readonly ContentRegistry _registry;
    
    public ChoiceResolver(ContentRegistry registry)
    {
        
        _registry = registry
            ?? throw new ArgumentNullException(nameof(registry));
    }

    public IReadOnlyList<ChoiceValidationResult> Validate(
            CharacterProgression progression
        )
    {
        ArgumentNullException.ThrowIfNull(progression);

        var results = new List<ChoiceValidationResult>();
        var classLevels = new Dictionary<
            DnDTrainingTool.Domain.Content.Identifiers.ContentId, int>();
        
        foreach (var record in progression.LevelHistory)
        {
            int classLevel = classLevels.GetValueOrDefault(
                record.ClassId) + 1;
            classLevels[record.ClassId] = classLevel;

            var classDefinition = _registry.Get<ClassDefinition>(record.ClassId);
            var levelDefinition = classDefinition.GetLevelDefinition(classLevel);

            foreach (var choiceId in levelDefinition.ChoiceIds)
            {
                var definition = _registry.Get<ChoiceDefinition>(choiceId);

                var selected = record.Choices.Where(c => c.ChoiceId == choiceId).ToList();

                if (selected.Count < definition.MinimumSelections)
                {
                    results.Add(new ChoiceValidationResult(
                        choiceId,
                        record.CharacterLevel,
                        ChoiceValidationStatus.Missing,
                        "Required selection is missing."
                    ));
                    continue;
                }

                bool invalid = 
                    selected.Count > definition.MaximumSelections ||
                    selected.Any(c => !definition.AvailableOptions.Contains(c.SelectedOptionId));

                    results.Add(new ChoiceValidationResult(
                            choiceId,
                            record.CharacterLevel,
                            invalid
                                ? ChoiceValidationStatus.Invalid
                                : ChoiceValidationStatus.Valid,
                            invalid
                                ? "Selection is not valid."
                                : "Selection is valid."
                        ));
            }
        }

        return results.AsReadOnly();
    }
}