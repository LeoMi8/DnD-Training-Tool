namespace DnDTrainingTool.Domain.Characters.Progression;

public sealed class CharacterBuild
{
    public SpeciesSelection Species {get; private set; }
    public BackgroundSelection Background {get; private set; }
    public CharacterProgression Progression {get; }

    public CharacterBuild(
            SpeciesSelection species,
            BackgroundSelection background,
            CharacterProgression progression
        )
    {
        Species = species
            ?? throw new ArgumentNullException(nameof(species));

        Background = background
            ?? throw new ArgumentNullException(nameof(background));
        Progression = progression
            ?? throw new ArgumentNullException(nameof(progression));
    }

    public void ChangeSpecies(SpeciesSelection species)
    {
        Species = species
            ?? throw new ArgumentNullException(nameof(species));
    }

    public void ChangeBackground(BackgroundSelection background)
    {
        Background = background
            ?? throw new ArgumentNullException(nameof(background));
    }
    
}