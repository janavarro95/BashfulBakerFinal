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

        // set blips
        if (nameText.text == "Dane")
            SetBlip(maleBlip, 1.25f, 8);
        else if (nameText.text == "Sylvia")
            SetBlip(femaleBlip, 1f, 8);
        else if(nameText.text == "Jeb")
            SetBlip(maleBlip, 0.5f, 12);
        else if(nameText.text == "Sully")
            SetBlip(maleBlip, 0.75f, 4);
        else if (nameText.text == "Amari")
            SetBlip(femaleBlip, 1.25f, 4);
        else if (nameText.text == "Guard")
            SetBlip(maleBlip, 1f, 4+Game.Player.PlayerMovement.breathingProficiency);
        else if(nameText.text == "Brian")
            SetBlip(maleBlip, 1f, 12);
        else if (nameText.text == "Ian")
            SetBlip(femaleBlip, 1f, 8);
        else if(nameText.text == "Dog")
            SetBlip(femaleBlip, 0.5f, 4);
        else if (nameText.text == "Raccoon")
            SetBlip(femaleBlip, 1.5f, 4);
        else
            SetBlip(maleBlip, 1f, 8);

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    void SetBlip(AudioClip b, float p, int m)
    {
        audioSource.clip = b;
        audioSource.pitch = p;
        blipMod = m;
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
