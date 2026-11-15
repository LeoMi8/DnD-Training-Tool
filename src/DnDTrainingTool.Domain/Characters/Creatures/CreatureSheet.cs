using DnDTrainingTool.Domain.Characters.Statistics;
namespace DnDTrainingTool.Domain.Characters.Creatures;

public abstract class CreatureSheet
{
    public Guid Id {get; }
    public string Name {get; private set; }
    public CreatureBaseStatistics Statistics {get; }

    protected CreatureSheet(
            Guid id,
            string name,
            CreatureBaseStatistics statistics
        )
    {
        if (id == Guid.Empty)
            throw new ArgumentException(
                "Creature ID cannot be empty.",
                nameof(id)
            );

        Id = id;
        Name = ValidateName(name);
        Statistics = statistics
            ?? throw new ArgumentNullException(nameof(statistics));
    }

    public void SetName(string name)
    {
        Name = ValidateName(name);
    }
    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Creature name cannot be empty."
            );
        
        return name.Trim();
    }
}