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
    public class GameCursor:MonoBehaviour
    {
        private Vector3 oldMousePos;
        private Vector3 newMousePos;

        public float mouseMovementSpeed = 0.05f;

        public bool movedByCursor;

        void Start()
        {
            oldMousePos = Camera.main.ScreenToWorldPoint((Vector2)UnityEngine.Input.mousePosition);
            newMousePos = oldMousePos;
        }

        void Update()
        {
            Vector2 vec = Camera.main.ScreenToWorldPoint((Vector2)UnityEngine.Input.mousePosition);
            if (vec.Equals(oldMousePos))
            {
                Vector3 delta= new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * mouseMovementSpeed;
                this.gameObject.transform.position += delta;
                if (delta.x == 0 && delta.y == 0) return; 
                movedByCursor = false;
            }
            else
            {
                oldMousePos = vec;
                this.gameObject.transform.position = vec;
                movedByCursor = true;
            }
        }

        public static bool CursorIntersectsRect(MonoBehaviour behavior)
        {
            if (UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(behavior.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(Game.MousePosition)))
            {
                return true;
            }
            return false;
        }

        public static bool MouseIntersectsRect(MonoBehaviour behavior)
        {
            if (UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(behavior.GetComponent<RectTransform>(), Input.mousePosition))
            {
                return true;
            }
            return false;
        }

        public static bool CanCursorInteract(MonoBehaviour behavior)
        {
            if (GameCursor.CursorIntersectsRect(behavior) && Game.MouseCursor.movedByCursor == false)
            {
                return true;
            }
            return false;
        }

        public static bool CanMouseInteract(MonoBehaviour behavior)
        {
            if (GameCursor.MouseIntersectsRect(behavior))
            {
                return true;
            }
            return false;
        }

        public static bool SimulateMousePress(MonoBehaviour behavior,bool useHardwareMouse=false)
        {

            if (GameCursor.CursorIntersectsRect(behavior) && Game.MouseCursor.movedByCursor == false && GameInput.InputControls.APressed)
            {
                return true;
            }            
            else if (GameCursor.MouseIntersectsRect(behavior) && Input.GetMouseButtonDown(0))
            {
                return true;
            }
            else
            {             
                return false;
            }
        }
    }
}
