namespace DnDTrainingTool.Domain.Content.Identifiers;

public readonly record struct ContentId
{
    public string PackId {get; }
    public string LocalId{get; } 


    public ContentId(
            string packId,
            string localId
        )
    {
        if (string.IsNullOrWhiteSpace(packId))
            throw new ArgumentException(
                "Pack ID cannot be empty.",
                nameof(packId)
            );

        if (string.IsNullOrWhiteSpace(localId))
            throw new ArgumentException(
                "Local ID cannot be empty.",
                nameof(localId)
            );
        
        PackId = packId.Trim();
        LocalId = localId.Trim();
    }

    public override string ToString()
    {
        return $"{PackId}:{LocalId}";
    }
}