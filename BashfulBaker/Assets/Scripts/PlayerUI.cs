
using Assets.Scripts;
using Assets.Scripts.Content;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    Image uiPickupImage;

    private float lerp;
    public float lerpSpeed = 0.01f;
    private bool shouldLerp = false;

    public Texture2D chocochip;
    public Texture2D mintChip;
    public Texture2D raisin;
    public Texture2D pecan;

    private float y;

    // Start is called before the first frame update
    public void Start()
    {
        uiPickupImage = this.gameObject.transform.Find("Canvas").Find("PickupImage").gameObject.GetComponent<Image>();
        uiPickupImage.gameObject.SetActive(false);
        lerp = 0f;
        y = uiPickupImage.rectTransform.localPosition.y;
    }

    // Update is called once per frame
    public void Update()
    {
        if (shouldLerp == true)
        {
            lerp += lerpSpeed;
            if (lerp > 1f) lerp = 1f;
            if (lerp == 1f)
            {
                uiPickupImage.gameObject.SetActive(false);
                shouldLerp = false;
            }
            uiPickupImage.rectTransform.localPosition = Vector3.Lerp(new Vector3(0, y, 0), new Vector3(0, y+50, 0), lerp);
            float scale= (lerp < 0.5f) ? 1f + (lerp * 3f) : 4f - (lerp * 3f);
            uiPickupImage.rectTransform.localScale = new Vector3(scale, scale, 1f);
        }
    }

    public void pickUp(Enums.SpecialIngredients SP)
    {
        if(SP== Enums.SpecialIngredients.ChocolateChips)
        {
            uiPickupImage.sprite = Sprite.Create(chocochip, new Rect(0, 0, 48, 32), new Vector2(0.5f, 0.5f));
        }
        if (SP == Enums.SpecialIngredients.MintChips)
        {
            uiPickupImage.sprite = Sprite.Create(mintChip, new Rect(0, 0, 48, 32), new Vector2(0.5f, 0.5f));
        }
        if (SP == Enums.SpecialIngredients.Raisins)
        {
            uiPickupImage.sprite = Sprite.Create(raisin, new Rect(0, 0, 48, 32), new Vector2(0.5f, 0.5f));
        }
        if (SP == Enums.SpecialIngredients.Pecans)
        {
            uiPickupImage.sprite = Sprite.Create(pecan, new Rect(0, 0, 48, 32), new Vector2(0.5f, 0.5f));
        }

        uiPickupImage.rectTransform.localPosition = new Vector3(0, y, 0);
        uiPickupImage.gameObject.SetActive(true);
        lerp = 0f;
        shouldLerp = true;
        uiPickupImage.rectTransform.localScale = new Vector3(1f, 1f, 1f);

    }
}
