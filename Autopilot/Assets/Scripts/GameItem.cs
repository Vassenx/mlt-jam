using System.Runtime.InteropServices;
using UnityEngine;
using Assets.Scripts;

public class GameItem : MonoBehaviour
{
    [SerializeField] string Name;
    [SerializeField] string Description;
    private bool Collected;
    [SerializeField] bool Enabled;
    [SerializeField] Item item; 

    public delegate void CollectedHasChanged();
    public event CollectedHasChanged collectedHasChanged;

    void Start()
    {

    }

    public void SetCollected()
    {
        if(Enabled)
        {
            Debug.Log(item.name + " has been collected");
            Collected = true;
            if(collectedHasChanged != null)
                collectedHasChanged();

            //Inventory.instance.Add(item);

           // Destroy(gameObject); 
        }
    }

    public void SetEnabled()
    {
        Debug.Log(Name + " has been enabled");
        Enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SetCollected(); 
    }
}