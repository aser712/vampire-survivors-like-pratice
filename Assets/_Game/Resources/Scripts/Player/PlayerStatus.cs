using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int maxhp = 100;
    public int hp = 100;
    public int attack = 5;
    public int defense = 5;
    public float speed = 5.0f;
    public int level = 1;
    public int exp = 0;

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            hp = 0;
            speed = 0;
            Debug.Log("Player Dead");
        }
    }

    public void Heal(int amount)
    {
        hp += amount;
    }

    public void GetExp(int expToGet)
    {
        exp += expToGet;
        CheckLevelUP();
    }
    private void CheckLevelUP()
    {
        int expToLevelUP = level * level + 50;

        if(exp > expToLevelUP)
        {
            level++;
            exp -= expToLevelUP;

            CheckLevelUP();
        }
    }
}
