using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject InventoryBackGround;
    bool menuActivated;

    void Update()
    {
        // Detect when the "E" key is pressed once
        if (Input.GetKeyDown(KeyCode.E))
        {
            menuActivated = !menuActivated; // Toggle the inventory state

            if (menuActivated)
            {
                Time.timeScale = 0; // Pause the game
                InventoryBackGround.SetActive(true); 
            }
            else
            {
                Time.timeScale = 1; // Resume the game
                InventoryBackGround.SetActive(false); 
            }
        }
    }
}
