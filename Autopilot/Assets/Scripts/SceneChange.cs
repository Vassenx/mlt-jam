using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    float fadeDuration = 100;
    float startTime = 0;

    private bool sceneChange;
    private bool fadeInDone;
    private float transparency;
    private float speed = 100f;
    Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetFloat("_Transparency", 0f);
    }

    void Start()
    {
        transparency = 0.99f;
        fadeInDone = false;
    }

    public void activateSceneChange()
    {
        sceneChange = true;
        startTime = Time.time;
    }

    private void fadeIn()
    {
        transparency = Mathf.Lerp(0.0f, 0.99f, (Time.time - startTime) * speed / fadeDuration);
        rend.material.SetFloat("_Transparency", transparency);

        //fadeIn finished
        if (transparency == 0.99f) StartCoroutine(FadeStop());
    }

    private void fadeOut()
    {
        transparency = Mathf.Lerp(0.99f, 0.0f, (Time.time - startTime) * speed / fadeDuration);
        rend.material.SetFloat("_Transparency", transparency);
        if (transparency == 0.0f) sceneChange = false;
    }

    void Update()
    {
        if (sceneChange)
        {
            if(!fadeInDone) fadeIn();
            if(fadeInDone) fadeOut();
        }
    }

    IEnumerator FadeStop()
    {
        yield return new WaitForSeconds(0.5f);
        fadeInDone = true;
        startTime = Time.time;
    }
}
