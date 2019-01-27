using System.Runtime.InteropServices;
using UnityEngine;
using Assets.Scripts;

public class GameItem : MonoBehaviour
{
    [SerializeField] string Name;
    [SerializeField] string Description;
    [SerializeField] string UniteractableText;
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
        if(Enabled && !Collected)
        {

            Debug.Log("Player interacted with " + Name);
            Collected = true;
            if(collectedHasChanged != null)
                collectedHasChanged();
        } else
        {
            Debug.Log(UniteractableText);
        }
    }

    public Item getItem()
    {
        return item; 
    }

    public void SetEnabled()
    {
        Enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SetCollected(); 
    }
    
    public void SetDisabled()
    {
        Enabled = false;
    }
}