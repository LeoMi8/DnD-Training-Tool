
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

        // 1. Species grants
        var species = _registry.Get<SpeciesDefinition>(
            build.Species.SpeciesId);

        AddGrants(results, species.Id, null, species.Grants);

        // 2. Selected species options
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

        // 3. Background grants
        var background = _registry.Get<BackgroundDefinition>(
            build.Background.BackgroundId);

        AddGrants(
            results,
            background.Id,
            null,
            background.Grants);

        // 4. Class grants
        var classLevels = build.Progression.LevelHistory
            .GroupBy(record => record.ClassId);

        foreach (var group in classLevels)
        {
            var classDefinition =
                _registry.Get<ClassDefinition>(group.Key);

            int acquiredLevels = group.Count();

            for (int level = 1; level <= acquiredLevels; level++)
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

        // 5. Find selected subclasses
        var classLevelCounts = new Dictionary<ContentId, int>();
        var selectedSubclasses =
            new Dictionary<ContentId, SubclassDefinition>();

        foreach (var record in build.Progression.LevelHistory)
        {
            int classLevel =
                classLevelCounts.GetValueOrDefault(record.ClassId) + 1;

            classLevelCounts[record.ClassId] = classLevel;

            var currentClass =
                _registry.Get<ClassDefinition>(record.ClassId);

            var currentLevel =
                currentClass.GetLevelDefinition(classLevel);

            foreach (var selection in record.Choices)
            {
                if (!currentLevel.ChoiceIds.Contains(selection.ChoiceId))
                    continue;

                var choiceDefinition =
                    _registry.Get<ChoiceDefinition>(selection.ChoiceId);

                if (!choiceDefinition.AvailableOptions.Contains(
                    selection.SelectedOptionId))
                    continue;

                if (!_registry.TryGet(
                    selection.SelectedOptionId,
                    out var selectedDefinition))
                    continue;

                if (selectedDefinition is not SubclassDefinition subclass)
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
                        $"A subclass has already been selected for class '{record.ClassId}'.");
                }
            }
        }

        // 6. Resolve subclass grants
        foreach (var (classId, subclass) in selectedSubclasses)
        {
            int classLevel = classLevelCounts[classId];

            for (int level = 1; level <= classLevel; level++)
            {
                AddGrants(
                    results,
                    subclass.Id,
                    level,
                    subclass.GetLevelGrants(level));
            }
        }

        return results.AsReadOnly();
    }

    private static void AddGrants(
        List<ResolvedGrant> results,
        ContentId sourceId,
        int? sourceLevel,
        IReadOnlyList<GrantDefinition> grants)
    {
        foreach (var grant in grants)
        {
            results.Add(
                new ResolvedGrant(
                    sourceId,
                    sourceLevel,
                    grant));
        }
    }
}
