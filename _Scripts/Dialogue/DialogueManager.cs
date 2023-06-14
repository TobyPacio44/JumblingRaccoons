using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public SpriteRenderer dialogueFace;
    public Canvas canvas;

    public bool whileDialogue = false;

    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
        canvas = GetComponent<Canvas>();
    }

    public void Update()
    {
        if (Input.GetKeyDown("return")){
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (!whileDialogue)
        {
            whileDialogue = true;
            canvas.enabled = true;
            nameText.text = dialogue.name;
            dialogueFace.sprite = dialogue.dialogueFace;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        whileDialogue = false;
        dialogueFace.sprite = null;
        canvas.enabled = false;
    }
}
