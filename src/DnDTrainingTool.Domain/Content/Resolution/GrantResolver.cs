
using System.Runtime.Serialization.Formatters;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using DndTrainingTool.Domain.Content;
using DnDTrainingTool.Domain.Characters.Progression;
using DnDTrainingTool.Domain.Content.Definitions;
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Content.Resolution;

public sealed class GrantResolver
{
    private readonly ContentRegistry _registry;
    
    public GrantResolver(ContentRegistry registry)
    {
        _registry = registry
            ?? throw new ArgumentNullException(nameof(registry));
    } 

    public IReadOnlyList<ResolvedGrant> Resolve(CharacterBuild build)
    {
        ArgumentNullException.ThrowIfNull(build);
        var results = new List<ResolvedGrant>();

        var species = _registry.Get<SpeciesDefinition>(
                build.Species.SpeciesId
            );
        AddGrants(results, species.Id, null, species.Grants);

        var background = _registry.Get<BackgroundDefinition>(
                build.Background.BackgroundId
            );
        AddGrants(results, background.Id, null, background.Grants);

        var classLevels = build.Progression.LevelHistory.GroupBy(record => record.ClassId);
        foreach (var group in classLevels)
        {
            var classDefinition = _registry.Get<ClassDefinition>(group.Key);
            int acquiredLevels = group.Count();
            for (int level = 1; level <= acquiredLevels; level++)
            {
                var levelDefinition = classDefinition.GetLevelDefinition(level);

                AddGrants(
                    results,
                    classDefinition.Id,
                    level,
                    levelDefinition.Grants
                );
            }
        }

        return results.AsReadOnly();
    }

    private static void AddGrants(
            List<ResolvedGrant> results,
            ContentId sourceId,
            int? sourceLevel,
            IReadOnlyList<GrantDefinition> grants
        )
    {
        foreach (var grant in grants)
        {
            results.Add(
                    new ResolvedGrant(sourceId, sourceLevel, grant)
                );
        }
    }


    public IReadOnlyList<ChoiceValidationResult> Validate(
        SpeciesSelection speciesSelection)
    {
        ArgumentNullException.ThrowIfNull(speciesSelection);

        var results = new List<ChoiceValidationResult>();

        var speciesDefinition =
            _registry.Get<SpeciesDefinition>(speciesSelection.SpeciesId);

        foreach (var choiceId in speciesDefinition.ChoiceIds)
        {
            var definition =
                _registry.Get<ChoiceDefinition>(choiceId);

            var selected = speciesSelection.Options
                .Where(option => option.OptionId == choiceId)
                .ToList();

            ChoiceValidationStatus status;
            string message;

            if (selected.Count < definition.MinimumSelections)
            {
                status = ChoiceValidationStatus.Missing;
                message = "Required species selection is missing.";
            }
            else if (
                selected.Count > definition.MaximumSelections ||
                selected.Any(option =>
                    !definition.AvailableOptions.Contains(
                        option.SelectedValueId)))
            {
                status = ChoiceValidationStatus.Invalid;
                message = "Species selection is not valid.";
            }
            else
            {
                status = ChoiceValidationStatus.Valid;
                message = "Species selection is valid.";
            }

            results.Add(new ChoiceValidationResult(
                choiceId,
                null,
                status,
                message));
        }

        return results.AsReadOnly();
    }


}