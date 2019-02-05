using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GameInput
{
    /// <summary>
    /// TODO:
    /// Change this to move cursor on right stick.
    /// </summary>
    public class GameCursorMenu:GameCursor,ICanvasRaycastFilter
    {

        private RectTransform rect;

        void Start()
        {
            oldMousePos = Camera.main.ScreenToWorldPoint((Vector2)UnityEngine.Input.mousePosition);
            timer = new Utilities.Timers.DeltaTimer(5, Enums.TimerType.CountDown, false,new Utilities.Delegates.VoidDelegate(makeInvisible));
            timer.start();
            rect = this.gameObject.GetComponent<RectTransform>();
            Game.MouseCursor = this;
        }

        void Update()
        {
            timer.tick();
            setVisibility();
            Vector2 vec = UnityEngine.Input.mousePosition;
            if (vec.Equals(oldMousePos))
            {
                Vector3 delta= new Vector3(GameInput.InputControls.RightJoystickHorizontal, GameInput.InputControls.RightJoystickVertical, 0) * mouseMovementSpeed;
                this.rect.position += delta;
                if (delta.x == 0 && delta.y == 0) return;
                if (Mathf.Abs(delta.x) > 0 || Mathf.Abs(delta.y) > 0) timer.restart();
                movedByCursor = false;
                isVisible = true;
            }
            else
            {
                if (Mathf.Abs(vec.x - oldMousePos.x) < .001 && Mathf.Abs(vec.y - oldMousePos.y) < .001) return; //stop random mouse sliding.
                oldMousePos = vec;
                this.rect.position = vec;
                movedByCursor = true;
                timer.restart();
                isVisible = true;
            }
        }

        /// <summary>
        /// https://forum.unity.com/threads/ignoring-layers-in-unitygui.272524/
        /// </summary>
        /// <param name="screenPoint"></param>
        /// <param name="eventCamera"></param>
        /// <returns></returns>
        public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            //the raycast will continue, if you want to block then set to true
            return false;
        }

        /// <summary>
        /// Sets the cursors position.
        /// </summary>
        /// <param name="position"></param>
        public void setCursorPosition(Vector2 position)
        {
            this.rect.position = position;
            timer.restart();
            movedByCursor = false;
        }

        /// <summary>
        /// Sets the cursor's position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setCursorPosition(float x, float y)
        {
            this.rect.position = new Vector3(x,y,0);
            timer.restart();
            movedByCursor = false;
        }

        /// <summary>
        /// Checks to see if the game's cursor intersects with the ui element.
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public static new bool CursorIntersectsRect(MonoBehaviour behavior) 
        {
            if (GetWorldSapceRect(behavior.GetComponent<RectTransform>()).Overlaps(GetWorldSapceRect(Game.Menu.menuCursor.rect)))
            {
                return true;
            }
            return false;
        }

        static Rect GetWorldSapceRect(RectTransform rt)
        {
            var r = rt.rect;
            r.center = rt.TransformPoint(r.center);
            r.size = rt.TransformVector(r.size);
            return r;
        }

        /// <summary>
        /// Checks to see if the computers mouse cursor intersects with the ui element.
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public static bool MouseIntersectsRect(MonoBehaviour behavior)
        {
            if (UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(behavior.GetComponent<RectTransform>(), Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the game's cursor can interact with the UI Element.
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public static bool CanCursorInteract(MonoBehaviour behavior)
        {
            if (GameCursorMenu.CursorIntersectsRect(behavior) && Game.MouseCursor.movedByCursor == false)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Simulates a mouse press for the cursor if the A button is pressed and the cursor intersects the mono behavior.
        /// </summary>
        /// <param name="behavior"></param>
        /// <param name="useHardwareMouse"></param>
        /// <returns></returns>
        public static new bool SimulateMousePress(MonoBehaviour behavior,bool useHardwareMouse=false)
        {
            if (behavior == null)
            {
                Debug.Log("BEHAVIOR IS NULL");
            }
            if (GameCursorMenu.CursorIntersectsRect(behavior) && Game.MouseCursor.movedByCursor == false && GameInput.InputControls.APressed)
            {
                return true;
            }
            
            
            else if (GameCursorMenu.MouseIntersectsRect(behavior) && Input.GetMouseButtonDown(0))
            {
                return true;
            }
            else
            {             
                return false;
            }
            
        }

        /// <summary>
        /// Sets the game's cursor to a specific position.
        /// </summary>
        /// <param name="position"></param>
        public static void SetCursorPosition(Vector2 position)
        {
            Game.MouseCursor.setCursorPosition(position);
        }

        /// <summary>
        /// Sets the game's cursor to a specific position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetCursorPosition(float x, float y)
        {
            Game.MouseCursor.setCursorPosition(x,y);
        }

        /// <summary>
        /// Sets the visibility of the mouse.
        /// </summary>
        /// <param name="visible"></param>
        public void setVisibility(bool visible)
        {
            timer.stop();
            isVisible = visible;
        }

        private void setVisibility()
        {
            this.GetComponent<Image>().enabled = isVisible;
        }

        private void makeInvisible()
        {
            isVisible = false;
        }
    }
}
