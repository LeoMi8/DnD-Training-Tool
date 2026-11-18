
using DnDTrainingTool.Domain.Content;
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

        ResolveSpeciesGrants(build, results);
        ResolveBackgroundGrants(build, results);
        ResolveClassGrants(build, results);
        ResolveSubclassGrants(build, results);

        return results.AsReadOnly();
    }

    private void ResolveSpeciesGrants(
        CharacterBuild build,
        List<ResolvedGrant> results)
    {
        var species = _registry.Get<SpeciesDefinition>(
            build.Species.SpeciesId);

        AddGrants(results, species.Id, null, species.Grants);

        foreach (var selection in build.Species.Options)
        {
            var choice = _registry.Get<ChoiceDefinition>(
                selection.OptionId);

            if (!species.ChoiceIds.Contains(choice.Id))
            {
                throw new InvalidOperationException(
                    $"Choice '{choice.Id}' does not belong to species '{species.Id}'.");
            }

            if (!choice.AvailableOptions.Contains(selection.SelectedValueId))
            {
                throw new InvalidOperationException(
                    $"Option '{selection.SelectedValueId}' is not allowed.");
            }

            var selectedOption =
                _registry.Get<SpeciesOptionDefinition>(
                    selection.SelectedValueId);

            AddGrants(
                results,
                selectedOption.Id,
                null,
                selectedOption.Grants);
        }
    }

    private void ResolveBackgroundGrants(
        CharacterBuild build,
        List<ResolvedGrant> results)
    {
        var background = _registry.Get<BackgroundDefinition>(
            build.Background.BackgroundId);

        AddGrants(
            results,
            background.Id,
            null,
            background.Grants);
    }

    private void ResolveClassGrants(
        CharacterBuild build,
        List<ResolvedGrant> results)
    {
        var classGroups = build.Progression.LevelHistory
            .GroupBy(record => record.ClassId);

        foreach (var group in classGroups)
        {
            var classDefinition =
                _registry.Get<ClassDefinition>(group.Key);

            for (int level = 1; level <= group.Count(); level++)
            {
                var levelDefinition =
                    classDefinition.GetLevelDefinition(level);

                AddGrants(
                    results,
                    classDefinition.Id,
                    level,
                    levelDefinition.Grants);
            }
        }
    }

    private void ResolveSubclassGrants(
        CharacterBuild build,
        List<ResolvedGrant> results)
    {
        var classLevelCounts = new Dictionary<ContentId, int>();
        var selectedSubclasses =
            new Dictionary<ContentId, SubclassDefinition>();

        foreach (var record in build.Progression.LevelHistory)
        {
            int classLevel =
                classLevelCounts.GetValueOrDefault(record.ClassId) + 1;

            classLevelCounts[record.ClassId] = classLevel;

            var classDefinition =
                _registry.Get<ClassDefinition>(record.ClassId);

            var levelDefinition =
                classDefinition.GetLevelDefinition(classLevel);

            foreach (var selection in record.Choices)
            {
                if (!levelDefinition.ChoiceIds.Contains(selection.ChoiceId))
                    continue;

                var choiceDefinition =
                    _registry.Get<ChoiceDefinition>(selection.ChoiceId);

                if (!choiceDefinition.AvailableOptions.Contains(
                    selection.SelectedOptionId))
                    continue;

                if (!_registry.TryGet(
                    selection.SelectedOptionId,
                    out var definition))
                    continue;

                if (definition is not SubclassDefinition subclass)
                    continue;

                if (subclass.ParentClassId != record.ClassId)
                {
                    throw new InvalidOperationException(
                        $"Subclass '{subclass.Id}' does not belong " +
                        $"to class '{record.ClassId}'.");
                }

                if (!selectedSubclasses.TryAdd(record.ClassId, subclass))
                {
                    throw new InvalidOperationException(
                        $"A subclass has already been selected for '{record.ClassId}'.");
                }
            }
        }

        foreach (var (classId, subclass) in selectedSubclasses)
        {
            int acquiredLevels = classLevelCounts[classId];

            for (int level = 1; level <= acquiredLevels; level++)
            {
                AddGrants(
                    results,
                    subclass.Id,
                    level,
                    subclass.GetLevelGrants(level));
            }
        }
    }

    private static void AddGrants(
        List<ResolvedGrant> results,
        ContentId sourceId,
        int? sourceLevel,
        IReadOnlyList<GrantDefinition> grants)
    {
        foreach (var grant in grants)
        {
            results.Add(new ResolvedGrant(
                sourceId,
                sourceLevel,
                grant));
        }
    }
}
