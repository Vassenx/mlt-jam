using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{
    GameEvent CollectCylinder;
    [SerializeField] GameItem capsule;
    [SerializeField] GameItem cube;
    [SerializeField] GameItem cylinder;

    void Start()
    {
        CollectCylinder = new GameEvent("Collect the Cylinder", 2, new List<GameItem> { capsule, cube });
        CollectCylinder.flagHasChanged += cylinder.SetEnabled;
    }

    //This update is for debugging purposes, should SetCollected() should be managed by the player picking up objects
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            capsule.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            cube.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            cylinder.SetCollected();
        }
    }
}
