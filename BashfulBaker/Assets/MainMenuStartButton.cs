using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;

public class MainMenuStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameCursor.CursorIntersectsRect(this))
        {
            if (Assets.Scripts.GameInput.InputControls.APressed)
            {
                OnClick();
            }
        }
    }

    public void OnClick()
    {
        Debug.Log("CLICK");
    }
}
