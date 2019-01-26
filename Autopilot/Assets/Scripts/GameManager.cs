using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{
    GameEvent SpawnKey;
    GameEvent UnlockDoor;
    [SerializeField]
    GameItem tricycle;
    [SerializeField]
    GameItem key;
    [SerializeField]
    GameItem door;

    void Start()
    {
        SpawnKey = new GameEvent("Spawn the Key", 1, new List<GameItem> { tricycle });
        UnlockDoor = new GameEvent("Unlock the Door", 1, new List<GameItem> { key });
        SpawnKey.flagHasChanged += Event_SpawnKey;
        UnlockDoor.flagHasChanged += Event_UnlockDoor;
        door.collectedHasChanged += Event_DoorFade;
    }

    void Event_DoorFade()
    {
        Debug.Log("Init door fade"); //Replace with actual fade for animation.
    }

    void Event_SpawnKey()
    {
        key.GetComponent<MeshRenderer>().enabled = true;
        Debug.Log("The key is now visible");
        key.SetEnabled();
    }

    void Event_UnlockDoor()
    {
        Debug.Log("The door is now unlocked");
        door.SetEnabled();
    }

    //This update is for debugging purposes, should SetCollected() should be managed by the player picking up objects
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            tricycle.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            key.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            door.SetCollected();
        }
    }
}
