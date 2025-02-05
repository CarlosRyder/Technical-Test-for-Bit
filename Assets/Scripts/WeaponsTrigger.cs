using UnityEngine;

public class WeaponsTrigger : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController newAnimatorController; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animator playerAnimator = collision.GetComponent<Animator>();

            if (playerAnimator != null)
            {
                playerAnimator.runtimeAnimatorController = newAnimatorController;

                Debug.Log("Animator Controller has changed to the new one.");
            }

            else
            {
                Debug.LogWarning("Player object has no animator.");
            }

            gameObject.SetActive(false);
        }
    }
}
