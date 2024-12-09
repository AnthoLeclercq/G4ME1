using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    private Text interactUI;
    public DialogueNPC dialogue;

    public bool isInRange;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
        interactUI.enabled = false;
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
            TriggerDialogue();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            interactUI.enabled = false;
        }
    }

    void TriggerDialogue() => DialogueManager.instance.StartDialogue(dialogue);

}
