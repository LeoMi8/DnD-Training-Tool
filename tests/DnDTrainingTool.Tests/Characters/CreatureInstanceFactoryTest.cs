using DndTrainingTool.Domain.Characters.State;
using DnDTrainingTool.Domain.Characters.Creatures;
using DnDTrainingTool.Domain.Characters.Progression;
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Content.Identifiers;
using Xunit;

namespace DnDTrainingTool.Test.Characters;

public class CreatureInstanceFactoryTest
{
    [Fact]
    public void Create_ShouldProduceIndependentStatistics()
    {
        // Given
        var statistics = new CreatureBaseStatistics(
            new AbilityScores(10, 14, 12, 10, 10, 10),
            new HitPointProfile(20),
            new MovementProfile(30),
            new SenseProfile(),
            new DefenseProfile(),
            new ProficiencyProfile()
        );

        var build = new CharacterBuild(
            new SpeciesSelection(new ContentId("test-pack", "human")),
            new BackgroundSelection("guard"),
            new CharacterProgression("fighter")
        );

        var character = new PlayerCharacter(
            Guid.NewGuid(),
            "Test Character",
            statistics,
            build
        );

         // Act: create an independent simulation instance.
        var instance = CreatureInstanceFactory.Create(character);

        instance.Snapshot.Statistics.Movement.SetSpeed(
            MovementType.Walking,
            60
        );

        instance.State.SetCurrentHitPoints(5, 20);

         // Assert: the original sheet was not modified.
        Assert.Equal(
            30,
            character.Statistics.Movement.GetSpeed(MovementType.Walking)
        );

        Assert.Equal(
            60,
            instance.Snapshot.Statistics.Movement.GetSpeed(MovementType.Walking)
        );

        Assert.Equal(5, instance.State.CurrentHitPoints);
        Assert.Equal(character.Id, instance.Snapshot.SourceCreatureId);
        Assert.NotEqual(Guid.Empty, instance.InstanceId);
    }

    
}