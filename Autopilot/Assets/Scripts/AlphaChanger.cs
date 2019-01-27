using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AlphaChanger : MonoBehaviour
{
    SpriteRenderer spriteRend;

    void Start()
    {
        spriteRend = this.GetComponent<SpriteRenderer>();
    }

    public void changeAlpha(float alpha)
    {
        Color newColor = spriteRend.color;
        newColor.a = alpha;
        spriteRend.color = newColor;
    }

}
