using Unity.VisualScripting;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public EnemyData EnemyData;
    [SerializeField] private int HP;
    [SerializeField] private GameObject expPrefab;

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
            GameObject orb = Instantiate(expPrefab, transform.position, Quaternion.identity);

            orb.GetComponent<EXPOrb>().Initialize(EnemyData.expReward);

            Destroy(gameObject);
        }
    }
    public void Init()
    {
        HP = EnemyData.maxHP;
    }
}
