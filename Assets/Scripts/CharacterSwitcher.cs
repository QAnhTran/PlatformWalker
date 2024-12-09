using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject otherCharacter; 
    [SerializeField] private Transform handTransform; 
    [SerializeField] private Transform otherCharacterHand; 
    [SerializeField] private GameObject heldItem; 
    [SerializeField] private bool isCurrentCharacterActive = true; 

    void Start()
    {
        if (isCurrentCharacterActive)
        {
            otherCharacter.SetActive(false);
            gameObject.tag = "Player"; 
            otherCharacter.tag = "Untagged";

            UpdateCameraTarget(gameObject.transform);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchCharacter();
        }
    }

    private void SwitchCharacter()
    {
        // Sync positions and rotations between characters
        otherCharacter.transform.position = transform.position;
        otherCharacter.transform.rotation = transform.rotation;

        // Transfer held item to the other character's hand
        if (heldItem != null)
        {
            // Determine the target hand based on the current active character
            Transform targetHand = isCurrentCharacterActive ? otherCharacterHand : handTransform;

            // Re-parent the held item to the target hand
            heldItem.transform.SetParent(targetHand);
            heldItem.transform.localPosition = Vector3.zero;
            heldItem.transform.localRotation = Quaternion.identity;
        }

        gameObject.SetActive(false);
        otherCharacter.SetActive(true);

        InventoryManager.Instance.UpdateActivePlayer();

        gameObject.tag = "Untagged";
        otherCharacter.tag = "Player";

        InventoryManager.Instance.SetActiveHand(isCurrentCharacterActive ? otherCharacterHand : handTransform);

        isCurrentCharacterActive = !isCurrentCharacterActive;

        UpdateCameraTarget(otherCharacter.transform);

        Debug.Log($"Switched to: {otherCharacter.name}");
    }


    private void UpdateCameraTarget(Transform newTarget)
    {
        foreach (var cameraController in FindObjectsOfType<Cameracontroller>())
        {
            cameraController.UpdatePlayerTarget(newTarget);
        }
    }
}
