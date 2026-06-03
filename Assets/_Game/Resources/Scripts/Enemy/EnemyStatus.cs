using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public EnemyData EnemyData;
    [SerializeField] private int HP;

    private void Awake()
    {
        Init();
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            HP = 0;
            Destroy(gameObject);
        }
    }
    public void Init()
    {
        HP = EnemyData.maxHP;
    }
}
