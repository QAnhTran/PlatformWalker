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
    }
}
