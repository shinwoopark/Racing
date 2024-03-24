using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;

    public bool bGamePlay = false;

    public bool bRacing = true;

    public bool bSlow = false;

    public int CurrentStage;
    public int MaxStage = 3;

    public bool bDesertWheel, bMountainWheel, bCityWheel;

    public bool bDesertOtherItem, bMountainOtherItem, bCityOtherItem;

    public int CurrentPlayerEngineLever;
    public int MaxPlayerEngineLever = 3;

    public int CurrentItemInventoryCount;
    public int MaxItemInventory = 2;

    public int CurrentMoney;
    public int[] CurrentInventorys;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
