using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject InventoryBackGround;
    bool menuActivated;
    public ItemSlot[] itemSlot;

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

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].name == name || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                }
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].isThisItemSelected = false;
        }
    }
}
