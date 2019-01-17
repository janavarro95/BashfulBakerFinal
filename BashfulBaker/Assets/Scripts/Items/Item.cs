using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string itemName;

    public string Name
    {
        get
        {
            return itemName;
        }
        set
        {
            this.itemName = value;
        }
    }

    public Sprite sprite;

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = this.sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Gets a clone of the game object. Aka instantiates a new object with the same data as this object it was cloned from.
    /// </summary>
    /// <returns></returns>
    public Item clone()
    {
        //Implement this.
        throw new NotImplementedException();
    }
}
