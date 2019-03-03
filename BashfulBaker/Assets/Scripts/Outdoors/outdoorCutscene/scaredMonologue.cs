using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;


public class scaredMonologue : MonoBehaviour
{
    private bool usable;
    public Dialogue Scared;
    public Animator Guard;
    public Sprite guardFace;
    public AnimationCurve goatPlayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {      
            FindObjectOfType<DialogueManager>().StartDialogue(Scared);
            Destroy(gameObject);     
    }

    public void MoveToDane()
    {

            goatPlayer = new AnimationCurve(
                new Keyframe(GameObject.Find("Guard").GetComponent<Transform>().position.x, GameObject.Find("Player(Clone)").GetComponent<Transform>().position.y), 
                new Keyframe(GameObject.Find("Player(Clone)").GetComponent<Transform>().position.x, GameObject.Find("Guard").GetComponent<Transform>().position.y));
           //goatPlayer.preWrapMode = WrapMode.Linear;
            //goatPlayer.postWrapMode = WrapMode.Linear;
                
    }
    
}
