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
    public Transform activePlayerHand; // Reference to the active player's hand
    public Transform inventoryGrid;
    public Button inventoryButton;
    public Item selectedItem;
    public GameObject selectedItemPrefab;
    public Sprite bombIcon;
    public GameObject bombPrefab;
    public PlayerMove player;
    public int throwForce;
    public lifeplayerLV1 playerLifeScript;
    public Dictionary<GameObject, int> itemUses = new Dictionary<GameObject, int>();
    private Transform activePlayer;


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
        GameObject bombPrefab = GameObject.FindGameObjectWithTag("Bomb");/* assign your bomb prefab */;
        UpdateActivePlayer();
        //        itemUses[bombPrefab] = 3; // Set the initial uses for the bomb
    }

    void Update()
    {
        // Check if Q key is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Call the ThrowBomb method when Q is pressed
            ThrowBomb();
        }
    }

    public void DisplayPage(int page)
    {
        ClearInventoryUI();  // Clear existing items in the grid

        // Populate the grid with new items
        int startIndex = page * itemsPerPage;
        for (int i = startIndex; i < startIndex + itemsPerPage; i++)
        {
            if (i < inventoryItems.Count)
            {
                AddItemToGrid(inventoryItems[i]);  // Custom method to add items to the grid
            }
        }
    }

    public void UpdateActivePlayer()
    {
        // Dynamically find the active player by tag
        GameObject activePlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (activePlayerObject != null)
        {
            activePlayer = activePlayerObject.transform;
            activePlayerHand = activePlayer.Find("Hand"); // Assuming each character has a "Hand" child object
        }
    }

    public void AddItemToGrid(Item item)
    {
        GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryGrid);
        itemSlot.transform.Find("Item Icon").GetComponent<Image>().sprite = item.icon;
        itemSlot.transform.Find("Item Name").GetComponent<Text>().text = item.name;
    }


    public void ThrowBomb()
    {
        if (selectedItemPrefab != null)
        {
            BombItem bombItem = selectedItemPrefab.GetComponent<BombItem>();

            if (bombItem != null)
            {
                if (bombItem.usesLeft > 0)
                {
                    // Instantiate the bomb and apply throwing logic
                    GameObject bombInstance = Instantiate(selectedItemPrefab, player.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                    Rigidbody2D bombRb = bombInstance.GetComponent<Rigidbody2D>();

                    if (bombRb != null)
                    {
                        bombRb.isKinematic = false;
                        Collider2D bombCollider = bombInstance.GetComponent<Collider2D>();
                        if (bombCollider != null)
                        {
                            bombCollider.enabled = true;
                        }

                        Vector2 throwDirection = new Vector2(1f, 0.5f);
                        bombRb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

                        // Decrement bomb use
                        bombItem.usesLeft--;
                        Debug.Log("Bomb thrown! Uses left: " + bombItem.usesLeft);
                        if (bombItem.usesLeft <= 0)
                        {
                            Debug.Log("Bomb out of uses! Removing from inventory.");
                            ClearPlayerHand();
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("No more uses left for the bomb.");
                }
            }
            else
            {
                Debug.LogWarning("The selected item is not a bomb.");
            }
        }
        else
        {
            Debug.LogWarning("No bomb selected to throw.");
        }
    }

    public void ClearPlayerHand()
    {
        if (player.handTransform.childCount > 0)
        {
            foreach (Transform child in player.handTransform)
            {
                Destroy(child.gameObject);
            }
        }
    }



    public void AddItem(Item newItem)
    {
        Debug.Log("AddItem called for: " + newItem.name);

        if (newItem == null)
        {
            Debug.LogError("Attempted to add a null item to the inventory!");
            return;
        }

        Debug.Log("Adding item: " + newItem.name);

        // Instantiate the prefab
        GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryGrid);

        if (itemSlot == null)
        {
            Debug.LogError("Failed to instantiate itemSlotPrefab!");
            return;
        }

        // Find the Icon child
        Transform iconTransform = itemSlot.transform.Find("Item Icon");
        if (iconTransform == null)
        {
            Debug.LogError("Icon object not found in the itemSlotPrefab!");
            return;
        }

        // Find the name child
        Transform nameTransform = itemSlot.transform.Find("Item Name");
        if (nameTransform == null)
        {
            Debug.LogError("Item Name object not found in the itemSlotPrefab!");
            return;
        }

        // Set the image and name
        Image iconImage = itemSlot.transform.Find("Item Icon").GetComponent<Image>();
        Text itemName = itemSlot.transform.Find("Item Name").GetComponent<Text>();

        if (iconImage != null)
            iconImage.sprite = newItem.icon;

        if (itemName != null)
            itemName.text = newItem.name;

        // Add item to inventory
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

        if (item.name == "Shield") // Ensure the item name matches your shield's name
        {
            playerLifeScript.hasShield = true;
        }
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

        // Check if it's a bomb
        BombItem bombItem = instantiatedItem.GetComponent<BombItem>();
        if (bombItem != null)
        {
            // Set bomb's Rigidbody2D to Kinematic when in hand
            Rigidbody2D bombRb = instantiatedItem.GetComponent<Rigidbody2D>();
            if (bombRb != null)
            {
                bombRb.isKinematic = true;
            }

            // Disable bomb's collider while in hand to avoid interaction with player
            Collider2D bombCollider = instantiatedItem.GetComponent<Collider2D>();
            if (bombCollider != null)
            {
                bombCollider.enabled = false;  // Disable collider while in hand
            }
        }

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
            // Destroy(child.gameObject);
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
    public int usesLeft;

    public Item(string name, Sprite icon = null, GameObject prefab = null, int uses = 1)
    {
        this.name = name;
        this.icon = icon;
        this.prefab = prefab;
        this.usesLeft = uses;
    }


}


