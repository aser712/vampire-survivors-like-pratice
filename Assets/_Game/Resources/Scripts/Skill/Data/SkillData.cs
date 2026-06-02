using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("스킬 이름")]
    public string skillName;

    [Header("스킬 레벨")]
    public int skillLevel;

    [Header("스킬 데미지")]
    public int skillDamage;

    [Header("스킬 쿨타임")]
    public float cooldown;
}
