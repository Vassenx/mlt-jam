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
            Debug.Log(Name + " has been collected");
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
        Debug.Log(Name + " has been enabled");
        Enabled = true;
    }

    public void SetDisabled()
    {
        Debug.Log(Name + "has been disabled");
        Enabled = false;
    }
}