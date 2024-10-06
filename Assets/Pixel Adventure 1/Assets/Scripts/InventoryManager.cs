using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; 

    public List<Item> inventoryItems = new List<Item>();                                                      
    public GameObject itemSlotPrefab; 
    public GameObject[] inventorySlots;
    public int itemsPerPage = 9;
    public Transform inventoryGrid;
    public Button inventoryButton;
    public Item selectedItem;
    public GameObject selectedItemPrefab;  
    public PlayerMove player;


    private int currentPage = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        DisplayPage(currentPage);
    }

    void DisplayPage(int page)
    {
        ClearInventoryUI();

        int startItemIndex = page * itemsPerPage;
        int endItemIndex = Mathf.Min(startItemIndex + itemsPerPage, inventoryItems.Count);

        for (int i = startItemIndex; i < endItemIndex; i++)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryGrid);
            itemSlot.GetComponentInChildren<Text>().text = inventoryItems[i].name;
        }
    }

    void CreateInventoryTiles()
    {
        for (int i = 0; i < itemsPerPage; i++)
        {
            GameObject tile = Instantiate(itemSlotPrefab, transform);
        }
    }

    public void AddItem(Item newItem)
    {
        if (newItem == null)
        {
            Debug.LogError("Attempted to add a null item to the inventory!");
            return;
        }

        Debug.Log("Adding item: " + newItem.name);

        GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryGrid);

        Image iconImage = itemSlot.transform.Find("Icon").GetComponent<Image>();
        Text itemName = itemSlot.transform.Find("ItemName").GetComponent<Text>();

        iconImage.sprite = newItem.icon;
        itemName.text = newItem.name;
        inventoryItems.Add(newItem);

        DisplayPage(currentPage);
    }



    public void AddItemToSlot(Sprite itemSprite, Item item)
    {
        if (item == null)
        {
            Debug.LogError("Cannot add a null item to the inventory slot.");
            return;
        }
        foreach (GameObject slot in inventorySlots)
        {
            Image slotImage = slot.transform.GetChild(0).GetComponent<Image>();

            if (slotImage.sprite == null)
            {
                slotImage.sprite = itemSprite;
                slot.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));
                break;
            }
        }
    }


    public void SelectItem(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Item is null! Cannot select a null item.");
            return;
        }

        selectedItemPrefab = item.prefab;  

        Debug.Log("Selected item: " + item.name);
    }


    public void UseSelectedItem()
    {
        if (selectedItemPrefab == null)
        {
            Debug.LogError("No item is selected! Cannot use the item.");
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player reference is missing! Cannot equip the item.");
            return;
        }

        // Clear any previous item in the player's hand
        if (player.handTransform.childCount > 0)
        {
            foreach (Transform child in player.handTransform)
            {
                Destroy(child.gameObject);  
            }
        }

        // Instantiate the selected item in the player's hand
        GameObject instantiatedItem = Instantiate(selectedItemPrefab, player.handTransform);
        instantiatedItem.transform.localPosition = Vector3.zero;
        instantiatedItem.transform.localRotation = Quaternion.identity;

        Debug.Log("Item equipped in player's hand: " + instantiatedItem.name);
    }

    public void RemoveItem(Item item)
    {
        foreach (GameObject slot in inventorySlots)
        {
            Image slotImage = slot.transform.GetChild(0).GetComponent<Image>();
            if (slotImage.sprite == item.icon)
            {
                slotImage.sprite = null;
                break;
            }
        }
    }

    void ClearInventoryUI()
    {
        foreach (Transform child in inventoryGrid)
        {
            Destroy(child.gameObject);
        }
    }

    public void NextPage()
    {
        if ((currentPage + 1) * itemsPerPage < inventoryItems.Count)
        {
            currentPage++;
            DisplayPage(currentPage);
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            DisplayPage(currentPage);
        }
    }
}


public class Item
{
    public string name;
    public Sprite icon;
    public GameObject prefab; 

    public Item(string name, Sprite icon = null, GameObject prefab = null)
    {
        this.name = name;
        this.icon = icon;
        this.prefab = prefab;  
    }
}
