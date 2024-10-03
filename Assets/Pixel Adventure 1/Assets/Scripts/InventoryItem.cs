using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemName;   
    public Sprite itemIcon;    
    public bool isConsumable;  
    public void UseItem()
    {
        Debug.Log("Using item: " + itemName);

        switch (itemName)
        {
            case "Health Potion":
             //   PlayerHealth.Instance.RestoreHealth(50); 
                break;
            case "Bomb":
             //   PlayerAttack.Instance.ThrowBomb();       
                break;
            default:
                Debug.Log("No specific use for this item.");
                break;
        }

        if (isConsumable)
        {
            InventoryManager.Instance.RemoveItem(this);
        }
    }
}
