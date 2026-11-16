
using DndTrainingTool.Domain.Content;
using DnDTrainingTool.Domain.Characters.Progression;
using DnDTrainingTool.Domain.Content;
using DnDTrainingTool.Domain.Content.Definitions;
using DnDTrainingTool.Domain.Content.Identifiers;
using DnDTrainingTool.Domain.Content.Resolution;
using Xunit;

namespace DnDTrainingTool.Tests.Content;

public class GrantResolverTests
{
    [Fact]
    public void Resolve_ShouldCollectGrantsFromAllSources()
    {
        // Arrange
        var registry = new ContentRegistry();

        var speciesId = new ContentId("test", "tiefling");
        var backgroundId = new ContentId("test", "guard");
        var monkId = new ContentId("test", "monk");
        var sorcererId = new ContentId("test", "sorcerer");

        var species = new SpeciesDefinition(
            speciesId, "Tiefling", "Test species");

        species.AddGrant(new FeatureGrantDefinition(
            new ContentId("test", "species-feature")));

        registry.Register(species);

        var background = new BackgroundDefinition(
            backgroundId, "Guard", "Test background");

        background.AddGrant(new FeatureGrantDefinition(
            new ContentId("test", "background-feature")));

        registry.Register(background);

        var monk = new ClassDefinition(
            monkId, "Monk", "Test class", 8);

        for (int level = 1; level <= 3; level++)
        {
            var definition = new ClassLevelDefinition(level);

            definition.AddGrant(new FeatureGrantDefinition(
                new ContentId("test", $"monk-feature-{level}")));

            monk.AddLevelDefinition(definition);
        }

        registry.Register(monk);

        var sorcerer = new ClassDefinition(
            sorcererId, "Sorcerer", "Test class", 6);

        for (int level = 1; level <= 2; level++)
        {
            var definition = new ClassLevelDefinition(level);

            definition.AddGrant(new FeatureGrantDefinition(
                new ContentId("test", $"sorcerer-feature-{level}")));

            sorcerer.AddLevelDefinition(definition);
        }

        registry.Register(sorcerer);

        var progression = new CharacterProgression(monkId);
        progression.AddLevel(monkId);
        progression.AddLevel(sorcererId);
        progression.AddLevel(monkId);
        progression.AddLevel(sorcererId);

        var build = new CharacterBuild(
            new SpeciesSelection(speciesId),
            new BackgroundSelection(backgroundId),
            progression);

        // Act
        var resolver = new GrantResolver(registry);
        var grants = resolver.Resolve(build);

        // Assert
        Assert.Equal(7, grants.Count);

        Assert.Single(grants, g =>
            g.SourceId == speciesId);

        Assert.Single(grants, g =>
            g.SourceId == backgroundId);

        Assert.Equal(3, grants.Count(g =>
            g.SourceId == monkId));

        Assert.Equal(2, grants.Count(g =>
            g.SourceId == sorcererId));

        Assert.Contains(grants, g =>
            g.SourceId == monkId && g.SourceLevel == 3);

        Assert.Contains(grants, g =>
            g.SourceId == sorcererId && g.SourceLevel == 2);
    }
}
