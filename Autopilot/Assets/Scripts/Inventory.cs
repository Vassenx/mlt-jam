using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    #region
    private void Awake()
    {
        if (instance != null)
        {
            return; 
        }

        instance = this; 
    }
    #endregion
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback; 
    public int space = 10; 

    public List<GameItem> items = new List<GameItem>(); 

    public void Add(GameItem item)
    {
        if (items.Count < space)
             items.Add(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke(); 
    }

    public void Remove(GameItem item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
