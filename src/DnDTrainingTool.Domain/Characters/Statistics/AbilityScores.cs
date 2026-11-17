namespace DnDTrainingTool.Domain.Characters.Statistics;

public sealed class AbilityScores
{
    public int Strength {get; }
    public int Dexterity {get; }
    public int Constitution {get; }
    public int Intelligence {get; }
    public int Wisdom {get; }
    public int Charisma {get; }
    
    public  AbilityScores(
        int strength,
        int dexterity,
        int constitution,
        int intelligence,
        int wisdom,
        int charisma
    )
    {
        Strength        = Validate(strength);
        Dexterity       = Validate(dexterity);
        Constitution    = Validate(constitution);
        Intelligence    = Validate(intelligence);
        Wisdom          = Validate(wisdom);
        Charisma        = Validate(charisma);
    }
    
    private static int Validate (int value)
    {
        if (value < 0 || value > 30)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                "Ability scores must be between 0 and 30"
            );
        }

        return value;
    }

    public int GetScore(Ability ability)
    {
        return ability switch
        {
            Ability.Strength => Strength,
            Ability.Dexterity => Dexterity,
            Ability.Constitution => Constitution,
            Ability.Intelligence => Intelligence,
            Ability.Wisdom => Wisdom,
            Ability.Charisma => Charisma,
            _ => throw new ArgumentOutOfRangeException(nameof(ability))
        };
    }

}
