namespace DnDTrainingTool.Domain.Characters.Statistics;

public sealed class DefenseProfile
{
    private readonly HashSet<DamageType> _resistances = new();
    private readonly HashSet<DamageType> _immunities = new();
    private readonly HashSet<DamageType> _vulnerabilities = new();

    public IReadOnlySet<DamageType> Resistances => _resistances;
    public IReadOnlySet<DamageType> Immunities => _immunities;
    public IReadOnlySet<DamageType> Vulnerabilities => _vulnerabilities;

    public void AddResistance(DamageType type)
    {
        _resistances.Add(type);
    }
    public void AddImmunity(DamageType type)
    {
        _immunities.Add(type);
    }
    public void AddVulnerability(DamageType type)
    {
        _vulnerabilities.Add(type);
    }

    public bool HasResistance(DamageType type) => 
        _resistances.Contains(type);
    public bool HasImmunity(DamageType type) =>
        _immunities.Contains(type);
    public bool HasVulnerability(DamageType type) =>
        _vulnerabilities.Contains(type);

}