using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool destroy;
    private DialogueManager Manager;

    public void TriggerDialogue()
    {
        Manager = FindObjectOfType<DialogueManager>();

        if (destroy && !Manager.whileDialogue)
        {
            this.gameObject.SetActive(false);
        }

        Manager.StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            TriggerDialogue();
        }
    }
}
