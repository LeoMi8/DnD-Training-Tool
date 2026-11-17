
using DnDTrainingTool.Domain.Characters.Equipment;
using DnDTrainingTool.Domain.Content.Definitions;
using DnDTrainingTool.Domain.Content.Identifiers;
using Xunit;
using DndTrainingTool.Domain.Content;

namespace DnDTrainingTool.Tests.Content;

public class EquipmentDefinitionTests
{
    [Fact]
    public void ArmorDefinition_StoresArmorProperties()
    {
        var armor = new ArmorDefinition(
            new ContentId("test", "chain-mail"),
            "Chain Mail",
            ArmorCategory.Heavy,
            16);

        Assert.Equal(ArmorCategory.Heavy, armor.Category);
        Assert.Equal(16, armor.BaseArmorClass);
    }

    [Fact]
    public void ShieldDefinition_StoresArmorClassBonus()
    {
        var shield = new ShieldDefinition(
            new ContentId("test", "shield"),
            "Shield",
            2);

        Assert.Equal(2, shield.ArmorClassBonus);
    }

    [Fact]
    public void ContentRegistry_ResolvesEquipmentDefinitions()
    {
        var registry = new ContentRegistry();

        var armor = new ArmorDefinition(
            new ContentId("test", "leather"),
            "Leather Armor",
            ArmorCategory.Light,
            11);

        registry.Register(armor);

        var result = registry.Get<ArmorDefinition>(armor.Id);

        Assert.Same(armor, result);
    }
}
