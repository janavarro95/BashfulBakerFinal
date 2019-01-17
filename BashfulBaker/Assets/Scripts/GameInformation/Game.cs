using Assets.Scripts.Utilities.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
        public static Serializer Serializer
        {
            get
            {
                return Assets.Scripts.Utilities.Serialization.Serializer.JSONSerializer;
            }
        }

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
            // I do not remember if the pre-processor check is necessary, but I do
            // know that this code will not get called unless you have the constructor like above.

            // Anyway, put whatever initialization code you want here.

            Assets.Scripts.Utilities.Serialization.Serializer.JSONSerializer = new Utilities.Serialization.Serializer();
            Cooking.Recipes.CookBook.Initialize();
        }

        // Various other things follow...

    }
}
