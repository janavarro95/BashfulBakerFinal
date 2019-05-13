using Assets.Scripts.Utilities.Delegates;
using Assets.Scripts.Utilities.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Menus.MenuTypes
{
    [System.Serializable]
    public class SlideIn
    {
        GameObject canvas;
        Dictionary<GameObject, Vector3> objs;
        DeltaTimer lerpTimer;

        Vector2 offset;

        VoidDelegate intro;

        public SlideIn(GameObject canvas, float time, VoidDelegate Intro)
        {
            objs = new Dictionary<GameObject, Vector3>();
            offset = new Vector2(Camera.main.pixelWidth,0);
            foreach (Transform t in canvas.transform)
            {
                if (t.gameObject.name == "EventSystem") continue;

                t.gameObject.GetComponent<RectTransform>().position -= (Vector3)offset;
                Vector3 fff = t.gameObject.GetComponent<RectTransform>().position;
                objs.Add(t.gameObject, fff);
                t.gameObject.GetComponent<RectTransform>().position -=(Vector3)offset;
            }
            intro = Intro;
           
            lerpTimer = new DeltaTimer(time, Enums.TimerType.CountUp, false, intro);
            lerpTimer.start();
        }

        public bool Update()
        {
            lerpTimer.Update();
            foreach(KeyValuePair<GameObject,Vector3> pair in objs)
            {
                pair.Key.GetComponent<RectTransform>().position = Vector3.Lerp(pair.Value, pair.Value + (Vector3)offset, (float)(lerpTimer.currentTime / lerpTimer.maxTime));
            }
            if (lerpTimer.state == Enums.TimerState.Finished) return true;
            else return false;
        }

    }
}
