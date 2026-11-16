using DnDTrainingTool.Domain.Content.Identifiers;
namespace DnDTrainingTool.Domain.Content.Definitions;

public abstract class ContentDefinition
{
    public ContentId Id {get; }
    public string Name {get; }

    protected ContentDefinition(
            ContentId id,
            string name
        )
    {
         if (string.IsNullOrWhiteSpace(id.PackId) ||
            string.IsNullOrWhiteSpace(id.LocalId))
        {
            throw new ArgumentException(
                "Content ID must be valid.",
                nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Content name cannot be empty.",
                nameof(name));

        Id = id;
        Name = name.Trim();
    }


}