using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public float speed = 5f;
    private float moveX;
    private float moveY;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnMove(InputValue value)
    {
        Vector2 v2 = value.Get<Vector2>();
        moveX = v2.x; 
        moveY = v2.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX*speed, moveY*speed);
    }
}
