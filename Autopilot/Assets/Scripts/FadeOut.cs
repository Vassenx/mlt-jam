using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FadeOut : MonoBehaviour
{

    //float fadeDuration = 4;
    //float startTime = 0;
    float transparency = 0;
    Renderer rend;
    bool insideBounds = false;

    GameObject target;
    float start = 0;

    void Start()
    {
        insideBounds = false;
        rend = GetComponent<Renderer>();
        //startTime = Time.time;
        target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        insideBounds = true;
        target = collision.gameObject;
        start = target.gameObject.transform.position.x;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        insideBounds = false;
        target = null;
    }

    void Update()
    {
        if (insideBounds && target != null)
        {
            //Debug.Log(transparency);
            transparency = 1 - Mathf.Abs(target.transform.position.x - start) / Mathf.Abs(this.transform.position.x - start);
            rend.material.SetFloat("_Transparency", transparency);
        }
        //transparency = Mathf.Lerp(0.9f, 0.0f, (Time.time - startTime) / fadeDuration);
        // rend.material.SetFloat("_Transparency", transparency);
    }
}