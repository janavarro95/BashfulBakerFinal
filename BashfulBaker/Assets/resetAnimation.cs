using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetAnimation : MonoBehaviour
{


    public Animator flyingFood;
    public Sprite butter;
    public Sprite sugar;
    public Sprite egg;
    public Sprite flour;
    public GameObject flyingFoodSprite;
    SpriteRenderer food;
    public Sprite[] Ingrediants;
    private int step;

    private void Start()
    {
        // Ingrediants = new Sprite[6];
        step = 0;
    }




    public void resetFood()
    {
        flyingFoodSprite.GetComponent<SpriteRenderer>().sprite = Ingrediants[step]; 

        flyingFood.SetInteger("addAnimation", 0);
        step++;
    }


}
