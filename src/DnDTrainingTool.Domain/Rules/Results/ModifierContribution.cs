
namespace DnDTrainingTool.Domain.Rules.Results;

public sealed record ModifierContribution
{
    public string Source { get; }
    public int Value { get; }

    public ModifierContribution(string source, int value)
    {
        if (string.IsNullOrWhiteSpace(source))
            throw new ArgumentException(
                "Source cannot be empty.",
                nameof(source));

        Source = source;
        Value = value;
    }
}
