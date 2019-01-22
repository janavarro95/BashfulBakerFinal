using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameInput
{
    /// <summary>
    /// Checks input for Unity.
    /// </summary>
    public class InputControls : MonoBehaviour
    {

        /// <summary>
        /// The types of input controllers supported.
        /// </summary>
        public enum ControllerType
        {
            Keyboard,
            XBox360,
            DualShock
        }

        /// <summary>
        /// Property to check if the "A" button is pressed.
        /// </summary>
        public static bool APressed
        {
            get
            {
                ControllerType controller = GetControllerType();
                if (controller == ControllerType.DualShock)
                {
                    return Input.GetButtonDown("Fire1");
                }
                else if (controller == ControllerType.XBox360)
                {
                    if(OSChecker.OS== Enums.OperatingSystem.Mac)
                    {
                        return Input.GetButtonDown("Fire1_Mac");
                    }
                    return Input.GetButtonDown("Fire1");
                }
                else
                {
                    return Input.GetButtonDown("Fire1");
                }
            }
        }

        /// <summary>
        /// Property to check if the "B" button is pressed.
        /// </summary>
        public static bool BPressed
        {
            get
            {
                if (GetControllerType() == ControllerType.DualShock)
                {
                    return Input.GetButtonDown("Fire2");
                }
                else if (GetControllerType() == ControllerType.XBox360)
                {
                    if(OSChecker.OS== Enums.OperatingSystem.Mac)
                    {
                        return Input.GetButtonDown("Fire2_Mac");
                    }
                    return Input.GetButtonDown("Fire2");
                }
                else
                {
                    return Input.GetButtonDown("Fire2");
                }
            }
        }

        /// <summary>
        /// Property to check if the "X" button is pressed.
        /// </summary>
        public static bool XPressed
        {
            get
            {
                if (GetControllerType() == ControllerType.DualShock)
                {
                    return Input.GetButtonDown("Fire3");
                }
                else if (GetControllerType() == ControllerType.XBox360)
                {
                    if(OSChecker.OS== Enums.OperatingSystem.Mac) return Input.GetButtonDown("Fire3_Mac");
                    return Input.GetButtonDown("Fire3");
                }
                else
                {
                    return Input.GetButtonDown("Fire3");
                }
            }
        }

        /// <summary>
        /// Property to check if the "Y" button is pressed.
        /// </summary>
        public static bool YPressed
        {
            get
            {
                if (GetControllerType() == ControllerType.DualShock)
                {
                    return Input.GetButtonDown("Fire4");
                }
                else if (GetControllerType() == ControllerType.XBox360)
                {
                    if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetButtonDown("Fire4_Mac");
                    return Input.GetButtonDown("Fire4");
                }
                else
                {
                    return Input.GetButtonDown("Fire4");
                }
            }
        }

        /// <summary>
        /// Property to check if the start button is pressed.
        /// </summary>
        public static bool StartPressed
        {
            get
            {
                if(GetControllerType()== ControllerType.XBox360)
                {
                    if(OSChecker.OS== Enums.OperatingSystem.Mac) return Input.GetButtonDown("Start_Mac");
                    return Input.GetButtonDown("Start");
                }
                else
                {
                    return Input.GetButtonDown("Start");
                }
               
            }
        }

        /// <summary>
        /// Property to check if the select button is pressed.
        /// </summary>
        public static bool SelectPressed
        {
            get
            {
                if (GetControllerType() == ControllerType.XBox360)
                {
                    if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetButtonDown("Select_Mac");
                    return Input.GetButtonDown("Select");
                }
                else
                {
                    return Input.GetButtonDown("Select");
                }
            }
        }

        /// <summary>
        /// Get the input for the left trigger.
        /// </summary>
        public static float LeftTrigger
        {
            get
            {
                if(GetControllerType()== ControllerType.XBox360)
                {
                    if(OSChecker.OS== Enums.OperatingSystem.Windows) return Input.GetAxis("LeftTrigger_Windows");
                    if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetAxis("LeftTrigger_Mac");
                    if (OSChecker.OS == Enums.OperatingSystem.Linux) return Input.GetAxis("LeftTrigger_Linux");
                }
                if (OSChecker.OS == Enums.OperatingSystem.Windows) return Input.GetAxis("LeftTrigger_Windows");
                if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetAxis("LeftTrigger_Mac");
                if (OSChecker.OS == Enums.OperatingSystem.Linux) return Input.GetAxis("LeftTrigger_Linux");
                return Input.GetAxis("LeftTrigger_Windows");
            }
        }

        /// <summary>
        /// Get the input for the right trigger.
        /// </summary>
        public static float RightTrigger
        {
            get
            {
                if (GetControllerType() == ControllerType.XBox360)
                {
                    if (OSChecker.OS == Enums.OperatingSystem.Windows) return Input.GetAxis("RightTrigger_Windows");
                    if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetAxis("RightTrigger_Mac");
                    if (OSChecker.OS == Enums.OperatingSystem.Linux) return Input.GetAxis("LeftTrigger_Linux");
                }
                if (OSChecker.OS == Enums.OperatingSystem.Windows) return Input.GetAxis("RightTrigger_Windows");
                if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetAxis("RightTrigger_Mac");
                if (OSChecker.OS == Enums.OperatingSystem.Linux) return Input.GetAxis("RightTrigger_Linux");
                return Input.GetAxis("RightTrigger_Windows");
            }
        }

        /// <summary>
        /// Get the input for the left bummper.
        /// </summary>
        public static bool LeftBumperPressed
        {
            get
            {
                if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetButtonDown("LeftBumper_Mac");
                return Input.GetButtonDown("LeftBumper");
            }
        }

        /// <summary>
        /// Get the input for the rightBummper
        /// </summary>
        public static bool RightBumperPressed
        {
            get
            {
                if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetButtonDown("RightBumper_Mac");
                return Input.GetButtonDown("RightBumper");
            }
        }

        /// <summary>
        /// Used to determine if the user is using a PS3 or XBox controller so that buttons can be mapped properly to inputs.
        /// </summary>
        /// <returns></returns>
        public static ControllerType GetControllerType()
        {
            if (Input.GetJoystickNames().Length == 0) return ControllerType.Keyboard;
            try
            {
                if (Input.GetJoystickNames().ElementAt(0).Contains("DualShock".ToLower()))
                {
                    return ControllerType.DualShock;
                }
                else if (Input.GetJoystickNames().ElementAt(0).Contains("XBox 360".ToLower()))
                {
                    return ControllerType.XBox360;
                }
                else if (Input.GetJoystickNames().ElementAt(0).Contains("XBox One".ToLower()))
                {
                    throw new Exception("Xbox One controllers not supported yet. Please contact Josh!");
                }
                else
                {
                    return ControllerType.Keyboard;
                }
            }
            catch (Exception err)
            {
                return ControllerType.Keyboard;
            }
        }



    }
}
