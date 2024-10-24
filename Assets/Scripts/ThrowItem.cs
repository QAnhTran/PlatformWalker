using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    public Transform handTransform; // Where the bomb will spawn from
    public float throwForce = 10f;

    private InventoryManager inventoryManager; // To get the selected item

    void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    void Update()
    {
     /*   if (Input.GetButtonDown("Fire1") && inventoryManager.selectedItem != null)
        {
            // Check if the selected item is of type BombItem
            if (inventoryManager.selectedItem is BombItem selectedBomb)
            {
                ThrowBomb(selectedBomb);  // Only throw if it's a BombItem
            }
            else
            {
                Debug.Log("Selected item is not a bomb!");
            }
        } */
    }


    void ThrowBomb(BombItem bombItem)
    {
        GameObject bomb = Instantiate(bombItem.prefab, handTransform.position, Quaternion.identity);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();

        // Calculate throw direction
        Vector2 throwDirection = new Vector2(transform.localScale.x, 0.5f).normalized; // Slight upward throw
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

        // Call explosion after a delay
        BombController bombController = bomb.GetComponent<BombController>();
        bombController.SetupBomb(bombItem);
    }
}