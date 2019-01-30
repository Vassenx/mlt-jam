﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Controller : MonoBehaviour
{
    public GameObject sceneChangePrefab;
    public GameObject sceneChangePrefab_C;
    public float y_search_offset;

    SceneChange sceneChanger;
    SceneChange sceneChanger_C;

    [SerializeField]
    GameObject adultLights;
    [SerializeField]
    GameObject childLights;
    [SerializeField]
    AudioSource rain;

    private float pixelsPerUnit = 16;

    public float speed = 10;
    public Camera adultCamera;
    public Camera childCamera;
    public GameObject adultFurniture;
    public GameObject childFurniture;
    public Transform adultBody;
    public Transform childBody;
    public bool timeTravelEnabled;

    public float searchRadius;

    public GameObject inventory;
    public GameObject journal;
    public Transform journalList;

    private bool hide_inventory = true;
    private bool hide_journal = true;
    private Collider2D[] itemColliders;

    private Text[] textList;

    private static int journal_slots = 6;
    private int selectedItem;

    public GameObject inspectorUI;
    public Text inspectorText;

    // Use this for initialization
    void Start()
    {
        sceneChanger = sceneChangePrefab.GetComponent<SceneChange>();
        sceneChanger_C = sceneChangePrefab_C.GetComponent<SceneChange>();
        adultCamera.GetComponent<Camera>().enabled = true;
        childCamera.GetComponent<Camera>().enabled = false;
        timeTravelEnabled = true;

        textList = journalList.GetComponentsInChildren<Text>();
        selectedItem = 0;
    }

    //freeze movements

    /*
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
    */

    private void Update()
    {
        if (adultCamera.GetComponent<Camera>().enabled)
        {
            childBody.GetComponent<Collider2D>().enabled = false;
            adultBody.GetComponent<Collider2D>().enabled = true;

            Vector3 pos = adultBody.position;
            pos.z = childBody.position.z;
            childBody.position = pos;

            adultLights.SetActive(true);
            childLights.SetActive(false);

            rain.Play();
        }

        else
        {
            adultBody.GetComponent<Collider2D>().enabled = false;
            childBody.GetComponent<Collider2D>().enabled = true;

            Vector3 pos = childBody.position;
            pos.z = adultBody.position.z;
            adultBody.position = pos;

            adultLights.SetActive(false);
            childLights.SetActive(true);

            rain.Stop();
        }

        // Toggle inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            // If journal is not hidden
            if (!hide_journal)
            {
                hide_journal = !hide_journal;
            }
            hide_inventory = !hide_inventory;
        }

        if (hide_inventory)
        {
            inventory.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!hide_journal)
            {

                if (itemColliders.Length != 0)
                {
                    GameItem gItem = itemColliders[selectedItem].GetComponent<GameItem>(); 
                    gItem.SetCollected();
                    if (gItem.Grabable && gItem.Collected)
                    {
                        Inventory.instance.Add(gItem);
                        if (!gItem.Contains)
                        {
                            Destroy(itemColliders[selectedItem].gameObject);
                        }
                       
                    }
                }

            }
            else
            {
                // If inventory is open close it
                if (!hide_inventory)
                {
                    hide_inventory = !hide_inventory;
                }
            }

            hide_journal = !hide_journal;
        }

        if (hide_journal)
        {
            journal.SetActive(false);

            foreach (Text text in textList)
            {
                text.text = "";
                text.color = Color.black;
            }
            selectedItem = 0;
            textList[selectedItem].color = Color.red;
        }
        else
        {
            journal.SetActive(true);

            if (Input.GetKeyDown(KeyCode.W))
            {
                textList[selectedItem].color = Color.black;
                selectedItem--;
                if (selectedItem < 0)
                {
                    selectedItem = 0;
                }
                textList[selectedItem].color = Color.red;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                textList[selectedItem].color = Color.black;
                selectedItem++;
                if (selectedItem > itemColliders.Length - 1)
                {
                    selectedItem = itemColliders.Length - 1;
                }

                textList[selectedItem].color = Color.red;
            }
        }


        LayerMask mask = LayerMask.GetMask("Item");
        Vector3 offset = new Vector3(0.0f, y_search_offset, 0.0f); 
        
        itemColliders = Physics2D.OverlapCircleAll(adultBody.position + offset, searchRadius, mask);
        checkInteractables();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("CameraSwitch") && timeTravelEnabled)
        {
            if(sceneChanger != null)
            {
                sceneChanger.activateSceneChange();
                sceneChanger_C.activateSceneChange();
            }
            
            adultCamera.GetComponent<Camera>().enabled = !adultCamera.GetComponent<Camera>().enabled;
            adultFurniture.SetActive(!adultFurniture.activeInHierarchy);
            childCamera.GetComponent<Camera>().enabled = !childCamera.GetComponent<Camera>().enabled;
            childFurniture.SetActive(!childFurniture.activeInHierarchy);
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
            AnimatePlayers(true);        }

        if (hide_journal && hide_inventory)
        {
            adultBody.position += move * speed * Time.deltaTime;
            childBody.position += move * speed * Time.deltaTime;
        }
       
    }

    void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0.0f, y_search_offset, 0.0f);
        Gizmos.DrawWireSphere(adultBody.position + offset, searchRadius);
    }

    void AnimatePlayers(bool isWalking)
    {
        childBody.GetComponent<Animator>().SetBool("isWalking", isWalking);
        adultBody.GetComponent<Animator>().SetBool("isWalking", isWalking);
    }

    void EnableTimeTravel()
    {
        timeTravelEnabled = true;
    }

    void DisableTimeTravel()
    {
        timeTravelEnabled = false;
    }

    public void checkInteractables()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int i = 0;

            if (itemColliders != null)
            {

                foreach (var collider in itemColliders)
                {
                    GameItem gameItem = collider.gameObject.GetComponent<GameItem>();
                    if(gameItem != null && collider.gameObject.GetComponent<SpriteRenderer>().enabled)
                    {
                        textList[i].text = gameItem.name;
                        i++;
                    }
                }
            }
        }

    }
}
