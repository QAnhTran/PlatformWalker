using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource collectionSoundEffect;

    [SerializeField] private GameObject cherryPrefab;
    public GameObject bombPrefab;
    [SerializeField] private GameObject shieldPrefab;

    public Sprite itemIcon;
    public Sprite bombIcon;
    public Sprite shieldIcon;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            CollectItem("Cherry", itemIcon, cherryPrefab);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Bomb"))
        {
            CollectItem("Bomb", bombIcon, bombPrefab);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            CollectItem("Shield", shieldIcon, shieldPrefab);
            Destroy(collision.gameObject);
        }

        
    }

    private void CollectItem(string itemName, Sprite itemIcon, GameObject itemPrefab)
    {
        collectionSoundEffect.Play();

        // Create a new Item with the provided name, icon, and prefab
        Item newItem = new Item(itemName)
        {
            name = itemName,
            icon = itemIcon,
            prefab = itemPrefab
        };

        inventoryManager.AddItemToSlot(itemIcon, newItem);
    }
}
