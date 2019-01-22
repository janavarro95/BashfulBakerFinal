using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

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
        if(UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(this.GetComponent<RectTransform>(), Input.mousePosition))
        {
            Debug.Log("WTF???");
        }
    }

    public void OnClick()
    {
        Debug.Log("CLICK");
    }
}
