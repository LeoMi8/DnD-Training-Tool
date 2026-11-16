using DnDTrainingTool.Domain.Content.Identifiers;
namespace DnDTrainingTool.Domain.Content.Definitions;

public sealed class BackgroundDefinition : ContentDefinition
{
    public string Description {get; }

        public BackgroundDefinition(
                ContentId id,
                string name,
                string description
            ) : base(id, name)
    {
        Description = description
            ?? throw new ArgumentNullException(nameof(description));
    }
}