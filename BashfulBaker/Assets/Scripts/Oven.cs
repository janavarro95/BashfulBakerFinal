using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameInput;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;


public class Oven : MonoBehaviour

{
    public ParticleSystem Smoke; 
    public GameObject Arrow;
    // Start is called before the first frame update
    void Start()
    {
        //Smoke.enableEmission = false;
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (InputControls.APressed && Game.Player.dishesInventory.Contains("Chocolate Chip Cookie") && Arrow.GetComponent<progress>().step == 3)
        {
            Smoke.enableEmission = false; 
            Debug.Log("Good to go");
        }
    }
    //Game.Player.specialIngredientsInventory.Add(new SpecialIngredient("Chocolate Chip"));
    //Game.Player.dishesInventory.Add(new Dish("Cookie Ingredients"));
}
