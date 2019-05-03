using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInput;

public class IceIceBaby : MonoBehaviour
{
    public float speed;
    public SpriteRenderer r;
    public int count;
    Texture2D tex, startTex;
    Color[] blank;// pixels, startPixels;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        count = r.material.GetInt("_Counter");
        tex = r.material.GetTexture("_MainTex") as Texture2D;
        r.material.SetTexture("_MainTex", tex);
        tex = r.material.GetTexture("_MainTex") as Texture2D;
        // startTex = ScreenCapture.CaptureScreenshotAsTexture();
        // startPixels = startTex.GetPixels32();

        blank = new Color[1] { new Color(0, 0, 0, 0) };
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (Input.GetAxis("Horizontal") * speed) + (InputControls.RightJoystickHorizontal / 20f), transform.position.y + (Input.GetAxis("Vertical")*speed) + (InputControls.RightJoystickVertical / 20), transform.position.z);
        pos = r.transform.InverseTransformPoint(transform.position);
        tex.SetPixels((int)pos.x, (int)pos.y, 1, 1, blank);

        /*count = 0;

        tex = ScreenCapture.CaptureScreenshotAsTexture();
        pixels = tex.GetPixels32();

        Vector3 start = Camera.main.WorldToScreenPoint(new Vector3(r.transform.position.x - r.bounds.extents.x, r.transform.position.y - r.bounds.extents.y, 0));
        Vector3 end   = Camera.main.WorldToScreenPoint(new Vector3(r.transform.position.x + r.bounds.extents.x, r.transform.position.y + r.bounds.extents.y, 0));

        for (float x = start.x; x < end.x; x++)
        {
            for(float y = start.y; y < end.y; y++)
            {
                if(!pixels[(int)(x + y * Screen.width)].Equals(startPixels[(int)(x + y * Screen.width)]))
                {
                    count++;
                }
            }
        }*/

       Debug.Log(count);
    }
}
