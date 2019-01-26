﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed;
    public Camera adultCamera;
    public Camera childCamera;
    public Transform adultBody;
    public Transform childBody;

    // Use this for initialization
    void Start()
    {
        adultCamera.GetComponent<Camera>().enabled = true;
        childCamera.GetComponent<Camera>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("CameraSwitch"))
        {
            adultCamera.GetComponent<Camera>().enabled = !adultCamera.GetComponent<Camera>().enabled;
            childCamera.GetComponent<Camera>().enabled = !childCamera.GetComponent<Camera>().enabled;
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        adultBody.position += move * speed * Time.deltaTime;
        childBody.position += move * speed * Time.deltaTime;
    }
}
