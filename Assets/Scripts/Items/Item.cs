using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] int quantity;
    [SerializeField] Sprite sprite;

    [TextArea]
    [SerializeField] string itemDescription;

    InventoryManager inventoryManager;

    void Awake()
    {
        inventoryManager = GameObject.Find("CanvasUI").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems <= 0) 
            {
                gameObject.SetActive(false);
            }
            else 
            {
                quantity = leftOverItems;
            }
        }
    }
}
