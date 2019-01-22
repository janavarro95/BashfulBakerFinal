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
                this.gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * mouseMovementSpeed;
            }
            else
            {
                oldMousePos = vec;
                this.gameObject.transform.position = vec;
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
    }
}
