using Assets.Scripts.Cooking.Recipes;
using Assets.Scripts.GameInput;
using Assets.Scripts.Items;
using Assets.Scripts.Menus;
using Assets.Scripts.Player;
using Assets.Scripts.QuestSystem;
using Assets.Scripts.QuestSystem.Quests;
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
                if (Serializer.JSONSerializer == null) Serializer.JSONSerializer = new Utilities.Serialization.Serializer();
                if (Cooking.Recipes.CookBook.CookingRecipes == null) Cooking.Recipes.CookBook.CookingRecipes = new CookBook();
                if (QuestSystem.QuestManager.Quests == null) QuestSystem.QuestManager.Quests = new QuestManager();
                if (player == null) player = new PlayerInfo();

                gameLoaded = true;
            }

            if (MouseCursor == null)
            {
                string path = Path.Combine(Path.Combine("Prefabs", "Misc"), "GameCursor");
                _MouseCursor = Instantiate((GameObject)Resources.Load(path, typeof(GameObject))).GetComponent<GameCursor>();
                GameObject.DontDestroyOnLoad(_MouseCursor);
            }

            if (SoundManager == null)
            {
                

                string soundManagerPath = Path.Combine(Path.Combine("Prefabs", "Misc"), "SoundManager");
                Instantiate((GameObject)Resources.Load(soundManagerPath, typeof(GameObject)));
            }

            setUpScene();

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        }

        private static void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            setUpScene();
        }

        static void setUpScene()
        {

            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                Menus.Menu.Instantiate<MainMenu>();
            }

            if (SceneManager.GetActiveScene().name == "preloadScene")
            {
                string path = Path.Combine("Prefabs", "Player");
                Player.gameObject = Instantiate((GameObject)Resources.Load(path, typeof(GameObject)));
                Player.gameObject.transform.position = new Vector3(-1.3f, .25f, 0);
                DontDestroyOnLoad(Player.gameObject);

                SceneManager.LoadScene("Kitchen");
                Debug.Log("Loading kitchen scene from the Game.cs script!");
            }

            Debug.Log(SceneManager.GetActiveScene().name);
        }

#endif

        // Various other things follow...

    }
}
