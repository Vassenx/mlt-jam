using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float xOffset = 0f;
    float yOffset = 1f;
    float zOffset = 0f;

    //screen bounds
    float xMin = -20;
    float xMax = 20;
    float yMin = -6;
    float yMax = 8;

    public GameObject player;

    void LateUpdate()
    {
        float xPos = player.transform.position.x + xOffset;
        float yPos = player.transform.position.y + yOffset;

        if (xPos < xMin) xPos = xMin;
        if (xPos > xMax) xPos = xMax;
        if (yPos < yMin) yPos = yMin;
        if (yPos > yMax) yPos = yMax;

        this.transform.position = new Vector3(xPos, yPos, player.transform.position.z + zOffset);
    }

}
