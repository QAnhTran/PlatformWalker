using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject otherCharacter;
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform otherCharacterHand;
    [SerializeField] private GameObject heldItem;
    [SerializeField] private bool isCurrentCharacterActive = true;
    [SerializeField] private Text transformationText;

    [SerializeField] private bool canTransform = true; // Flag to allow transformation
    private float transformationDuration = 10f; // Duration of the transformation
    private bool isTransformed = false;
    private Coroutine transformationCoroutine; // To track the coroutine

    void Start()
    {
        if (isCurrentCharacterActive)
        {
            otherCharacter.SetActive(false);
            gameObject.tag = "Player";
            otherCharacter.tag = "Untagged";

            UpdateCameraTarget(gameObject.transform);
        }

        if (transformationText != null)
        {
            transformationText.text = "1 use";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && canTransform)
        {
            Debug.Log("T key pressed, starting transformation.");
            if (transformationCoroutine == null)
            {
                transformationCoroutine = StartCoroutine(Transformation());
            }
        }
    }

    private IEnumerator Transformation()
    {
        Debug.Log("Transformation started!");
        canTransform = false;
        isTransformed = true;

        SwitchCharacter();



        try
        {
            SetUndetectable(true);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error in SetUndetectable: " + ex.Message);
        }

        Debug.Log("Waiting for transformation duration...");
        yield return GameManager.Instance.StartCoroutine(TransformationTimer());
    }

    private IEnumerator TransformationTimer()
    {
        yield return new WaitForSecondsRealtime(transformationDuration);

        Debug.Log("Transformation duration complete!");
        isTransformed = false;

        try
        {
            SetUndetectable(false);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error in SetUndetectable: " + ex.Message);
        }

        // Explicitly switch back to the first character
        SwitchCharacter(false);
        if (transformationText != null)
        {
            transformationText.text = "0 use";
        }
        Debug.Log("Transformation ended!");
    }




    private void SetUndetectable(bool state)
    {
        foreach (var enemy in FindObjectsOfType<EnemyBot>())
        {
            enemy.SetPlayerDetectable(!state);
        }

        if (state)
        {
            Debug.Log("Player is now undetectable by enemies.");
        }
        else
        {
            Debug.Log("Player is detectable again.");
        }
    }

    private void SwitchCharacter(bool switchToSecondCharacter = true)
    {
        if (switchToSecondCharacter)
        {
            otherCharacter.transform.position = transform.position;
            otherCharacter.transform.rotation = transform.rotation;

            if (heldItem != null)
            {
                heldItem.transform.SetParent(otherCharacterHand);
                heldItem.transform.localPosition = Vector3.zero;
                heldItem.transform.localRotation = Quaternion.identity;
            }

            gameObject.SetActive(false);
            otherCharacter.SetActive(true);

            gameObject.tag = "Untagged";
            otherCharacter.tag = "Player";

            InventoryManager.Instance.UpdateActivePlayer();
            InventoryManager.Instance.SetActiveHand(otherCharacterHand);

            isCurrentCharacterActive = false;

            UpdateCameraTarget(otherCharacter.transform);

            PlayerMove playerMove = otherCharacter.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                Animator otherAnimator = otherCharacter.GetComponent<Animator>();
                playerMove.SwitchAnimator(otherAnimator);
            }

            Debug.Log($"Switched to: {otherCharacter.name}");
        }
        else
        {
            transform.position = otherCharacter.transform.position;
            transform.rotation = otherCharacter.transform.rotation;

            if (heldItem != null)
            {
                heldItem.transform.SetParent(handTransform);
                heldItem.transform.localPosition = Vector3.zero;
                heldItem.transform.localRotation = Quaternion.identity;
            }

            otherCharacter.SetActive(false);
            gameObject.SetActive(true);

            otherCharacter.tag = "Untagged";
            gameObject.tag = "Player";

            InventoryManager.Instance.UpdateActivePlayer();
            InventoryManager.Instance.SetActiveHand(handTransform);

            isCurrentCharacterActive = true;

            UpdateCameraTarget(gameObject.transform);

            PlayerMove playerMove = GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                Animator currentAnimator = GetComponent<Animator>();
                playerMove.SwitchAnimator(currentAnimator);
            }

            Debug.Log($"Switched back to: {gameObject.name}");
        }
    }


    private void UpdateCameraTarget(Transform newTarget)
    {
        foreach (var cameraController in FindObjectsOfType<Cameracontroller>())
        {
            cameraController.UpdatePlayerTarget(newTarget);
        }
    }
}
