using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
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

    public int stack;

    public Sprite sprite;

    public Item()
    {

    }

    public Item(string Name)
    {
        this.itemName = Name;
        stack = 1;
    }

    public Item(string Name, int StackSize)
    {
        this.itemName = Name;
        stack = StackSize;
    }

	// Use this for initialization
	void Start () {
        this.sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void addToStack(int amount)
    {
        this.stack += amount;
    }

    public void removeFromStack(int amount)
    {
        this.stack -= amount;
    }

    /// <summary>
    /// Gets a clone of the game object. Aka instantiates a new object with the same data as this object it was cloned from.
    /// </summary>
    /// <returns></returns>
    public GameObject clone()
    {
        //Implement this.
        return loadFromPrefab();
    }

    /// <summary>
    /// Loads an asset from the list of prefabs.
    /// </summary>
    /// <returns></returns>
    public virtual GameObject loadFromPrefab()
    {
        string path = Path.Combine(Path.Combine("Prefabs", "Items"), this.Name);
        return (GameObject)Resources.Load(path, typeof(Item));
    }

    public static GameObject LoadItemFromPrefab(string ItemName)
    {
        string path = Path.Combine(Path.Combine( "Prefabs", "Items"), ItemName);
        return (GameObject)Resources.Load(path, typeof(Item));
    }
}
