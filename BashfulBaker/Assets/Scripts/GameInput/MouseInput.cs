using System;
using System.Runtime.InteropServices;
using UnityEngine;

//https://stackoverflow.com/questions/2416748/how-do-you-simulate-mouse-click-in-c

namespace Assets.Scripts.GameInput {
    public class MouseInput
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static int MouseMovementSpeed=10;

        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void SetCursorPosition(Vector2 vec)
        {
            SetCursorPos((int)vec.x, (int)vec.y);
        }

        public static void SetCursorPosition(Vector3 vec)
        {
            SetCursorPos((int)vec.x, (int)vec.y);
        }

        public static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static void LeftClick()
        {
            MouseEvent(MouseEventFlags.LeftDown);
        }

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        /// <summary>
        /// Move the cursor relatively to the input axis for movement * a speed.
        /// </summary>
        public static void MoveCursorRelatively()
        {
            Assets.Scripts.GameInput.MouseInput.MoveCursorRelatively(new Vector2(Input.GetAxis("Horizontal"),-1*Input.GetAxis("Vertical")) * MouseMovementSpeed);
        }

        /// <summary>
        /// Move the cursor relatively to the input axis for movement * a speed.
        /// </summary>
        public static void MoveCursorRelatively(float speed)
        {
            Assets.Scripts.GameInput.MouseInput.MoveCursorRelatively(new Vector2(Input.GetAxis("Horizontal"), -1 * Input.GetAxis("Vertical")) * speed);
        }

        public static void MoveCursorRelatively(int x, int y)
        {
            MousePoint point = GetCursorPosition();
            SetCursorPosition(point.X + x, point.Y + y);
        }

        public static void MoveCursorRelatively(Vector2 vec)
        {
            MoveCursorRelatively((int)vec.x, (int)vec.y);
        }

        public static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

    }
}