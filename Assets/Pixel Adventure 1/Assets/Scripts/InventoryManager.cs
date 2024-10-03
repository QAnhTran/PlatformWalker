using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton instance

    public List<Item> inventoryItems = new List<Item>(); // List of items in the inventory
    public Transform inventoryUI; // Reference to the UI grid
    public GameObject itemSlotPrefab; // Prefab for each item slot
    public GameObject[] inventorySlots;
    public int itemsPerPage = 9;
    public Transform inventoryGrid;
    public Button inventoryButton;
    public GameObject selectedItem;
    

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
        DisplayPage(currentPage);
    }

    void DisplayPage(int page)
    {
        ClearInventoryUI(); 

        int startItemIndex = page * itemsPerPage;
        int endItemIndex = Mathf.Min(startItemIndex + itemsPerPage, inventoryItems.Count);

        for (int i = startItemIndex; i < endItemIndex; i++)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryUI);
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
        GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryGrid);

        Image iconImage = itemSlot.transform.Find("Icon").GetComponent<Image>(); 
        Text itemName = itemSlot.transform.Find("ItemName").GetComponent<Text>();

        iconImage.sprite = newItem.icon;  
        itemName.text = newItem.name;     
        inventoryItems.Add(newItem);
        DisplayPage(currentPage); 
    }

    public void AddItemToSlot(Sprite itemSprite, InventoryItem item)
    {
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


    public void SelectItem(InventoryItem item)
    {
        selectedItem = item.gameObject;
        Debug.Log("Selected item: " + item.itemName);
    }

    public void UseSelectedItem()
    {
        if (selectedItem != null)
        {
            InventoryItem itemComponent = selectedItem.GetComponent<InventoryItem>();
            itemComponent.UseItem();
        }
        else
        {
            Debug.Log("No item selected.");
        }
    }

    public void RemoveItem(InventoryItem item)
    {
        foreach (GameObject slot in inventorySlots)
        {
            Image slotImage = slot.transform.GetChild(0).GetComponent<Image>();
            if (slotImage.sprite == item.itemIcon)
            {
                slotImage.sprite = null; 
                break;
            }
        }
    }

    void ClearInventoryUI()
    {
        foreach (Transform child in inventoryUI)
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

    public Item(string name, Sprite icon = null)
    {
        this.name = name;
        this.icon = icon;
    }
}
