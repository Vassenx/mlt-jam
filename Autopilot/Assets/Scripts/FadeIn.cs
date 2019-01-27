using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FadeIn : MonoBehaviour
{

    float fadeDuration = 30f;
    float transparency = 0;

    Renderer rend;
    bool startGame = false;

    AudioSource startingAudio;
    GameObject target;
    float startTime = 0;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        startingAudio = this.GetComponent<AudioSource>();
    }

    void Start()
    {
        rend.material.SetFloat("_Transparency", 0.99f);
        startingAudio.Play();
        startTime = Time.time;
        startGame = true;
    }

    void Update()
    {
        //once audio stops, fade in
        if (startGame && !startingAudio.isPlaying)
        {
            transparency = Mathf.Lerp(0.99f, 0.0f, (Time.time - startTime) / fadeDuration);
            rend.material.SetFloat("_Transparency", transparency);

            if (transparency <= 0.0f) startGame = false;
        }
    }
}
