﻿using Assets.Scripts.GameInformation;
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
    public class GameCursor:MonoBehaviour
    {
        private Vector3 oldMousePos;

        public float mouseMovementSpeed = 0.05f;

        public bool movedByCursor;

        void Start()
        {
            oldMousePos = Camera.main.ScreenToWorldPoint((Vector2)UnityEngine.Input.mousePosition);
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

        /// <summary>
        /// Sets the cursors position.
        /// </summary>
        /// <param name="position"></param>
        public void setCursorPosition(Vector2 position)
        {
            this.gameObject.transform.position = position;
            movedByCursor = false;
        }

        /// <summary>
        /// Sets the cursor's position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setCursorPosition(float x, float y)
        {
            this.gameObject.transform.position = new Vector3(x,y,0);
            movedByCursor = false;
        }

        /// <summary>
        /// Checks to see if the game's cursor intersects with the ui element.
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public static bool CursorIntersectsRect(MonoBehaviour behavior)
        {
            if (UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(behavior.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(Game.MousePosition)))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Checks to see if the computers mouse cursor intersects with the ui element.
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public static bool MouseIntersectsRect(MonoBehaviour behavior)
        {
            if (UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(behavior.GetComponent<RectTransform>(), Input.mousePosition))
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
            if (GameCursor.CursorIntersectsRect(behavior) && Game.MouseCursor.movedByCursor == false)
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

    }
}