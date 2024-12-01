using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject otherCharacter; // Nhân vật để chuyển đổi sang
    [SerializeField] private bool isCurrentCharacterActive = true; // Trạng thái: nhân vật hiện tại đang hoạt động

    void Start()
    {
        // Nhân vật này bắt đầu ở trạng thái hoạt động, nhân vật khác bị ẩn
        if (isCurrentCharacterActive)
        {
            gameObject.SetActive(true);
            otherCharacter.SetActive(false);
            otherCharacter.tag = "Untagged";
        }
    }

    void Update()
    {
        // Nhấn phím T để chuyển đổi nhân vật
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchCharacter();
        }
    }

    private void SwitchCharacter()
    {
        if (isCurrentCharacterActive)
        {
            otherCharacter.transform.position = transform.position;
            otherCharacter.transform.rotation = transform.rotation;
            otherCharacter.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = otherCharacter.transform.position;
            transform.rotation = otherCharacter.transform.rotation;
            gameObject.SetActive(true);
            otherCharacter.SetActive(false);
        }

        // Update the Player tag for the new active character
        gameObject.tag = "Player";
        otherCharacter.tag = "Untagged";

        // Update the state
        isCurrentCharacterActive = !isCurrentCharacterActive;

        // Notify the inventory system of the active player change
        FindObjectOfType<InventoryManager>()?.UpdateActivePlayer();
    }

}
