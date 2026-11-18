using DnDTraining.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Characters.Statistics;
using DnDTrainingTool.Domain.Content.Definitions;
using DnDTrainingTool.Domain.Content.Identifiers;
using DnDTrainingTool.Domain.Content.Resolution;
using Xunit;

namespace DnDTrainingTool.Tests.Content;

public sealed class ProficiencyProfileResolverTests
{
    [Fact]
    public void Resolve_AddsGrantedCategoryToCopyOfBaseProfile()
    {
        var baseProfile = new ProficiencyProfile();
        baseProfile.AddEquipmentCategory(EquipmentProficiencyType.LightArmor);

        var grant = new ResolvedGrant(
            new ContentId("test", "shield-training-class"),
            1,
            new EquipmentProficiencyGrantDefinition(
                EquipmentProficiencyType.Shields));

        var resolver = new ProficiencyProfileResolver();
        var effectiveProfile = resolver.Resolve(baseProfile, new[] { grant });

        Assert.True(effectiveProfile.HasEquipmentCategory(
            EquipmentProficiencyType.LightArmor));

        Assert.True(effectiveProfile.HasEquipmentCategory(
            EquipmentProficiencyType.Shields));

        Assert.False(baseProfile.HasEquipmentCategory(
            EquipmentProficiencyType.Shields));

        var recalculated = resolver.Resolve(
            baseProfile,
            Array.Empty<ResolvedGrant>());

        Assert.False(recalculated.HasEquipmentCategory(
            EquipmentProficiencyType.Shields));
    }
}