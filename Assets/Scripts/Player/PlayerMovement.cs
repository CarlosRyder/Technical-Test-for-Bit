using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator animator;
    Vector2 movement;
    bool facingRight = true;
    Sprite originalDownGunSprite; // To store the original sprite of downGun
    SpriteRenderer spriteRenderer;
    [SerializeField] float moveSpeed;
    [SerializeField] SpriteRenderer gunSpriteRenderer;
    [SerializeField] Transform gunTransform;
    [SerializeField] GameObject upGun, downGun, sideGun;
    [SerializeField] RuntimeAnimatorController WalkWithoutGun;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Save the original sprite of downGun
        if (downGun != null)
        {
            var downGunSpriteRenderer = downGun.GetComponent<SpriteRenderer>();
            if (downGunSpriteRenderer != null)
            {
                originalDownGunSprite = downGunSpriteRenderer.sprite;
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        playerRB.linearVelocity = movement * moveSpeed;

        // Get input from horizontal and vertical axes
        float inputX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float inputY = Input.GetAxisRaw("Vertical") * moveSpeed;

        // Normalize the movement vector to prevent faster diagonal movement
        movement = new Vector2(inputX, inputY).normalized;

        // Update animator parameters for movement
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

        switch (movement)
        {
            case Vector2 playerMovement when playerMovement.y > 0: // Moving upwards
                upGun.SetActive(true);
                downGun.SetActive(false);
                sideGun.SetActive(false);
                break;

            case Vector2 playerMovement when playerMovement.y < 0: // Moving downwards
                downGun.SetActive(true);
                upGun.SetActive(false);
                sideGun.SetActive(false);
                break;

            case Vector2 playerMovement when playerMovement.x != 0 && playerMovement.y == 0: // Moving sideways
                sideGun.SetActive(true);
                upGun.SetActive(false);
                downGun.SetActive(false);
                facingRight = playerMovement.x > 0; // Determine the facing direction based on x-axis input
                break;

            default: // No movement
                upGun.SetActive(false);
                downGun.SetActive(false);
                sideGun.SetActive(false);
                break;
        }

        // Check if the animator is using the "WalkWithoutGun" controller
        var downGunSpriteRenderer = downGun.GetComponent<SpriteRenderer>();
        if (animator.runtimeAnimatorController == WalkWithoutGun)
        {
            // Disable the downGun sprite
            if (downGunSpriteRenderer != null)
            {
                downGunSpriteRenderer.sprite = null;
            }
        }
        else
        {
            // Restore the original downGun sprite if it was disabled
            if (downGunSpriteRenderer != null && downGunSpriteRenderer.sprite == null)
            {
                downGunSpriteRenderer.sprite = originalDownGunSprite;
            }
        }

        // Update the player's sprite direction (flipX) based on the facing direction
        spriteRenderer.flipX = facingRight;
        gunSpriteRenderer.flipX = facingRight;

        // Adjust the gun's position based on the player's facing direction
        Vector3 gunPosition = gunTransform.localPosition;
        gunPosition.x = facingRight ? 0.5f : -0.5f; // Move gun to the correct side
        gunTransform.localPosition = gunPosition;
    }
}
