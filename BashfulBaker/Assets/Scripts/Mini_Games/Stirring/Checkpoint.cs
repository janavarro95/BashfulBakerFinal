using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Tracks the stirring progression
/// </summary>
public class Checkpoint : MonoBehaviour {
    /// <summary>
    /// tracks the location of the Checker Box
    /// </summary>
    int state;

    /// <summary>
    /// How far (counted in %) we are toward being done stirring
    /// </summary>
    int stirPercentage;

    /// <summary>
    /// Percent increase per revolution
    /// </summary>
    public int Increment;


    /// <summary>
    /// the number of ingredients that need to be added to the bowl
    /// </summary>
    public int ingredients;

    public Sprite completeIcon;

    private int c;


  /*  private int C
    {
        get
        {
            return c;
        }
        set
        {
            c = value;
        }
    }*/


    private void Start()
    {
        Increment = 15;
        stirPercentage = 0;
        state = 0;
        ingredients = 0;
    }
  

    private void Update()
    {
        // on pressing the A button we add anew ingredients
        if (Assets.Scripts.GameInput.InputControls.APressed)
        {
            //sliding scale code for Increment (not needed, works w/ or w.out
            /* Increment = (10 * stirPercentage) / 100;
             if (Increment < 5)
              {
                 Increment = 5;
               }
               */

            //Add 1 to ingredients if we are under 4 
            //signifying we have added an ingredient to the bowl 
            if (ingredients < 4)
             {
                ingredients += 1;
               // //Debug.Log(ingredients);
                //Debug.Log(stirPercentage);
            }
            else if (stirPercentage == 100 && ingredients >= 4)
            {
                //SceneManager.LoadScene("Kitchen");
                actuallyTransition();
            }

            //Update the Percentage
            //lower to 60% of older value to show a new ingredient has been added      
            stirPercentage = (stirPercentage * 6) / 10;
            //Debug.Log(stirPercentage);

        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Move the Checker as it gets touched

        if (coll.gameObject.tag == "Player" && state ==0){
            //Debug.Log("0th square");
            this.transform.position = new Vector3(1, 1, 0);
            state = 1;
        }else

        if (coll.gameObject.tag == "Player" && state == 1)
        {
           // //Debug.Log("1st square");
            this.transform.position = new Vector3(1, -1, 0);
            state = 2;
        }else

        if (coll.gameObject.tag == "Player" && state == 2)
        {
            //Debug.Log("2nd square");
            this.transform.position = new Vector3(-1, -1, 0);
            state = 3;
        }else

        if (coll.gameObject.tag == "Player" && state == 3)
        {
            //Debug.Log("3rd square");
            this.transform.position = new Vector3(1, -1, 0);
            state = 0;

            if (ingredients == 0) return; //It doesn't make sense that we can stir something with no ingredients in the bowl so we just return.

            if (stirPercentage < 100)
            {
                stirPercentage = stirPercentage + Increment;


                if (stirPercentage > 100)
                {
                    stirPercentage = 100;
                }


                if (ingredients == 1 && stirPercentage == 100)
                {
                    GameObject Checkmark1 = new GameObject();
                    Checkmark1.AddComponent<SpriteRenderer>();
                    Checkmark1.GetComponent<SpriteRenderer>().sprite = completeIcon;
                    Checkmark1.transform.position = new Vector3(-4.45f, 1.2f, 0);
                    Checkmark1.layer = 1;
                }
                if (ingredients == 2 && stirPercentage == 100)
                {
                    GameObject Checkmark2 = new GameObject();
                    Checkmark2.AddComponent<SpriteRenderer>();
                    Checkmark2.GetComponent<SpriteRenderer>().sprite = completeIcon;
                    Checkmark2.transform.position = new Vector3(-4.45f, .25f, 0);
                    Checkmark2.layer = 1;
                }
                if (ingredients == 3 && stirPercentage == 100)
                {
                    GameObject Checkmark3 = new GameObject();
                    Checkmark3.AddComponent<SpriteRenderer>();
                    Checkmark3.GetComponent<SpriteRenderer>().sprite = completeIcon;
                    Checkmark3.transform.position = new Vector3(-4.45f, -.85f, 0);
                    Checkmark3.layer = 1;
                }
                if (ingredients == 4 && stirPercentage == 100)
                {
                    GameObject Checkmark4 = new GameObject();
                    Checkmark4.AddComponent<SpriteRenderer>();
                    Checkmark4.GetComponent<SpriteRenderer>().sprite = completeIcon;
                    Checkmark4.transform.position = new Vector3(-4.45f, -1.9f, 0);
                    Checkmark4.layer = 1;
                }


                if (ingredients == 4 && stirPercentage == 100)
                {
                    GameObject FinalCheck = new GameObject();
                    FinalCheck.AddComponent<SpriteRenderer>();
                    FinalCheck.GetComponent<SpriteRenderer>().sprite = completeIcon;
                    FinalCheck.transform.position = new Vector3( 0, 0, 0);
                    FinalCheck.layer = 1;
                }



            }
        }

        //Debug.Log(stirPercentage);

    }
    private void actuallyTransition()
    {
        ScreenTransitions.StartSceneTransition(.5f, "Kitchen", Color.black, ScreenTransitions.TransitionState.FadeOut, new VoidDelegate(finishedTransition));
    }
    private void finishedTransition()
    {
        Game.Player.setSpriteVisibility(Enums.Visibility.Visible);
        Game.HUD.showHUD = true;
        Game.HUD.showAll();
        Game.LoadCorrectKitchenScene();
        ScreenTransitions.PrepareForSceneFadeIn(.5f, Color.black);
    }
    /*
              for (int x = 0; x < totalIngredients; x++)
        {
            if (ingredients == x && stirPercentage == 100)
            {
                GameObject Checkmark = new GameObject();
                Checkmark.AddComponent<SpriteRenderer>();
                Checkmark.GetComponent<SpriteRenderer>().sprite = completeIcon;

                if (x == 1) Checkmark.transform.position = new Vector3(5, 4, 0);
                if (x == 2) Checkmark.transform.position = new Vector3(5, 3.5f, 0);
                if (x == 3) Checkmark.transform.position = new Vector3(5, 3, 0);
                if (x == 4) Checkmark.transform.position = new Vector3(5, 2.5f, 0);


                Checkmark.layer = 1;
            }
        }
     */

}
