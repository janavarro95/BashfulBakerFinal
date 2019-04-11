using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;

public class MainMenuStartButton : MonoBehaviour
{
    GameObject eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        //eventSystem = GameObject.Find("EventSystem");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameCursor.SimulateMousePress(this))
        {
            //this.GetComponent<Button>().onClick.Invoke();
            //eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(this.gameObject);
            this.OnClick();
            //eventSystem.SendMessage("OnClick");
            //if (eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == this.gameObject) eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }

    }

    public void OnClick()
    {
        //Debug.Log("CLICK");
    }
}
