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
    [SerializeField] SpriteRenderer gunSpriteRenderer, bowSpriteRenderer;
    [SerializeField] Transform gunTransform, bowTransform;
    [SerializeField] GameObject upGun, downGun, sideGun;
    [SerializeField] GameObject upBow, downBow, sideBow;

    [SerializeField] RuntimeAnimatorController WalkWithoutGun;
    [SerializeField] RuntimeAnimatorController WalkWithGun;
    [SerializeField] RuntimeAnimatorController WalkWithBow;

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

        // Update the facing direction based on horizontal movement
        if (movement.x != 0)
        {
            facingRight = movement.x > 0;
        }

        // Determine weapon visibility based on the animator's runtimeAnimatorController
        switch (animator.runtimeAnimatorController)
        {
            case RuntimeAnimatorController controller when controller == WalkWithGun:
                HandleGunVisibility();
                DisableBowParts(); // Disable all bow parts when using a gun
                break;

            case RuntimeAnimatorController controller when controller == WalkWithBow:
                HandleBowVisibility();
                DisableGunParts(); // Disable all gun parts when using a bow
                break;

            default: // WalkWithoutGun
                DisableGunParts();
                DisableBowParts();
                break;
        }

        // Update the player's sprite direction (flipX) based on the facing direction
        spriteRenderer.flipX = facingRight;
        gunSpriteRenderer.flipX = facingRight;
        bowSpriteRenderer.flipX = facingRight;

        // Adjust the gun's position based on the player's facing direction
        Vector3 gunPosition = gunTransform.localPosition;
        gunPosition.x = facingRight ? 0.5f : -0.5f; // Move gun to the correct side
        gunTransform.localPosition = gunPosition; 
        
        // Adjust the bows's position based on the player's facing direction
        Vector3 bowPosition = bowTransform.localPosition;
        bowPosition.x = facingRight ? 0.5f : -0.5f; // Move gun to the correct side
        bowTransform.localPosition = bowPosition;
    }

    void HandleGunVisibility()
    {
        // Update visibility of gun parts based on movement direction
        if (movement.y > 0) // Moving upwards
        {
            upGun.SetActive(true);
            downGun.SetActive(false);
            sideGun.SetActive(false);
        }
        else if (movement.y < 0) // Moving downwards
        {
            downGun.SetActive(true);
            upGun.SetActive(false);
            sideGun.SetActive(false);
        }
        else if (movement.x != 0) // Moving sideways
        {
            sideGun.SetActive(true);
            upGun.SetActive(false);
            downGun.SetActive(false);
            facingRight = movement.x > 0;
        }
        else // No movement
        {
            upGun.SetActive(false);
            downGun.SetActive(false);
            sideGun.SetActive(false);
        }
    }

    void HandleBowVisibility()
    {
        // Update visibility of bow parts based on movement direction
        if (movement.y > 0) // Moving upwards
        {
            upBow.SetActive(true);
            downBow.SetActive(false);
            sideBow.SetActive(false);
        }
        else if (movement.y < 0) // Moving downwards
        {
            downBow.SetActive(true);
            upBow.SetActive(false);
            sideBow.SetActive(false);
        }
        else if (movement.x != 0) // Moving sideways
        {
            sideBow.SetActive(true);
            upBow.SetActive(false);
            downBow.SetActive(false);
            facingRight = movement.x > 0;
        }
        else // No movement
        {
            upBow.SetActive(false);
            downBow.SetActive(false);
            sideBow.SetActive(false);
        }
    }

    void DisableGunParts()
    {
        upGun.SetActive(false);
        downGun.SetActive(false);
        sideGun.SetActive(false);
    }

    void DisableBowParts()
    {
        upBow.SetActive(false);
        downBow.SetActive(false);
        sideBow.SetActive(false);
    }
}
