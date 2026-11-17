
using DndTrainingTool.Domain.Content;
using DnDTrainingTool.Domain.Characters.Progression;
using DnDTrainingTool.Domain.Content;
using DnDTrainingTool.Domain.Content.Definitions;
using DnDTrainingTool.Domain.Content.Identifiers;
using DnDTrainingTool.Domain.Content.Resolution;
using Xunit;

namespace DnDTrainingTool.Tests.Content;

public class SubclassGrantResolverTests
{
    [Fact]
    public void Resolve_ShouldGrantSubclassFeaturesWithoutDuplicates()
    {
        var registry = new ContentRegistry();

        var speciesId = new ContentId("test", "human");
        var backgroundId = new ContentId("test", "guard");
        var monkId = new ContentId("test", "monk");
        var sorcererId = new ContentId("test", "sorcerer");

        var subclassId = new ContentId("test", "elemental-monk");
        var choiceId = new ContentId("test", "monk-subclass");

        // Species and background
        registry.Register(new SpeciesDefinition(
            speciesId, "Human", "Test species"));

        registry.Register(new BackgroundDefinition(
            backgroundId, "Guard", "Test background"));

        // Monk: levels 1-3
        var monk = new ClassDefinition(
            monkId, "Monk", "Test class", 8);

        for (int level = 1; level <= 3; level++)
        {
            var levelDefinition = new ClassLevelDefinition(level);

            levelDefinition.AddGrant(
                new FeatureGrantDefinition(
                    new ContentId("test", $"monk-feature-{level}")));

            if (level == 3)
                levelDefinition.AddChoice(choiceId);

            monk.AddLevelDefinition(levelDefinition);
        }

        registry.Register(monk);

        // Sorcerer: levels 1-2
        var sorcerer = new ClassDefinition(
            sorcererId, "Sorcerer", "Test class", 6);

        for (int level = 1; level <= 2; level++)
        {
            var levelDefinition = new ClassLevelDefinition(level);

            levelDefinition.AddGrant(
                new FeatureGrantDefinition(
                    new ContentId("test", $"sorcerer-feature-{level}")));

            sorcerer.AddLevelDefinition(levelDefinition);
        }

        registry.Register(sorcerer);

        // Subclass definition
        var subclass = new SubclassDefinition(
            subclassId, "Elemental Monk", "Test subclass", monkId);

        subclass.AddLevelGrant(
            3,
            new FeatureGrantDefinition(
                new ContentId("test", "elemental-feature-3")));

        subclass.AddLevelGrant(
            6,
            new FeatureGrantDefinition(
                new ContentId("test", "elemental-feature-6")));

        registry.Register(subclass);

        // Available subclass selection
        var subclassChoice = new ChoiceDefinition(
            choiceId, "Monk Subclass");

        subclassChoice.AddOption(subclassId);
        registry.Register(subclassChoice);

        // Monk 1 -> Monk 2 -> Sorcerer 1
        // -> Monk 3 -> Sorcerer 2
        var progression = new CharacterProgression(monkId);

        progression.AddLevel(monkId);
        progression.AddLevel(sorcererId);
        progression.AddLevel(monkId);
        progression.AddLevel(sorcererId);

        // The subclass is selected at character level 4,
        // corresponding to Monk level 3.
        progression.LevelHistory[3].AddChoice(
            new CharacterChoice(choiceId, subclassId));

        var build = new CharacterBuild(
            new SpeciesSelection(speciesId),
            new BackgroundSelection(backgroundId),
            progression);

        // Resolve grants
        var resolver = new GrantResolver(registry);
        var grants = resolver.Resolve(build);

        // 3 Monk + 2 Sorcerer + 1 subclass = 6
        Assert.Equal(6, grants.Count);

        Assert.Equal(3, grants.Count(g => g.SourceId == monkId));
        Assert.Equal(2, grants.Count(g => g.SourceId == sorcererId));

        // Subclass level 3 must be granted exactly once
        Assert.Single(grants, g =>
            g.SourceId == subclassId && g.SourceLevel == 3);

        // Level 6 is not yet available
        Assert.DoesNotContain(grants, g =>
            g.SourceId == subclassId && g.SourceLevel == 6);
    }
}
