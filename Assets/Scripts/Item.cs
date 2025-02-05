using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] int quantity;
    [SerializeField] Sprite sprite;

    InventoryManager inventoryManager;

    void Awake()
    {
        inventoryManager = GameObject.Find("CanvasUI").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventoryManager.AddItem(itemName, quantity, sprite);
            gameObject.SetActive(false);
        }
    }
}
