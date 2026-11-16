namespace DnDTrainingTool.Domain.Characters.Creatures;

public sealed class StatBlockReference
{
    public string StatBlockId {get;}
    public StatBlockReference(string statBlockId)
    {
        if (string.IsNullOrWhiteSpace(statBlockId))
            throw new ArgumentException(
                "Stat block ID cannot be empty.",
                nameof(statBlockId));

        StatBlockId = statBlockId.Trim();
    }
}