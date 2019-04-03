using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeEgg : MonoBehaviour
{
    public SpriteRenderer eggCarton;
    public Sprite cartonMinusone;

    void takeEggOut()
    {
        eggCarton.sprite = cartonMinusone;
    }
}
