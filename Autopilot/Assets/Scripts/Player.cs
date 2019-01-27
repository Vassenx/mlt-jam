using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public float searchRadius; 

    public GameObject Inventory; 

    private bool hide_inventory = true; 
    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();
        searchRadius = collider.radius; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vx = new Vector3(speed * Time.deltaTime, 0, 0);
        Vector3 vy = new Vector3(0, speed * Time.deltaTime, 0); 

        float axis_x = Input.GetAxisRaw("Horizontal");
        if (axis_x < 0)
        {
            transform.position = transform.position - vx;
        }
        else if (axis_x > 0)
        {
            transform.position = transform.position + vx;
        }

        float axis_y = Input.GetAxisRaw("Vertical");

        if (axis_y < 0)
        {
            transform.position = transform.position - vy;
        }
        else if (axis_y > 0)
        {
            transform.position = transform.position + vy;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            hide_inventory = !hide_inventory; 
        }

        if (hide_inventory)
        {
            Inventory.SetActive(true); 
        } 
        else
        {
            Inventory.SetActive(false); 
        }

        
        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }

    void onColliderEnter2D(Collider2D col)
    {
        checkInteractables(); 
    }

    public void checkInteractables()
    {
        LayerMask mask = LayerMask.GetMask("Item"); 
        Collider2D[] itemColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, mask);
     
        foreach (var collider in itemColliders)
        {
             Debug.Log(collider.gameObject); 
        }
    }
}
