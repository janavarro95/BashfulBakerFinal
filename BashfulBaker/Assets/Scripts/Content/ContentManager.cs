using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Content
{
    public class ContentManager
    {
        public static ContentManager Instance;


        public ContentManager()
        {
           
        }

        public Sprite loadSprite(string RelativePath)
        {
            return loadSprite(RelativePath, new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f), 16f);
        }

        public Sprite loadSprite(Texture2D texture)
        {
            return loadSprite(texture, new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f), 16f);
        }


        public Sprite loadSprite(string RelativePath, Rect RectInfo, Vector2 Pivots, float PixelsPerUnit)
        {
            Sprite s = Sprite.Create(loadTexture2DFromStreamingAssets(RelativePath), RectInfo, Pivots, PixelsPerUnit);
            s.texture.filterMode = FilterMode.Point; https://docs.unity3d.com/ScriptReference/FilterMode.html
            return s;
        }

        public Sprite loadSprite(Texture2D texture, Rect RectInfo, Vector2 Pivots, float PixelsPerUnit)
        {
            if (texture == null) return null;
            Sprite s = Sprite.Create(texture, RectInfo, Pivots, PixelsPerUnit);
            try
            {
                s.texture.filterMode = FilterMode.Point; https://docs.unity3d.com/ScriptReference/FilterMode.html
                return s;
            }
            catch(Exception err) {
                //Debug.Log(err.ToString());
                //Debug.Log("Texture name:" + texture.name);
                return null;
            }

        }


        /// <summary>
        /// Loads a resource from a given path.
        /// </summary>
        /// <param name="AbsolutePath"></param>
        /// <returns></returns>
        public Texture2D loadTexture2DFromStreamingAssets(string RelativePath)
        {
            string finalPath;
            WWW localFile;

            finalPath = Path.Combine(Application.streamingAssetsPath, RelativePath);

            if (!File.Exists(finalPath))
            {
                //Debug.Log("File: " + Path.GetFileName(finalPath) + " does not exist at the given path! " + finalPath);
                //throw new Exception("File: " + Path.GetFileName(finalPath) + " does not exist at the given path! " + finalPath);
            }

            localFile = new WWW(finalPath);

            if (localFile == null) throw new Exception("LOCAL FILE IS NULL!!!!");


            localFile.texture.filterMode = FilterMode.Point;

            return localFile.texture;
        }

        public Texture2D loadTexture2DFromResources(string RelativePath) {
            Texture2D text = Resources.Load<Texture2D>(RelativePath);
            return text;
        }
    }
}
