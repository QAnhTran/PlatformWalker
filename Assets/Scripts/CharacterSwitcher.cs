using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject otherCharacter; // The other character to switch to
    [SerializeField] private Transform handTransform; // Hand transform for this character
    [SerializeField] private Transform otherCharacterHand; // Hand transform for the other character
    [SerializeField] private GameObject heldItem; // Reference to the currently held item
    [SerializeField] private bool isCurrentCharacterActive = true; // Tracks if this is the active character

    void Start()
    {
        // Initialize active/inactive characters and tags
        if (isCurrentCharacterActive)
        {
            otherCharacter.SetActive(false);
            gameObject.tag = "Player"; // This character starts active
            otherCharacter.tag = "Untagged";

            // Update the camera target on start
            UpdateCameraTarget(gameObject.transform);
        }
    }

    void Update()
    {
        // Switch character on key press
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

        // Toggle active/inactive states
        gameObject.SetActive(false);
        otherCharacter.SetActive(true);

        // Notify InventoryManager to update the active player and hand
        InventoryManager.Instance.UpdateActivePlayer();

        // Update tags for active/inactive characters
        gameObject.tag = "Untagged";
        otherCharacter.tag = "Player";

        // Update heldItem reference in the InventoryManager
        InventoryManager.Instance.SetActiveHand(isCurrentCharacterActive ? otherCharacterHand : handTransform);

        // Toggle the state variable
        isCurrentCharacterActive = !isCurrentCharacterActive;

        // Notify the camera to update its target
        UpdateCameraTarget(otherCharacter.transform);

        // Debug logs for confirmation
        Debug.Log($"Switched to: {otherCharacter.name}");
    }


    private void UpdateCameraTarget(Transform newTarget)
    {
        // Update all cameras with the new target
        foreach (var cameraController in FindObjectsOfType<Cameracontroller>())
        {
            cameraController.UpdatePlayerTarget(newTarget);
        }
    }
}
