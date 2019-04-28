using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class TreeDecider : MonoBehaviour
{
    public int decided = -1;
    public Sprite[] TreeSprites;

    #if UNITY_EDITOR
    private void Awake()
    {
        this.decided = -1;
    }
    void Start()
    {
        int l = TreeSprites.Length;

        if (l > 0 && decided < 0)
        {
            int r = Random.Range(0, l - 1);
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();

            this.decided = r;
            sr.sprite = TreeSprites[r];
        }
    }
    #endif
}
