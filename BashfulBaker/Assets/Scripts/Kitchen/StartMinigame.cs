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
    private Animator glow;
    private SpriteRenderer spriteRend;
    private Sprite basicOven;
	private float startTime, endTime, smokeTime, burnTime;
	private ParticleSystem ps;

    public AudioSource timerSFX, chimeSFX;
    /// <summary>
    /// Used to determine if the player should be invisible in the minigame.
    /// </summary>
    public bool makePlayerInvisible;

    public Enums.CookingStationMinigame station;

    public static Dish ovenDish;

    private int ovenCookingTime = 10;

    private void Awake()
    {
        if (minigame == "Oven")
        {
            ps = this.gameObject.transform.Find("Smoke").gameObject.GetComponent<ParticleSystem>();
			ps.Stop();
            glow = GetComponent<Animator>();
            glow.enabled = true;
            spriteRend = GetComponent<SpriteRenderer>();
            basicOven = spriteRend.sprite;
			baked = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Assets.Scripts.GameInformation.Game.IsMenuUp) return;

        if (Game.TutorialCompleted == false)
        {
            if (collision.GetComponent<PlayerMovement>().currentStep == thisStep)
            {
                if (baked != 1)
                {
                    //arrow.GetComponent<SpriteRenderer>().enabled = false;
                    //arrow.GetComponent<progress>().A.SetActive(true);
                    SetSprite(1);
                }

                if (InputControls.APressed)
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
                    if (minigame == "Oven")
                    {
                        if (baked == 0)
                        {
                            baked = 1;
                            SetSprite(2);
                            timerSFX.loop = true;
                            timerSFX.Play();

                            ovenDish = (Dish)Game.Player.activeItem;
                            Game.Player.dishesInventory.Remove(Game.Player.activeItem);
                            Game.Player.removeActiveItem();
                            Game.Player.updateHeldItemSprite();
                            
							startTime = (float)Game.PhaseTimer.currentTime;
							endTime = startTime + ovenCookingTime;
							smokeTime = endTime + 5;
							burnTime = smokeTime + 10;

                            ps.Play();
                            glow.enabled = true;
                        }
                        else if (baked == 2) //The dish has been baked
                        {
                            SetSprite(0);
                            collision.GetComponent<PlayerMovement>().NextStep();
                            ovenDish.currentDishState = Enums.DishState.Baked;
                            Game.Player.dishesInventory.Add(ovenDish);
                            Game.Player.resetActiveDishFromMenu();

                            ovenDish = null;
							baked = 0;
                        }
                        return;
                    }

                    if (Game.Player.activeItem is Dish)
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
                        Game.HUD.showOnlyTimer();
                        startTransition();
                        //SceneManager.LoadScene(minigame);
                        playMinigame(d);
                    }
                }

            }
        }
        else
        {
            if (baked != 1)
            {
                //arrow.GetComponent<SpriteRenderer>().enabled = false;
                //arrow.GetComponent<progress>().A.SetActive(true);
                SetSprite(1);
            }

            if (InputControls.APressed)
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
                if (minigame == "Oven")
                {
                    if (baked == 0 && Game.Player.activeItem!=null&& ovenDish==null)
                    {
                        if ((Game.Player.activeItem as Dish).currentDishState != Enums.DishState.Prepped) return;
                        baked = 1;
                        SetSprite(2);
                        timerSFX.Play();

                        ovenDish = (Dish)Game.Player.activeItem;
                        Game.Player.dishesInventory.Remove(Game.Player.activeItem);
                        Game.Player.removeActiveItem();
                        Game.Player.updateHeldItemSprite();
                        Game.HUD.InventoryHUD.updateDishes();
                        
						startTime = (float)Game.PhaseTimer.currentTime;
						endTime = startTime + ovenCookingTime;
						smokeTime = endTime + 5;
						burnTime = smokeTime + 10;

                        ps.Play();
                        glow.enabled = true;

                    }
                    else if(baked == 0 && ovenDish != null)
                    {
                        //collision.GetComponent<PlayerMovement>().NextStep();
                        ovenDish.currentDishState = Enums.DishState.Baked;
                        ovenDish.Update();
                        Game.Player.dishesInventory.Add(ovenDish);
                        Game.HUD.InventoryHUD.updateDishes();
                        Game.Player.resetActiveDishFromMenu();
                        Game.HUD.InventoryHUD.updateDishes();
                        ovenDish.loadSprite();

                        ovenDish = null;
                    }
                    else if (baked == 2) //The dish has been baked
                    {
                        SetSprite(0);
                        //collision.GetComponent<PlayerMovement>().NextStep();
                        
                        Game.Player.dishesInventory.Add(ovenDish);
                        Game.HUD.InventoryHUD.updateDishes();
                        Game.Player.resetActiveDishFromMenu();
                        ovenDish.loadSprite();

                        ovenDish = null;
						baked = 0;
                    }
                    return;
                }

                if (Game.Player.activeItem is Dish)
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
                    if(playMinigame(d)==false) return;
                    SetSprite(0);
                    //collision.GetComponent<PlayerMovement>().NextStep();
                    Game.HUD.showOnlyTimer();
                    //SceneManager.LoadScene(minigame);
                }
            }
        }

    }

    /// <summary>
    /// Starts the scene transition for the fade out sequence.
    /// </summary>
    private void startTransition()
    {
        ScreenTransitions.StartSceneTransition(.5f, minigame, Color.black, ScreenTransitions.TransitionState.FadeOut, new VoidDelegate(finishedTransition));
    }
    /// <summary>
    /// Finish the scene fade out transition and loads the scene.
    /// </summary>
    private void finishedTransition()
    {
        Game.HUD.showHUD = false;
        Game.Player.setSpriteVisibility(Enums.Visibility.Invisible);
        SceneManager.LoadScene(minigame);
        ScreenTransitions.PrepareForSceneFadeIn(.5f, Color.black);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //arrow.GetComponent<SpriteRenderer>().enabled = true;
        //arrow.GetComponent<progress>().A.SetActive(false);

        if (minigame != "Oven" || baked != 1) SetSprite(0);
    }

    /// <summary>
    /// Checks a dish and tries to play the appropriate minigame if possible.
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    private bool playMinigame(Dish d)
    {
        if (d.currentDishState == Enums.DishState.Ingredients && station == Enums.CookingStationMinigame.MixingBowl)
        {
            d.currentDishState = Enums.DishState.Mixed;
            startTransition();
            return true;
        }
        if (d.currentDishState == Enums.DishState.Mixed && station == Enums.CookingStationMinigame.RollingStation)
        {
            d.currentDishState = Enums.DishState.Prepped;
            startTransition();
            return true;
        }
        if (d.currentDishState == Enums.DishState.Prepped && station == Enums.CookingStationMinigame.Oven)
        {
            d.currentDishState = Enums.DishState.Baked;
            return true;
        }
        if (d.currentDishState == Enums.DishState.Baked && station == Enums.CookingStationMinigame.PackingStation)
        {
            d.currentDishState = Enums.DishState.Packaged;
            startTransition();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets a verb for the cooking station.
    /// </summary>
    /// <returns></returns>
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

	private void Update()
	{
		if(minigame == "Oven")
		{
			switch (baked)
			{
				case 0:
				{ 
					break;
				}
				case 1:
				{
					if((float)Game.PhaseTimer.currentTime >= endTime)
					{
						baked = 2;
						ovenDish.currentDishState = Enums.DishState.Baked;
                        ovenDish.Update();
					}
					break;
				}
				case 2:
				{
					if((float)Game.PhaseTimer.currentTime >= smokeTime)
					{
						//start smoke
						var emission = ps.emission;
						emission.rate = Mathf.Lerp(1, 10, ((float)Game.PhaseTimer.currentTime - startTime) / (endTime - startTime));
					}
					if((float)Game.PhaseTimer.currentTime >= burnTime)
					{
						//burn cookie
						ovenDish.currentDishState = Enums.DishState.Burnt;
                        ovenDish.Update();
					}
					break;
				}
				default:
				{
					break;
				}
			}
		}
	}

    /// <summary>
    /// Bakes a thing in the oven.
    /// </summary>
    private void Bake()
    {
        timerSFX.Stop();
        chimeSFX.Play();
        baked = 2;
        SetSprite(0);
        //this.gameObject.transform.Find("Smoke").gameObject.GetComponent<ParticleSystem>().Stop();
        // glow.enabled = false;
        spriteRend.sprite = basicOven;
        //arrow.GetComponent<SpriteRenderer>().enabled = true;
        //arrow.GetComponent<progress>().A.SetActive(false);
    }

    /// <summary>
    /// Sets the sprite for the arrow in the tutorial.
    /// </summary>
    /// <param name="state"></param>
    private void SetSprite(int state)
    {
        if (arrow == null) return; //In case it isn't set and we aren't baby gating in the tutorial anymore.
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
