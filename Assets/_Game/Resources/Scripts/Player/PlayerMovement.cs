using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    private float moveX;
    private float moveY;
    private Rigidbody2D rb;

    private PlayerStatus status;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
    }

    void OnMove(InputValue value)
    {
        Vector2 v2 = value.Get<Vector2>();
        moveX = v2.x; 
        moveY = v2.y;
    }

    void OnJump(InputValue value)
    {
        status.TakeDamage(10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX*status.speed, moveY*status.speed);
    }
}
