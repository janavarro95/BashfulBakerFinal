using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.Components
{
    public class MenuComponent
    {
        public MenuComponent leftNeighbor;
        public MenuComponent rightNeighbor;
        public MenuComponent topNeighbor;
        public MenuComponent bottomNeighbor;

        public MonoBehaviour unityObject;

        public bool active
        {
            get
            {
                return unityObject.gameObject.activeInHierarchy;
            }
        }

        public MenuComponent(MonoBehaviour UnityObject)
        {
            this.unityObject = UnityObject;
        }

        public void setNeighbors(MenuComponent Left=null, MenuComponent Right=null, MenuComponent Top=null, MenuComponent Bottom = null)
        {
            leftNeighbor = Left;
            rightNeighbor = Right;
            topNeighbor = Top;
            bottomNeighbor = Bottom;
        }

        public void snapToThisComponent()
        {
            if (Game.Menu.menuCursor != null)
            {
                if (Game.Menu.selectedComponent != null)
                {
                    Game.Menu.menuCursor.gameObject.GetComponent<RectTransform>().position = this.unityObject.transform.position;
                }
            }
        }

        public MenuComponent snapToNextComponent(Enums.FacingDirection NextDirection)
        {
            if (Game.Menu.menuCursor != null)
            {
                if(NextDirection== Enums.FacingDirection.Up && topNeighbor!=null)
                {
                    Game.Menu.menuCursor.gameObject.GetComponent<RectTransform>().position = topNeighbor.unityObject.transform.position;
                    Game.Menu.selectedComponent = topNeighbor;
                    return topNeighbor;
                }
                if (NextDirection == Enums.FacingDirection.Down && bottomNeighbor != null)
                {
                    Game.Menu.menuCursor.gameObject.GetComponent<RectTransform>().position = bottomNeighbor.unityObject.transform.position;
                    Game.Menu.selectedComponent = bottomNeighbor;
                    return bottomNeighbor;
                }
                if (NextDirection == Enums.FacingDirection.Left && leftNeighbor != null)
                {
                    Game.Menu.menuCursor.gameObject.GetComponent<RectTransform>().position = leftNeighbor.unityObject.transform.position;
                    Game.Menu.selectedComponent = leftNeighbor;
                    return leftNeighbor;
                }
                if (NextDirection == Enums.FacingDirection.Right && rightNeighbor != null)
                {
                    Game.Menu.menuCursor.gameObject.GetComponent<RectTransform>().position = rightNeighbor.unityObject.transform.position;
                    Game.Menu.selectedComponent = rightNeighbor;
                    return rightNeighbor;
                }

                return Game.Menu.selectedComponent;
            }
            return null; //No snapping here!
        }

        public void setActive(bool active)
        {
            this.unityObject.gameObject.SetActive(active);
        }
    }
}
