using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public GameObject selectedShader;
    public bool isThisItemSelected;
    private InventoryManager inventoryManager;
    [SerializeField] TMP_Text quantityText;
    [SerializeField] Image itemImage;

    private void Awake()
    {
        inventoryManager = GameObject.Find("CanvasUI").GetComponent<InventoryManager>();
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite) 
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) 
        {
            OnLeftClick();
        }  
        if (eventData.button == PointerEventData.InputButton.Right) 
        {
            OnRightClick();
        }
    }

    public void OnLeftClick() 
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        isThisItemSelected = true;
    }

    public void OnRightClick() 
    {
        
    }
}
