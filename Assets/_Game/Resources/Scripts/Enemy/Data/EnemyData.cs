using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("기본 정보")]
    public string enemyName;

    [Header("스탯")]
    public int maxHp;
    public int attack;
    public int defense;
    public float speed;

    [Header("보상")]
    public int expReward;
}
