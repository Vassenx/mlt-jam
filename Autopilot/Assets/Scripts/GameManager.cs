using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{
    GameEvent SpawnKey;
    GameEvent UnlockDoor;
    GameEvent BreakPiggy;
    GameEvent DiscoverReceipe;
    GameEvent MakeSandwhich;
    GameEvent LearnRepair;
    GameEvent FixVCR;
    GameEvent EndGame;
    [SerializeField]
    GameItem tricycle, key, door;
    [SerializeField]
    GameItem pastPiggyBank,presentPiggyBank, coin1, coin2, coin3;
    [SerializeField]
    GameItem mum, presentFridge, presentPantry, presentMicrowave, presentToaster;
    [SerializeField]
    GameItem cable1, cable2, cable3, dad, vcr;
    [SerializeField]
    GameItem playerPainting, dadPainting, mumPainting;

    void Start()
    {
        SpawnKey = new GameEvent("Located the Key", 1, new List<GameItem> { tricycle });
        UnlockDoor = new GameEvent("Unlocked the Door", 1, new List<GameItem> { key });
        BreakPiggy = new GameEvent("Broke the bank", 3, new List<GameItem> { coin1, coin2, coin3 });
        DiscoverReceipe = new GameEvent("Learned the receipe from Mum", 1, new List<GameItem> { mum });
        MakeSandwhich = new GameEvent("Made a sandwhich", 3, new List<GameItem> { presentFridge, presentMicrowave, presentPantry });
        LearnRepair = new GameEvent("Learnt VCR repair from Dad", 3, new List<GameItem> { cable1, cable2, cable3 });
        FixVCR = new GameEvent("Fixed the VCR", 1, new List<GameItem> { dad });
        EndGame = new GameEvent("Opened the Attic", 3, null);

        SpawnKey.flagHasChanged += Event_SpawnKey;
        UnlockDoor.flagHasChanged += Event_UnlockDoor;
        door.collectedHasChanged += Event_DoorFade;

        BreakPiggy.flagHasChanged += Event_BreakPiggy;
        pastPiggyBank.collectedHasChanged += Event_ChangeBankSprite;
        presentPiggyBank.collectedHasChanged += Event_CheckBrokenBank;

        DiscoverReceipe.flagHasChanged += Event_ReciepeLearnt;
        MakeSandwhich.flagHasChanged += Event_PrepareMeal;
        presentToaster.collectedHasChanged += Event_EnjoyYourMeal;

        LearnRepair.flagHasChanged += Event_KnowVcrRepair;
        FixVCR.flagHasChanged += Event_FixVcr;
        vcr.collectedHasChanged += Event_PlayVHS;

        EndGame.flagHasChanged += Event_OpenAttic;
    }

    void Event_OpenAttic()
    {
        Debug.Log("The attic has opened");
    }

    void Event_PlayVHS()
    {
        Debug.Log("Your favourite VHS still works");
        Debug.Log("Play TV animation");
        dad.SetDisabled();
        dadPainting.SetEnabled(); // Maybe use set visible instead.
        EndGame.IncCounter();
    }

    void Event_FixVcr()
    {
        Debug.Log("Dad taught you how to repair the VCR");
        vcr.SetEnabled();
    }

    void Event_KnowVcrRepair()
    {
        Debug.Log("Got all cables, Dad will teach VCR repair");
        dad.SetEnabled();
    }

    void Event_EnjoyYourMeal()
    {
        Debug.Log("Play eating sandwhich");
        mum.SetDisabled();
        presentToaster.SetDisabled();
        mumPainting.SetEnabled(); // Maybe use set visible instead.
        EndGame.IncCounter();
    }

    void Event_PrepareMeal()
    {
        presentToaster.SetEnabled();
        Debug.Log("The toaster is ready for you");
    }

    void Event_ReciepeLearnt()
    {
        presentFridge.SetEnabled();
        presentMicrowave.SetEnabled();
        presentPantry.SetEnabled();
        Debug.Log("You can know make PBJ");
    }

    void Event_CheckBrokenBank()
    {
        Debug.Log("Money well spent");
        playerPainting.SetEnabled(); // Maybe use set visible instead.
        EndGame.IncCounter();
    }

    void Event_ChangeBankSprite()
    {
        Debug.Log("Change future bank sprite to broken");
        presentPiggyBank.SetEnabled();
    }

    void Event_BreakPiggy()
    {
        Debug.Log("Ready to deposit money");
        pastPiggyBank.SetEnabled();
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            tricycle.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            key.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            door.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            pastPiggyBank.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            presentPiggyBank.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            coin1.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            coin2.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            coin3.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            mum.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            presentFridge.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            presentPantry.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            presentMicrowave.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            presentToaster.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cable1.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            cable2.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            cable3.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            dad.SetCollected();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            vcr.SetCollected();
        }
    }
}
