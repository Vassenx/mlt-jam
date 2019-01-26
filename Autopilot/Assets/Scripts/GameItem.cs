using System.Runtime.InteropServices;
using UnityEngine;
using Assets.Scripts;

public class GameItem : MonoBehaviour
{
    [SerializeField] string Name;
    [SerializeField] string Description;
    private bool Collected;
    [SerializeField] bool Enabled;
    public delegate void CollectedHasChanged();
    public event CollectedHasChanged collectedHasChanged;

    void Start()
    {

    }

    public void SetCollected()
    {
        if(Enabled)
        {
            Debug.Log(Name + " has been collected");
            Collected = true;
            if(collectedHasChanged != null)
                collectedHasChanged();
        }
    }

    public void SetEnabled()
    {
        Debug.Log(Name + " has been enabled");
        Enabled = true;
    }
}