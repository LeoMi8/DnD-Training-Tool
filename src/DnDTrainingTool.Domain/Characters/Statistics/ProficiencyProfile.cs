using System.Runtime.CompilerServices;
using DnDTraining.Domain.Characters.Statistics;

namespace DnDTrainingTool.Domain.Characters.Statistics;

public sealed class ProficiencyProfile
{
    private readonly Dictionary<Skill, ProficiencyLevel> _skills = new();
    private readonly HashSet<Ability> _savingThrows = new();
    
    private readonly HashSet<EquipmentProficiencyType> _equipmentCategories = new();
    private readonly HashSet<string> _specificEquipment = new();
    private readonly HashSet<string> _tools = new();



    public ProficiencyLevel GetSkillProficiency(Skill skill)
    {
        if (!Enum.IsDefined(skill))
            throw new ArgumentOutOfRangeException(nameof(skill));

        return _skills.GetValueOrDefault(skill);
    }

    public void SetSkillProficiency(
            Skill skill,
            ProficiencyLevel level
        )
    {
        if (!Enum.IsDefined(skill))
            throw new ArgumentOutOfRangeException(nameof(skill));
        if (!Enum.IsDefined(level))
            throw new ArgumentOutOfRangeException(nameof(level));

        if (level == ProficiencyLevel.None)
            _skills.Remove(skill);
        else
            _skills[skill] = level;
    }


    public bool HasSavingThrowProficiency(Ability ability)
    {
        if (!Enum.IsDefined(ability))
            throw new ArgumentOutOfRangeException(nameof(ability));

            return _savingThrows.Contains(ability);
    }

    public void SetSavingThrowProficiency(
            Ability ability,
            bool proficient
        )
    {
        if (!Enum.IsDefined(ability))
            throw new ArgumentOutOfRangeException(nameof(ability));

        if(proficient)
            _savingThrows.Add(ability);
        else
            _savingThrows.Remove(ability);
    }


    
    public void AddEquipmentCategory(EquipmentProficiencyType type)
    {
        if (!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(nameof(type));

        _equipmentCategories.Add(type);
    }

    public bool HasEquipmentCategory(EquipmentProficiencyType type)
    {
        if (!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(nameof(type));

            return _equipmentCategories.Contains(type);
    }

    public void AddSpecificEquipment(string equipmentId)
    {
        if (string.IsNullOrWhiteSpace(equipmentId))
            throw new ArgumentException(
                "Equipment ID cannot be empty.",
                nameof(equipmentId)
            );
        _specificEquipment.Add(equipmentId);
    }
    public bool HasSpecificEquipment(string equipmentId)
    {
        return _specificEquipment.Contains(equipmentId);
    }

    public void AddTool(string toolId)
    {
        if (string.IsNullOrWhiteSpace(toolId))
        throw new ArgumentException(
            "Tool ID cannot be empty.",
            nameof(toolId));

        _tools.Add(toolId);
    }

    public bool HasTool(string toolId)
    {
    return _tools.Contains(toolId);
    }

    public ProficiencyProfile Clone()
    {
        var copy = new ProficiencyProfile();

        foreach (var (skill, level) in _skills)
        {
            copy.SetSkillProficiency(skill, level);
        }

        foreach (var ability in _savingThrows)
        {
            copy.SetSavingThrowProficiency(ability, true);
        }

        foreach (var category in _equipmentCategories)
        {
            copy.AddEquipmentCategory(category);
        }

        foreach (var equipmentId in _specificEquipment)
        {
            copy.AddSpecificEquipment(equipmentId);
        }

        foreach (var toolId in _tools)
        {
            copy.AddTool(toolId);
        }

        return copy;
    }


}