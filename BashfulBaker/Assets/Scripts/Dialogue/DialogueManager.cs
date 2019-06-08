using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts;
using Assets.Scripts.GameInformation;
using System;
using Assets.Scripts.Utilities.Delegates;

public class DialogueManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI dialogueText;
    public Animator animator;
    public bool sentenceFinished = false;
    public AudioClip maleBlip;
    public AudioClip femaleBlip;
    public AudioSource audioSource;
    public float pitch = 1;
    public int blipMod = 5;

    private Queue<string> sentences;
    private string currentSentence;

    public bool IsDialogueUp
    {
        get
        {
            return animator.GetBool("isOpen");
        }
        set
        {
            animator.SetBool("isOpen", value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        Game.DialogueManager = this;
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = maleBlip;
    }

    private void Update()
    {
        if (animator.GetBool("isOpen") && InputControls.APressed)
        {
            if (String.IsNullOrEmpty(this.currentSentence))
            {
                DisplayNextSentence();
            }
            else if(this.dialogueText.text != this.currentSentence)
            {
                this.dialogueText.text = this.currentSentence;
                StopAllCoroutines();
            }
            else if (this.dialogueText.text == this.currentSentence)
            {
                if (sentences.Count != 0)
                    this.currentSentence = "";
                DisplayNextSentence();
            }
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
            if (this.dialogueText.text == this.currentSentence)
            {
                EndDialogue();
            }
            return;
        }
        string sentence = sentences.Dequeue();
        this.currentSentence = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        sentenceFinished = false;
        int count = 0;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            if (count % blipMod == 0)
            {
                audioSource.pitch = pitch;
                audioSource.Play();
            }

            count++;

            yield return null;
        }
        sentenceFinished = true;
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }

}
