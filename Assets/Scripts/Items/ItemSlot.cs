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
    [SerializeField] private RuntimeAnimatorController walkWithGunAnimator, walkWithBowAnimator, walkWithOutWeaponsAnimator;

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
                GameObject player = GameObject.Find("PlayerWithOutWeapons");
                if (player != null)
                {
                    Animator playerAnimator = player.GetComponent<Animator>();
                    if (playerAnimator != null)
                    {
                        switch (itemName)
                        {
                            case "Gun":
                                playerAnimator.runtimeAnimatorController = walkWithGunAnimator;
                                Debug.Log("AnimatorController changed to WalkWithGun!");
                                break;

                            case "Bow":
                                playerAnimator.runtimeAnimatorController = walkWithBowAnimator;
                                Debug.Log("AnimatorController changed to WalkWithBow!");
                                break;

                            case "":
                            case null:
                                playerAnimator.runtimeAnimatorController = walkWithOutWeaponsAnimator;
                                Debug.Log("AnimatorController changed to WalkWithOutWeapons!");
                                break;

                            default:
                                Debug.LogWarning("Unknown item name, no animator controller changed.");
                                break;
                        }
                    }
                    else
                    {
                        Debug.LogError("Animator not found on PlayerWithOutWeapons!");
                    }
                }
                else
                {
                    Debug.LogError("PlayerWithOutWeapons not found!");
                }

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
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        SpriteRenderer spriteRenderer = itemToDrop.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite;
        spriteRenderer.sortingLayerName = "Objects";

        itemToDrop.AddComponent<BoxCollider2D>();

        itemToDrop.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(1.5f, 0f, 0f);
        itemToDrop.transform.localScale = new Vector3(.5f, .5f, .5f);

        if (itemToDrop.GetComponent<BoxCollider2D>().isTrigger == false)
        {
            itemToDrop.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        if (this.quantity <= 0)
        {
            EmptySlot();
        }
    }
}
