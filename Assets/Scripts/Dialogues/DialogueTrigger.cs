using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject RobotDialoguesUI;

    private void Start()
    {
        if (RobotDialoguesUI != null)
        {
            RobotDialoguesUI.SetActive(false);
        }
        else
        {
            Debug.LogError("UI Panel is not assigned in the Inspector");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (RobotDialoguesUI != null)
            {
                RobotDialoguesUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (RobotDialoguesUI != null)
            {
                RobotDialoguesUI.SetActive(false);
            }
        }
    }
}