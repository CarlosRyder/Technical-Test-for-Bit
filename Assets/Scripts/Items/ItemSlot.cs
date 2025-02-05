using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public GameObject selectedShader;
    public bool isThisItemSelected;
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;
    public Sprite emptySprite;
    private InventoryManager inventoryManager;
    [SerializeField] TMP_Text quantityText;
    [SerializeField] Image itemImage;
    [SerializeField] int maxNumberOfItems;

    private void Awake()
    {
        inventoryManager = GameObject.Find("CanvasUI").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        if (isFull)
        {
            return quantity;
        }
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        this.quantity += quantity;
        itemImage.sprite = itemSprite;

        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
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
        if (isThisItemSelected) 
        {
            bool usable = inventoryManager.UseItem(itemName);
            if (usable) 
            {
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
            }
        }
        else 
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            isThisItemSelected = true;
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        }
    }

    private void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;

        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
    }

    public void OnRightClick()
    {

    }
}
