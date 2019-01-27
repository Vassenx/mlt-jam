using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public string description = "Description";
    public bool collected = false;
    public bool enabled = false;
    public bool contains = false;
    public bool grabable = false; 
    public string uninteractableText;
}
