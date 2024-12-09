using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
   [SerializeField] AudioSource SoundDeathEffect;
    private bool playerIsClose;

    void Start()
    {
        HideDialoguePanel();
    }

    void Update()
    {
        if (playerIsClose)
        {
            dialoguePanel.SetActive(true);
        }
        else
        {
            HideDialoguePanel();
        }
    }

    void HideDialoguePanel()
    {
        dialoguePanel.SetActive(false);

    }


    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
               SoundDeathEffect.Play();
            playerIsClose = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            HideDialoguePanel(); 
        }
    }
}
