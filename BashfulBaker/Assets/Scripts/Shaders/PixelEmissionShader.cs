using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Taken from the tutorial and used in conjunction with the modified PixelEmissionShader based on the sprite outline shader at: https://nielson.io/2016/04/2d-sprite-outlines-in-unity
/// </summary>
[ExecuteInEditMode]
public class PixelEmissionShader : MonoBehaviour
{
    public Color tintColor = Color.white;

    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Runs when the component is enabled.
    /// </summary>
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateOutline(true);
    }

    /// <summary>
    /// When disabled stop updating the outline.
    /// </summary>
    void OnDisable()
    {
        UpdateOutline(false);
    }

    /// <summary>
    /// Only runs if the component is disabled.
    /// </summary>
    void Update()
    {
        UpdateOutline(true);
    }

    /// <summary>
    /// Update the sprite emission outline.
    /// </summary>
    /// <param name="outline"></param>
    void UpdateOutline(bool outline)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", tintColor);
        spriteRenderer.SetPropertyBlock(mpb);
    }
}
