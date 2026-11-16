
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


}