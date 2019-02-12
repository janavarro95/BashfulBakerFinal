using Assets.Scripts.Menus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantryCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player" && Assets.Scripts.GameInput.InputControls.APressed)
        {
            Menu.Instantiate("PantryMenu");
        }
    }

}
