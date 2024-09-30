using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton instance

    public List<Item> inventoryItems = new List<Item>(); // List of items in the inventory
    public Transform inventoryUI; // Reference to the UI grid
    public GameObject itemSlotPrefab; // Prefab for each item slot
    public int itemsPerPage = 9;
    public Transform inventoryGrid;
    public Button inventoryButton;

    private int currentPage = 0;

    private void Awake()
    {
        // Ensure only one instance of the InventoryManager exists
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

    // Function to display the items for the current page
    void DisplayPage(int page)
    {
        ClearInventoryUI(); // Clear current UI slots

        int startItemIndex = page * itemsPerPage;
        int endItemIndex = Mathf.Min(startItemIndex + itemsPerPage, inventoryItems.Count);

        // Display items for the current page
        for (int i = startItemIndex; i < endItemIndex; i++)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryUI);
            itemSlot.GetComponentInChildren<Text>().text = inventoryItems[i].name;
        }
    }

    // Function to add a new item to the inventory
    public void AddItem(Item newItem)
    {
        // Instantiate the item slot prefab into the inventory grid
        GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryGrid);

        // Get the image and text components to update the slot with the item’s icon and name
        Image iconImage = itemSlot.transform.Find("Icon").GetComponent<Image>(); // Ensure "Icon" is the correct child name
        Text itemName = itemSlot.transform.Find("ItemName").GetComponent<Text>();

        // Set the icon and name for the item slot
        iconImage.sprite = newItem.icon;  // Set the item's icon
        itemName.text = newItem.name;     // Set the item's name
        inventoryItems.Add(newItem);
        DisplayPage(currentPage);  // Refresh the inventory UI after adding a new item
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

// Updated Item class to include an icon (optional)
public class Item
{
    public string name;
    public Sprite icon;  // Icon to display in the inventory (optional)

    public Item(string name, Sprite icon = null)
    {
        this.name = name;
        this.icon = icon;
    }
}
