using Assets.Scripts.GameInformation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Item
{

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

    protected Texture2D _sprite;

    public Texture2D Sprite
    {
        get
        {
            if (this._sprite == null) loadSpriteFromDisk();
            return _sprite;
        }
        set
        {
            _sprite = value;
        }

    }

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

    public void initializeSprite()
    {
        loadSpriteFromDisk();
    }


    public virtual Item clone()
    {
        return new Item(this.Name);
    }


    protected virtual void loadSpriteFromDisk()
    {
        string combinedFolders = Path.Combine("Graphics", "Items");

        this._sprite = Game.ContentManager.loadTexture2DFromStreamingAssets(Path.Combine(combinedFolders, this.itemName));
    }


    public void loadSprite()
    {
        loadSpriteFromDisk();
    }
}