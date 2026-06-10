using UnityEngine;

public class EXPOrb : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int expAmount;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite tier5;
    [SerializeField] private Sprite tier4;
    [SerializeField] private Sprite tier3;
    [SerializeField] private Sprite tier2;
    [SerializeField] private Sprite tier1;

    public void Initialize(int exp)
    {
        expAmount = exp;

        if (exp < 50)
            spriteRenderer.sprite = tier5;
        else if (exp < 160)
            spriteRenderer.sprite = tier4;
        else if(exp < 300)
            spriteRenderer.sprite = tier3;
        else if (exp < 500)
            spriteRenderer.sprite = tier2;
        else if (exp < 1000)
            spriteRenderer.sprite = tier1;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().GetExp(expAmount);
            Destroy(gameObject);
        }
    }
}
