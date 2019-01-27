using System.Runtime.InteropServices;
using UnityEngine;
using Assets.Scripts;

public class GameItem : MonoBehaviour
{
    [SerializeField] Item item; 

    public delegate void CollectedHasChanged();
    public event CollectedHasChanged collectedHasChanged;

    public void SetCollected()
    {
        if(item.enabled && !item.collected)
        {

            Debug.Log("Player interacted with " + item.name);
            //If object has sound play it here
            // Run interaction text here too
            item.collected = true;
            if(collectedHasChanged != null)
                collectedHasChanged();
        } else
        {
            Debug.Log(item.uninteractableText);
        }
    }

    public Item getItem()
    {
        return item; 
    }

    public void SetEnabled()
    {
        item.enabled = true;
    }
    
    public void SetDisabled()
    {
        item.enabled = false;
    }
}