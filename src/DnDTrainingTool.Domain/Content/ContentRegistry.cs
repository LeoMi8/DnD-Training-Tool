using DnDTrainingTool.Domain.Content.Definitions;
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DndTrainingTool.Domain.Content;

public sealed class ContentRegistry
{
    private readonly Dictionary<ContentId, ContentDefinition> _definitions = new();
    public int Count => _definitions.Count();

    public void Register(ContentDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        if(!_definitions.TryAdd(definition.Id, definition))
        {
            throw new InvalidOperationException(
                $"Content '{definition.Id}' is already registered."
            );
        }
    }

    public ContentDefinition Get(ContentId id)
    {
        if (!_definitions.TryGetValue(id, out var definition))
        {
            throw new KeyNotFoundException(
                $"Content '{id}' was not found."
            );
        }

        return definition;
    }

    public bool TryGet(
            ContentId id,
            out ContentDefinition? definition
        )
    {
        return _definitions.TryGetValue(id, out definition);
    }


    public TDefinition Get<TDefinition>(ContentId id)
        where TDefinition : ContentDefinition
    {
        var definition = Get(id);

        if (definition is not TDefinition typedDefinition)
        {
            throw new InvalidOperationException(
                $"Content '{id}' is not of type {typeof(TDefinition).Name}.");
        }

        return typedDefinition;
    }

}