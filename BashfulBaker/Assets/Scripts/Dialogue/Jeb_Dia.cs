using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeb_Dia : MonoBehaviour
{

    public Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

            TriggerDialogue();
        
    }


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    }
}
