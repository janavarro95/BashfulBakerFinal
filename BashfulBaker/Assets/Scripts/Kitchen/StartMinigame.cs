using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameInput;
using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using Assets.Scripts.Utilities.Timers;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Delegates;

public class StartMinigame : MonoBehaviour
{
    public string minigame;
    public GameObject arrow;
    public int thisStep;
    public DeltaTimer timer;
    private int baked;

    public AudioSource timerSFX, chimeSFX;
    /// <summary>
    /// Used to determine if the player should be invisible in the minigame.
    /// </summary>
    public bool makePlayerInvisible;

    public Enums.CookingStationMinigame station;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Assets.Scripts.GameInformation.Game.IsMenuUp) return;

        if (collision.GetComponent<PlayerMovement>().currentStep == thisStep)
        {
            if (baked != 1)
            {
                //arrow.GetComponent<SpriteRenderer>().enabled = false;
                //arrow.GetComponent<progress>().A.SetActive(true);
                SetSprite(1);
            }

            if (InputControls.APressed && collision.GetComponent<PlayerMovement>().currentStep == thisStep)
            {
                /*
                if (Game.Player.activeItem == null || (Game.Player.activeItem is SpecialIngredient))
                {
                    Game.DialogueManager.StartDialogue(new Dialogue("Dane", new List<string>()
            {
                "I'm not holding anything I could "+getCookingVerb()
            }.ToArray()));
                    return;
                }
                */
                if(minigame == "Oven")
                {
                    if (baked == 0)
                    {
                        baked = 1;
                        SetSprite(2);
                        timerSFX.Play();
                        Invoke("Bake", 5);
                    }
                    else if (baked == 2)
                    {
                        SetSprite(0);
                        collision.GetComponent<PlayerMovement>().NextStep();

                    }
                }

                else if (Game.Player.activeItem is Dish)
                {
                    
                    Dish d = (Game.Player.activeItem as Dish);
                    /*
                    if (d.currentDishState == Enums.DishState.Ingredients && station != Enums.CookingStationMinigame.MixingBowl)
                    {
                        Game.DialogueManager.StartDialogue(new Dialogue("Dane", new List<string>()
                {
                    "I'm not holding anything I could "+getCookingVerb()
                }.ToArray()));
                        return;
                    }
                    if (d.currentDishState == Enums.DishState.Mixed && station != Enums.CookingStationMinigame.RollingStation)
                    {
                        Game.DialogueManager.StartDialogue(new Dialogue("Dane", new List<string>()
                {
                    "I'm not holding anything I could "+getCookingVerb()
                }.ToArray()));
                        return;
                    }
                    if (d.currentDishState == Enums.DishState.Prepped && station != Enums.CookingStationMinigame.Oven)
                    {
                        Game.DialogueManager.StartDialogue(new Dialogue("Dane", new List<string>()
                {
                    "I'm not holding anything I could "+getCookingVerb()
                }.ToArray()));
                        return;
                    }
                    if (d.currentDishState == Enums.DishState.Baked && station != Enums.CookingStationMinigame.PackingStation)
                    {
                        Game.DialogueManager.StartDialogue(new Dialogue("Dane", new List<string>()
                {
                    "I'm not holding anything I could "+getCookingVerb()
                }.ToArray()));
                        return;
                    }
                    */

                    //arrow.GetComponent<SpriteRenderer>().enabled = true;
                    //arrow.GetComponent<progress>().A.SetActive(false);
                    SetSprite(0);
                    collision.GetComponent<PlayerMovement>().NextStep();
                    if (makePlayerInvisible) Assets.Scripts.GameInformation.Game.Player.setSpriteVisibility(Enums.Visibility.Invisible);
                    Game.HUD.showOnlyTimer();
                    actuallyTransition();
                    //SceneManager.LoadScene(minigame);
                    updateDishState(d);
                }
            }

        }


    }

    private void actuallyTransition()
    {
        ScreenTransitions.StartSceneTransition(.5f, minigame, Color.black, ScreenTransitions.TransitionState.FadeOut, new VoidDelegate(finishedTransition));
    }
    private void finishedTransition()
    {
        Game.Player.setSpriteVisibility(Enums.Visibility.Invisible);
        Game.HUD.showHUD = false;
        SceneManager.LoadScene(minigame);
        ScreenTransitions.PrepareForSceneFadeIn(.5f, Color.black);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //arrow.GetComponent<SpriteRenderer>().enabled = true;
        //arrow.GetComponent<progress>().A.SetActive(false);

        if(minigame != "Oven" || baked != 1) SetSprite(0);
    }

    private void updateDishState(Dish d)
    {
        if (d.currentDishState == Enums.DishState.Ingredients && station == Enums.CookingStationMinigame.MixingBowl)
        {
            d.currentDishState = Enums.DishState.Mixed;
            return;
        }
        if (d.currentDishState == Enums.DishState.Mixed && station == Enums.CookingStationMinigame.RollingStation)
        {
            d.currentDishState = Enums.DishState.Prepped;
            return;
        }
        if (d.currentDishState == Enums.DishState.Prepped && station == Enums.CookingStationMinigame.Oven)
        {
            d.currentDishState = Enums.DishState.Baked;
            return;
        }
        if (d.currentDishState == Enums.DishState.Prepped && station == Enums.CookingStationMinigame.PackingStation)
        {
            d.currentDishState = Enums.DishState.Packaged;
            return;
        }
        if (d.currentDishState == Enums.DishState.Baked && station == Enums.CookingStationMinigame.PackingStation)
        {
            d.currentDishState = Enums.DishState.Packaged;
            return;
        }
    }

    private string getCookingVerb()
    {
        if (station == Enums.CookingStationMinigame.MixingBowl)
        {
            return "mix";
        }
        if (station == Enums.CookingStationMinigame.RollingStation)
        {
            return "roll";
        }
        if (station == Enums.CookingStationMinigame.PackingStation)
        {
            return "pack";
        }
        if (station == Enums.CookingStationMinigame.TrashCan)
        {
            return "trash";
        }
        if (station == Enums.CookingStationMinigame.Oven)
        {
            return "bake";
        }
        return "";
    }

    private void Bake()
    {
        timerSFX.Stop();
        chimeSFX.Play();
        baked = 2;
        SetSprite(0);
        //arrow.GetComponent<SpriteRenderer>().enabled = true;
        //arrow.GetComponent<progress>().A.SetActive(false);
    }

    private void SetSprite(int state)
    {
        switch (state)
        {
            case 0:
                {
                    arrow.GetComponent<SpriteRenderer>().enabled = true;
                    arrow.GetComponent<progress>().A.SetActive(false);
                    arrow.GetComponent<progress>().clock.SetActive(false);
                    break;
                }
            case 1:
                {
                    arrow.GetComponent<SpriteRenderer>().enabled = false;
                    arrow.GetComponent<progress>().A.SetActive(true);
                    arrow.GetComponent<progress>().clock.SetActive(false);
                    break;
                }
            case 2:
                {
                    arrow.GetComponent<SpriteRenderer>().enabled = false;
                    arrow.GetComponent<progress>().A.SetActive(false);
                    arrow.GetComponent<progress>().clock.SetActive(true);
                    break;
                }
            default:
                break;
        }
    }
}
