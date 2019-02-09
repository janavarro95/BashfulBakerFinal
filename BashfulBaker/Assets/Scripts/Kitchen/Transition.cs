using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sprite;
    float alpha, top, bot;
    Color hard, soft;

    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        sprite = this.GetComponent<SpriteRenderer>();
        alpha = 0f;
        sprite.color = new Color(1f, 1f, 1f, alpha);

        hard = new Color(1f, 1f, 1f, 1f);
        soft = new Color(1f, 1f, 1f, 0f);

        top = -1;
        bot = -3;
    }

    void Update()
    {
        if(player.transform.position.y < top && player.transform.position.y > bot)
        {
            alpha = (bot - player.transform.position.y) / (bot - top);
            sprite.color = Color.Lerp(hard, soft, alpha);
        }
    }
}
