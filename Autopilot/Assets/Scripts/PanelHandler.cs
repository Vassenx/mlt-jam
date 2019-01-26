using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    
    GameObject descriptionPanel;
    Image descriptionImage;
    protected bool showPanel;

    private void Awake()
    {
        descriptionImage = descriptionPanel.GetComponent<Image>();
        descriptionImage.enabled = false;
    }

    void Update()
    {
        if (showPanel)
        {

        }
    }
}
