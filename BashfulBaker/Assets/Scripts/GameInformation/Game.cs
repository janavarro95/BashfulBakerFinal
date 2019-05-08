using Assets.Scripts.Cooking.Recipes;
using Assets.Scripts.GameInput;
using Assets.Scripts.Items;
using Assets.Scripts.Kitchen;
using Assets.Scripts.Menus;
using Assets.Scripts.Menus.HUDS;
using Assets.Scripts.Player;
using Assets.Scripts.QuestSystem;
using Assets.Scripts.QuestSystem.Quests;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Menu = Assets.Scripts.Menus.Menu;

namespace Assets.Scripts.GameInformation
{
    /// <summary>
    /// https://forum.unity.com/threads/best-way-to-initialize-static-class-not-attached-to-a-gameobject.402835/
    /// 
    /// Used to control/initialize all static variables and keep track of things we need to universally know.
    /// </summary>
    #if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
    #endif
    public class Game : MonoBehaviour
    {
        /// <summary>
        /// Checks to see if the game and it's internal systems have been loaded or not.
        /// </summary>
        private static bool gameLoaded;
        /// <summary>
        /// The information for the player that doesn't need to be seen on a MonoBehavior script.
        /// </summary>
        private static Assets.Scripts.Player.PlayerInfo player;

        /// <summary>
        /// Holds all of the information we need to keep track of on the player.
        /// </summary>
        public static Player.PlayerInfo Player
        {
            get
            {
                return player;
            }
        }

        private static Menu _Menu;

        public static Enums.GameState GameState;

        private static GameCursor _MouseCursor;

        public static GameCursor MouseCursor
        {
            get
            {
                return _MouseCursor;
            }
            set
            {
                _MouseCursor = value;
            }
        }

        public static Vector2 MousePosition
        {
            get
            {
                return _MouseCursor.gameObject.transform.position;
            }
        }

        public static Menu Menu
        {
            get
            {
                return _Menu;
            }
            set
            {
                _Menu=value;
            }
        }

        public static bool IsMenuUp
        {
            get
            {
                return Menu != null;
            }
        }


        /// <summary>
        /// Checks to see if the game and it's internal systems have been loaded or not.
        /// </summary>
        public static bool GameLoaded
        {
            get
            {
                return gameLoaded;
            }        
        }

        public static Serializer Serializer
        {
            get
            {
                return Assets.Scripts.Utilities.Serialization.Serializer.JSONSerializer;
            }
        }

        public static QuestManager QuestManager
        {
            get
            {
                return Assets.Scripts.QuestSystem.QuestManager.Quests;
            }
        }

        public static CookBook CookBook
        {
            get
            {
                return Cooking.Recipes.CookBook.CookingRecipes;
            }
        }

        public static GameSoundManager SoundManager;

        public static GameOptions Options;

        public static GameHUD HUD;

        public static Utilities.Timers.DeltaTimer PhaseTimer;

        public static Content.ContentManager ContentManager
        {
            get
            {
                return Content.ContentManager.Instance;
            }
        }

        public static ScreenTransitions CurrentTransition;
        public static bool IsScreenTransitionHappening
        {
            get
            {
                if (CurrentTransition == null) return false;
                else
                {
                    return CurrentTransition.IsTransitioning;
                }
            }
        }

        public static Pantry Pantry;
        public static bool TutorialCompleted;

        public static DialogueManager DialogueManager;

        public static SoundEffects SoundEffects;


        public static int CurrentDayNumber;
        public static bool IngredientsAddedForPlayer;
        public static bool TalkedtoSully;
        
        public struct IngSource
        {
            public String name;
            public int max;
            public int current;

            public IngSource(String Name, int Max)
            {
                name = Name;
                max = Max;
                current = 0;
            }
        }

        public static List<IngSource> Sources = new List<IngSource>();

        public static Dictionary<int, int> NumberOfTimesCaught;

        public static Dictionary<int, bool> DaysUnlocked;

        public static Dictionary<string, string> GuardsFed;

        // Notice that these methods are static! This is key!
        #if UNITY_EDITOR
        static Game()
        {
            // Outside of the editor, this doesn't get called, and RuntimeInitializeOnLoad does NOT
            // support calling constructors. Therefor, we cannot assume this will always get called.
            // This is a good opportunity to do editor-specific things if necessary.
            
            Initialize();
        }
        #endif

        #if UNITY_STANDALONE
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        #endif
        static void Initialize()
        {
        }

#if UNITY_STANDALONE
        /// <summary>
        /// Called once the game is loaded into play mode.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void InitializeAfterLoad()
        {

            if (GameLoaded == false)
            {


                //Debug.Log("SET UP GAME!");


                if (ContentManager == null)
                {
                    Content.ContentManager.Instance = new Content.ContentManager();
                }

                if (Serializer.JSONSerializer == null) Serializer.JSONSerializer = new Utilities.Serialization.Serializer();
                if (Cooking.Recipes.CookBook.CookingRecipes == null) Cooking.Recipes.CookBook.CookingRecipes = new CookBook();
                if (QuestSystem.QuestManager.Quests == null) QuestSystem.QuestManager.Quests = new QuestManager();
                if (player == null) player = new PlayerInfo();

                gameLoaded = true;

                if (SoundManager == null)
                {
                    string soundManagerPath = Path.Combine(Path.Combine("Prefabs", "Misc"), "SoundManager");
                    GameObject obj=Instantiate((GameObject)Resources.Load(soundManagerPath, typeof(GameObject)));
                    SoundManager = obj.GetComponent<GameSoundManager>();
                }

                if (SoundEffects == null)
                {
                    string soundManagerPath = Path.Combine(Path.Combine("Prefabs", "Misc"), "SoundEffects");
                    GameObject obj = Instantiate((GameObject)Resources.Load(soundManagerPath, typeof(GameObject)));
                    SoundEffects = obj.GetComponent<SoundEffects>();
                    SoundEffects.gameObject.transform.parent = SoundManager.gameObject.transform;
                }

                if (Options == null)
                {
                    Options = new GameOptions();
                }

                if (Pantry == null)
                {
                    Pantry = new Pantry();
                }

                if (IngredientsAddedForPlayer == false)
                {
                    Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.ChocolateChips));
                    Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.MintChips));
                    Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.Pecans));
                    Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.Raisins));
                    Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.Carrots));
                    Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.Strawberries));
                    IngredientsAddedForPlayer = true;
                }

                if (DaysUnlocked == null)
                {
                    DaysUnlocked = new Dictionary<int, bool>();
                    DaysUnlocked.Add(0, true);
                    DaysUnlocked.Add(1, true);
                    DaysUnlocked.Add(2, true);
                    DaysUnlocked.Add(3, false);
                    DaysUnlocked.Add(4, false);
                }

                setUpScene();

                SceneManager.sceneLoaded += SceneManager_sceneLoaded;

            }

            /*
            if (MouseCursor == null)
            {
               // string path = Path.Combine(Path.Combine("Prefabs", "Misc"), "GameCursor");
               // _MouseCursor = Instantiate((GameObject)Resources.Load(path, typeof(GameObject))).GetComponent<GameCursor>();
               // GameObject.DontDestroyOnLoad(_MouseCursor);
            }
            */
        }

        private static void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            setUpScene();
        }

        public static void returnToMainMenu()
        {

            DestroyAllForGameCleanUp();

            SceneManager.LoadScene("MainMenu");
            InitializeAfterLoad();
            setUpScene();
            

        }
        public static void returnToDailySelectMenu()
        {

            DestroyAllForGameCleanUp();

            SceneManager.LoadScene("DaySelectMenu");
            InitializeAfterLoad();
            setUpScene();
        }

        private static void DestroyAllForGameCleanUp()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;

            //Unload all info here.
            gameLoaded = false;
            Serializer.JSONSerializer = null;
            QuestManager.Quests = null;

            Player.setSpriteVisibility(Enums.Visibility.Invisible);
            //Destroy(Player.gameObject);
            //player = null;

            Destroy(SoundManager.gameObject);
            SoundManager = null;
            Options = null;
            Pantry = null;

            //Game.Menu.exitMenu();

            Game.PhaseTimer = null;

            Game.Player.dishesInventory.actualItems.Clear();
            Game.player.removeActiveItem();


            Game.HUD.showInventory = false;
            Game.HUD.showTimer = false;
            Game.HUD.showQuests = false;
            Game.HUD.showSpecialIngredients = false;

            StartMinigame.ovenDish = null;

            if (NumberOfTimesCaught != null)
            {

                foreach (int i in NumberOfTimesCaught.Keys)
                {
                    NumberOfTimesCaught[i] = 0;
                }
            }
            if(GuardsFed!=null)GuardsFed.Clear();

            IngredientsAddedForPlayer = false;

        }

        /// <summary>
        /// Sets up all of the necessary components when a scene changes
        /// </summary>
        public static void setUpScene()
        {

            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                Menus.Menu.Instantiate<MainMenu>();
            }

            if (SceneManager.GetActiveScene().name == "preloadScene")
            {
                if (GameObject.Find("Player(Clone)")==null)
                {
                    string path = Path.Combine("Prefabs", "Player");
                    Player.gameObject = Instantiate((GameObject)Resources.Load(path, typeof(GameObject)));
                    Player.gameObject.transform.position = new Vector3(-3.06971f, -9.5f, 0);
                    DontDestroyOnLoad(Player.gameObject);

                    string HUDPath = Path.Combine(Path.Combine("Prefabs", "HUDS"), "GameHUD");
                    //Debug.Log(HUDPath);
                    Instantiate((GameObject)Resources.Load(HUDPath, typeof(GameObject))); //Instantiate game hud;
                    //Debug.Log("Loading kitchen scene from the Game.cs script!");

                    //StartNewTimerPhase(2, 0);

                    if (Game.TutorialCompleted == false)
                    {
                        (HUD as GameHUD).showInventory = false;
                    }
                }

                SceneManager.LoadScene("DaySelectMenu");
            }

            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                
                //Josh's testing playground.
                string HUDPath = Path.Combine(Path.Combine("Prefabs", "HUDS"), "GameHUD");
                
                Instantiate((GameObject)Resources.Load(HUDPath, typeof(GameObject))); //Instantiate game hud;

                //Game.Player.specialIngredientsInventory.Add(new SpecialIngredient("Chocolate Chip"));
                Game.Player.dishesInventory.Add(new Dish(Enums.Dishes.ChocolateChipCookies));
                Game.Player.dishesInventory.Add(new Dish(Enums.Dishes.MintChipCookies));
                Game.Player.dishesInventory.Add(new Dish(Enums.Dishes.OatmealRaisinCookies));
                Game.Player.dishesInventory.Add(new Dish(Enums.Dishes.PecanCookies));

                Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.ChocolateChips));
                Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.MintChips));
                Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.Pecans));
                Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.Raisins));
                Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.Carrots));
                Game.player.specialIngredientsInventory.Add(new SpecialIngredient(Enums.SpecialIngredients.Strawberries));

            }

            if (SceneManager.GetActiveScene().name == "Kitchen")
            {
                try
                {
                    Player.arrowDirection.setTargetObject(GameObject.Find("OutsideTarget").transform.gameObject);

                }
                catch(Exception err)
                {
                    return;
                }
                if (Game.Player.dishesInventory.Contains("Chocolate Chip Cookies"))
                {
                    if ((Game.player.dishesInventory.getItem("Chocolate Chip Cookies") as Dish).currentDishState == Enums.DishState.Packaged)
                    {
                        Player.arrowDirection.gameObject.SetActive(true);
                    }
                    else
                    {
                        Player.arrowDirection.gameObject.SetActive(false);
                    }
                }
                else
                {
                    Player.arrowDirection.gameObject.SetActive(false);
                }

                //
            }
            else
            {
                try
                {
                    Player.arrowDirection.gameObject.SetActive(false);
                }
                catch(Exception err)
                {

                }
            }


            if (SceneManager.GetActiveScene().name == "KitchenDay2")
            {
                //Debug.Log("REMOVE THE COUNTERS!");
                //GameObject.Find("backCounter 1").SetActive(false);
                GameObject.Find("backCounter 1 (1)").SetActive(false);
            }

            if (SceneManager.GetActiveScene().name == "Neighborhood")
            {
                GameObject obj=GameObject.Find("DeliveryZones");
                if(CurrentDayNumber==0 || CurrentDayNumber == 1)
                {
                    obj.transform.Find("Day_1").gameObject.SetActive(true);
                    obj.transform.Find("Day_2").gameObject.SetActive(false);
                    obj.transform.Find("Day_3").gameObject.SetActive(false);
                    obj.transform.Find("Day_4").gameObject.SetActive(false);
                }
            
                if (CurrentDayNumber == 2)
                {
                    obj.transform.Find("Day_1").gameObject.SetActive(false);
                    obj.transform.Find("Day_2").gameObject.SetActive(true);
                    obj.transform.Find("Day_3").gameObject.SetActive(false);
                    obj.transform.Find("Day_4").gameObject.SetActive(false);
            }
                if (CurrentDayNumber == 3)
                {
                    obj.transform.Find("Day_1").gameObject.SetActive(false);
                    obj.transform.Find("Day_2").gameObject.SetActive(false);
                    obj.transform.Find("Day_3").gameObject.SetActive(true);
                    obj.transform.Find("Day_4").gameObject.SetActive(false);
                }

                if (CurrentDayNumber == 4)
                {
                    obj.transform.Find("Day_1").gameObject.SetActive(false);
                    obj.transform.Find("Day_2").gameObject.SetActive(false);
                    obj.transform.Find("Day_3").gameObject.SetActive(false);
                    obj.transform.Find("Day_4").gameObject.SetActive(true);
                }
            }

            if (ScreenTransitions.shouldFadeInAfterWarp)
            {
                ScreenTransitions.StartSceneTransition(ScreenTransitions.targetFadeInTime, "", ScreenTransitions.lastFadeInColor, ScreenTransitions.TransitionState.FadeIn);
                ScreenTransitions.shouldFadeInAfterWarp = false;
            }

            //Debug.Log(SceneManager.GetActiveScene().name);
        }


        /// <summary>
        /// Starts a new timer phase if there is currently not an active timer.
        /// </summary>
        /// <param name="Minutes"></param>
        /// <param name="Seconds"></param>
        /// <param name="checkForValidity"></param>
        public static void StartNewTimerPhase(int Minutes, int Seconds, bool checkForValidity=false)
        {
            if (checkForValidity == true)
            {
                if (PhaseTimer != null)
                {
                    if (PhaseTimer.state == Enums.TimerState.Ticking)
                    {
                        return;
                    }
                }
            }

            int actualTime = (Minutes * 60) + Seconds;
            PhaseTimer = new Utilities.Timers.DeltaTimer(actualTime, Enums.TimerType.CountDown, false, new Utilities.Delegates.VoidDelegate(phaseTimerRunsOut));
            PhaseTimer.start();
            Game.HUD.showTimer = true;
        }


        private static void phaseTimerRunsOut()
        {
            Game.DialogueManager.StartDialogue(new Dialogue("Guard",new List<string>()
                {
                    "Oh man it's getting pretty dark.",
                    "I guess I won't have any more time to do my deliveries..."
                }.ToArray()));

            ScreenTransitions.StartSceneTransition(5f, "EndOfDay", Color.black, ScreenTransitions.TransitionState.FadeOut);
        }

        public static void QuitGame()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
            Application.Quit();
        }

        /// <summary>
        /// Loads the correct kitchen scene when the minigame finishes.
        /// </summary>
        public static void LoadCorrectKitchenScene()
        {
            if(CurrentDayNumber==0 || CurrentDayNumber == 1)
            {
                SceneManager.LoadScene("Kitchen");
            }
            else if (CurrentDayNumber == 2)
            {
                SceneManager.LoadScene("KitchenDay2");
            }
            else if (CurrentDayNumber == 3)
            {
                SceneManager.LoadScene("KitchenDay3");
            }
            else if (CurrentDayNumber == 4)
            {
                SceneManager.LoadScene("KitchenDay4");
            }
        }


        /// <summary>
        /// Updates the index for how many times the player was caught by the guard today.
        /// </summary>
        public static void CaughtByGuard(string ID,string DishName)
        {
            if (GuardsFed == null) GuardsFed = new Dictionary<string, string>();
            if (NumberOfTimesCaught == null) NumberOfTimesCaught = new Dictionary<int, int>();

            if (NumberOfTimesCaught.ContainsKey(CurrentDayNumber))
            {
                NumberOfTimesCaught[CurrentDayNumber]++;
            }
            else
            {
                NumberOfTimesCaught.Add(CurrentDayNumber, 1);
            }

            GuardsFed.Add(ID, DishName);
        }

        /// <summary>
        /// Checks if a guard has been fed today.
        /// </summary>
        /// <param name="ID">The unique ID of the guard.</param>
        /// <returns></returns>
        public static bool HasGuardBeenFed(string ID)
        {
            if (GuardsFed == null) return false;
            else
            {
                return GuardsFed.ContainsKey(ID);
            }
        }

        /// <summary>
        /// Unlocks the next day.
        /// </summary>
        public static void UnlockDay()
        {
            try
            {
                DaysUnlocked[CurrentDayNumber+1] = true;
            }
            catch(Exception err)
            {

            }
        }

#endif

        // Various other things follow...

    }
}
