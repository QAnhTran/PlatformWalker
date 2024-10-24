using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public bool isConsumable;
    public GameObject itemPrefab;  
        
    public virtual void UseItem()
    {
        //  Destroy the item after using (for consumables)
        Debug.Log("Using item: " + itemName);

        if (itemName == "Health Potion")
        {
            // Heal the player
           /* PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(50);  // Heal the player by 50 HP
            } */
        }
        else if (itemName == "Bomb")
        {
            // Use the bomb (this could be throwing or dropping the bomb)
            Debug.Log("Using bomb");
            
        }

        // Remove the item from the inventory after use
    //    InventoryManager.Instance.RemoveItem(this);
    }
}
