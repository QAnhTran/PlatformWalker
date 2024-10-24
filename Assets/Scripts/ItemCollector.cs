using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource collectionSoundEffect;
    public Sprite itemIcon;
    [SerializeField] private GameObject cherryPrefab;
    public Sprite bombIcon; // Assign the bomb sprite in the Inspector
    public GameObject bombPrefab;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectionSoundEffect.Play();

            // Create a new Item with the cherry's icon and prefab
            Item newItem = new Item(name)
            {
                name = "Cherry",
                icon = itemIcon,
                prefab = cherryPrefab
            };

            inventoryManager.AddItemToSlot(itemIcon, newItem);
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }

        if (collision.gameObject.CompareTag("Bomb"))
        {
            collectionSoundEffect.Play();
            Item bombItem = new Item(name)
            {
                name = "Bomb",
                icon = itemIcon,
                prefab = bombPrefab
            };
            inventoryManager.AddItemToSlot(bombIcon, bombItem);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            collectionSoundEffect.Play();

            // Create a new Item with the cherry's icon and prefab
            Item newItem = new Item(name)
            {
                name = "Shield",
                icon = itemIcon,
                prefab = cherryPrefab
            };

            inventoryManager.AddItemToSlot(itemIcon, newItem);
            Destroy(collision.gameObject);
        }
    }
}
