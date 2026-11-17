
using DnDTrainingTool.Domain.Content.Identifiers;

namespace DnDTrainingTool.Domain.Characters.Equipment;

public sealed class EquipmentLoadout
{
    public ContentId? EquippedArmorId { get; private set; }
    public ContentId? EquippedShieldId { get; private set; }

    public bool IsWearingArmor => EquippedArmorId.HasValue;
    public bool IsUsingShield => EquippedShieldId.HasValue;

    public void EquipArmor(ContentId armorId)
    {
        ValidateId(armorId);
        EquippedArmorId = armorId;
    }

    public void UnequipArmor()
    {
        EquippedArmorId = null;
    }

    public void EquipShield(ContentId shieldId)
    {
        ValidateId(shieldId);
        EquippedShieldId = shieldId;
    }

    public void UnequipShield()
    {
        EquippedShieldId = null;
    }

    public EquipmentLoadout Clone()
    {
        return new EquipmentLoadout
        {
            EquippedArmorId = EquippedArmorId,
            EquippedShieldId = EquippedShieldId
        };
    }

    private static void ValidateId(ContentId id)
    {
        if (string.IsNullOrWhiteSpace(id.PackId) ||
            string.IsNullOrWhiteSpace(id.LocalId))
        {
            throw new ArgumentException(
                "Equipment must have a valid ContentId.");
        }
    }
}
