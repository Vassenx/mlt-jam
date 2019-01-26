using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    bool inBound;
    GameObject target;

    void Start()
    {
        inBound = false;
    }

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

    void Update()
    {
        //if within an item's collider and space was pressed, then grab object
        if (inBound && target != null && Input.GetKeyDown(KeyCode.Space))
        {
            //hide object that was grabbed
            this.GetComponent<Renderer>().enabled = false;
            //Inventory.instance.addItem(this);
        }
    }
}
