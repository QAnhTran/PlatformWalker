using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject otherCharacter; // The other character to switch to
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

        // Toggle active/inactive states
        gameObject.SetActive(false);
        otherCharacter.SetActive(true);

        // Update tags for active/inactive characters
        gameObject.tag = "Untagged";
        otherCharacter.tag = "Player";

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
