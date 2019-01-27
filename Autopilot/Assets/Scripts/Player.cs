using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public float searchRadius; 

    public GameObject inventory;
    public GameObject journal;
    public Transform journalList;

    private bool hide_inventory = true;
    private bool hide_journal = true;
    private bool canMove = true; 
    private Collider2D[] itemColliders2;

    private Text[] textList;

    private static int journal_slots = 6;
    private int selectedItem; 

    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();
        searchRadius = collider.radius;
        textList = journalList.GetComponentsInChildren<Text>();
        selectedItem = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
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
        }

        // Toggle inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
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

        // Toggle journal 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!hide_journal)
            {
                Item itemToAdd = itemColliders2[selectedItem].GetComponent<GameItem>().getItem();
                Inventory.instance.Add(itemToAdd);
                Destroy(itemColliders2[selectedItem].gameObject); 
            }
            hide_journal = !hide_journal;
        }

        if (hide_journal)
        {
            journal.SetActive(false);
            canMove = true; 
            //Text[] textList = journalList.GetComponentsInChildren<Text>();
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
            canMove = false;

            if (Input.GetKeyDown(KeyCode.W))
            {
                textList[selectedItem].color = Color.black; 
                selectedItem--;
                if (selectedItem < 0)
                {
                    selectedItem = 0;
                }
                textList[selectedItem].color = Color.red;

                Debug.Log(selectedItem);

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                textList[selectedItem].color = Color.black;
                selectedItem++; 
                if(selectedItem > itemColliders2.Length - 1)
                {
                    selectedItem = itemColliders2.Length - 1;   
                }

                textList[selectedItem].color = Color.red;
                Debug.Log(selectedItem);
            }

            
        }

        LayerMask mask = LayerMask.GetMask("Item");
        itemColliders2 = Physics2D.OverlapCircleAll(transform.position, searchRadius, mask);

        checkInteractables(); 
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }

    /*
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision"); 
        LayerMask mask = LayerMask.GetMask("Item");
        itemColliders2 = Physics2D.OverlapCircleAll(transform.position, searchRadius, mask);

        foreach (var collider in itemColliders2)
        {
            Debug.Log(collider.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log("Exit"); 
        LayerMask mask = LayerMask.GetMask("Item");
        itemColliders2 = Physics2D.OverlapCircleAll(transform.position, searchRadius, mask);
    }*/

    public void checkInteractables()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int i = 0;
            //Text[] textList = journalList.GetComponentsInChildren<Text>();
            if (itemColliders2 != null)
            {
           
                foreach (var collider in itemColliders2)
                {
                    Debug.Log(collider); 
                    Item gameItem = collider.gameObject.GetComponent<GameItem>().getItem();
                    textList[i].text = gameItem.name;
                    i++; 
                }
            }
        }
        
        /*
        Collider2D[] itemColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, mask);

        foreach (var collider in itemColliders)
        {
             Debug.Log(collider.gameObject); 
        }*/
    }
}
