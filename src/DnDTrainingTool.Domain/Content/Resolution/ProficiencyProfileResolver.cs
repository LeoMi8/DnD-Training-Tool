using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Content.Definitions;

namespace DnDTrainingTool.Domain.Content.Resolution;

public sealed class ProficiencyProfileResolver
{
    public ProficiencyProfile Resolve(
        ProficiencyProfile baseProfile,
        IEnumerable<ResolvedGrant> grants)
    {
        ArgumentNullException.ThrowIfNull(baseProfile);
        ArgumentNullException.ThrowIfNull(grants);

        var profile = baseProfile.Clone();

        foreach (var resolvedGrant in grants)
        {
            if (resolvedGrant.Grant is EquipmentProficiencyGrantDefinition grant)
                profile.AddEquipmentCategory(grant.Category);
        }

        return profile;
    }
}