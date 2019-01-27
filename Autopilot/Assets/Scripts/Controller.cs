using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject sceneChangePrefab;
    public GameObject sceneChangePrefab_C;
    SceneChange sceneChanger;
    SceneChange sceneChanger_C;

    private float pixelsPerUnit = 16;

    public float speed = 10;
    public Camera adultCamera;
    public Camera childCamera;
    public Transform adultBody;
    public Transform childBody;

    // Use this for initialization
    void Start()
    {
        sceneChanger = sceneChangePrefab.GetComponent<SceneChange>();
        sceneChanger_C = sceneChangePrefab_C.GetComponent<SceneChange>();
        adultCamera.GetComponent<Camera>().enabled = true;
        childCamera.GetComponent<Camera>().enabled = false;
    }

    //freeze movements
    public void stopMovements()
    {
        adultBody.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        childBody.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void startMovements()
    {
        adultBody.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        childBody.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    private void Update()
    {
        if (adultCamera.GetComponent<Camera>().enabled)
        {
            childBody.GetComponent<Collider2D>().enabled = false;
            adultBody.GetComponent<Collider2D>().enabled = true;
            Vector3 pos = adultBody.position;
            pos.z = childBody.position.z;
            childBody.position = pos;
        }
        else
        {
            adultBody.GetComponent<Collider2D>().enabled = false;
            childBody.GetComponent<Collider2D>().enabled = true;
            Vector3 pos = childBody.position;
            pos.z = adultBody.position.z;
            adultBody.position = pos;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("CameraSwitch"))
        {
            if(sceneChanger != null)
            {
                sceneChanger.activateSceneChange();
                sceneChanger_C.activateSceneChange();
            }
            
            adultCamera.GetComponent<Camera>().enabled = !adultCamera.GetComponent<Camera>().enabled;
            childCamera.GetComponent<Camera>().enabled = !childCamera.GetComponent<Camera>().enabled;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        Vector3 newLocalScale = adultBody.localScale;

        //animate walking and idle
        if (move.x < 0)
        {
            newLocalScale.x = Mathf.Abs(newLocalScale.x);
            adultBody.localScale = newLocalScale;
            childBody.localScale = newLocalScale;
            AnimatePlayers(true);
        }
        else if(move.x == 0)
        {
            AnimatePlayers(false);
        }
        else
        {
            newLocalScale.x = -1 * Mathf.Abs(newLocalScale.x);
            adultBody.localScale = newLocalScale;
            childBody.localScale = newLocalScale;
            AnimatePlayers(true);
        }


        adultBody.position += move * speed * Time.deltaTime;
        childBody.position += move * speed * Time.deltaTime;
    }

    void AnimatePlayers(bool isWalking)
    {
        childBody.GetComponent<Animator>().SetBool("isWalking", isWalking);
        adultBody.GetComponent<Animator>().SetBool("isWalking", isWalking);
    }
}
