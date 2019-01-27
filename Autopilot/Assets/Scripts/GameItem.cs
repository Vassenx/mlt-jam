using System.Runtime.InteropServices;
using UnityEngine;
using Assets.Scripts;

public class GameItem : MonoBehaviour
{
    [SerializeField]
    string Name;
    [SerializeField]
    string Description;
    [SerializeField]
    string UniteractableText;
    public bool Collected;
    public bool Enabled;
    public Sprite Icon;
    public bool Grabable;
    public bool Contains;

    public delegate void CollectedHasChanged();
    public event CollectedHasChanged collectedHasChanged;

    public void SetCollected()
    {
        if((Enabled && !Collected) || (Enabled && !Grabable))
        {
            //If object has sound play it here
            // Run interaction text here too
            Debug.Log(Name + ": " + Description);
            Collected = true;
            if(collectedHasChanged != null)
                collectedHasChanged();
        } else
        {
            Debug.Log(UniteractableText);
        }
    }

    public void SetEnabled()
    {
        Enabled = true;
    }
    
    public void SetDisabled()
    {
        Enabled = false;
    }
}