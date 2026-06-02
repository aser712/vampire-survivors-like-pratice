using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpinBlade : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SkillData skillData;
    public Transform player;
    Rigidbody2D rb;

    public float skillRange = 10;
    public float angle = 0;
    public float speed = 5;

    private Vector2 currentPos;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;


        angle += speed * Time.deltaTime;
        transform.position = player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * skillRange;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg*angle + 90);
    }
}
