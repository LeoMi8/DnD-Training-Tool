
using DnDTrainingTool.Domain.Characters.Statistics;

namespace DnDTrainingTool.Domain.Rules.ArmorClass;

public sealed record ArmorClassFormula
{
    public string Name { get; }
    public int BaseValue { get; }
    public bool IncludeDexterity { get; }
    public int? DexterityCap { get; }
    public Ability? AdditionalAbility { get; }
    public bool AllowsShield { get; }

    public ArmorClassFormula(
        string name,
        int baseValue,
        bool includeDexterity,
        int? dexterityCap = null,
        Ability? additionalAbility = null,
        bool allowsShield = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Formula name cannot be empty.", nameof(name));

        if (baseValue < 0)
            throw new ArgumentOutOfRangeException(nameof(baseValue));

        if (dexterityCap.HasValue && !includeDexterity)
            throw new ArgumentException(
                "Dexterity cap requires Dexterity.");

        if (additionalAbility.HasValue &&
            !Enum.IsDefined(additionalAbility.Value))
            throw new ArgumentOutOfRangeException(
                nameof(additionalAbility));

        Name = name;
        BaseValue = baseValue;
        IncludeDexterity = includeDexterity;
        DexterityCap = dexterityCap;
        AdditionalAbility = additionalAbility;
        AllowsShield = allowsShield;
    }
}
