using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts;
using Assets.Scripts.GameInformation;

public class DialogueManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI dialogueText;
    public Animator animator;


    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        Game.DialogueManager = this;
    }
    private void Update()
    {
        if (animator.GetBool("isOpen") &&InputControls.APressed)
        {
            DisplayNextSentence();
        }
    }
    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }

}
