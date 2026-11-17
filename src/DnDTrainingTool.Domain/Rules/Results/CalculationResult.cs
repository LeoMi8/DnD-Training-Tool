
namespace DnDTrainingTool.Domain.Rules.Results;

public sealed class CalculationResult
{
    public int Total { get; }

    public IReadOnlyList<ModifierContribution> Contributions { get; }

    public CalculationResult(
        IEnumerable<ModifierContribution> contributions)
    {
        ArgumentNullException.ThrowIfNull(contributions);

        var items = contributions.ToList();

        if (items.Any(item => item is null))
        {
            throw new ArgumentException(
                "Contributions cannot contain null values.",
                nameof(contributions));
        }

        Contributions = items.AsReadOnly();
        Total = checked(items.Sum(item => item.Value));
    }
}
