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
            //If object has sound play it here
            // Run interaction text here too
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