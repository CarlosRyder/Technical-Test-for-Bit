using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRB;
    [SerializeField] private float moveSpeed;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 movement;
    bool facingRight = true;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Move()
    {
        playerRB.linearVelocity = movement * moveSpeed;
        float inputX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float inputY = Input.GetAxisRaw("Vertical") * moveSpeed;
        movement = new Vector2(inputX, inputY).normalized;
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        if (movement.x != 0)
            facingRight = movement.x > 0;
        spriteRenderer.flipX = facingRight;
    }

    void FixedUpdate()
    {
        Move();
    }
}
