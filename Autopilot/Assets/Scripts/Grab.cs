using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grab : MonoBehaviour
{
    bool inspecting = false;

    bool inBound = false;
    GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inBound = true;
        target = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inBound = false;
        target = null;
    }

    void endInspect()
    {
        inspecting = false;
        //Inventory.instance.getItem("").disablePanel;
    }

    void Update()
    {
        //if within an item's collider and G was pressed, then grab object
        if (inBound && target != null && Input.GetKeyDown(KeyCode.G))
        {
            inspecting = true;
            //Inventory.instance.getItem("").disablePanel;

            //trigger interaction
        }

        if (inspecting && Input.GetKeyDown(KeyCode.Y))
        {
            //hide object that was grabbed
            this.GetComponent<Renderer>().enabled = false;
            //Inventory.instance.addItem(this);
            endInspect();
        }
        
        if(inspecting && Input.GetKeyDown(KeyCode.N))
        {
            endInspect();
        }
    }
}
