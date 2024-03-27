using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;

    public bool bGamePlay = false;

    public bool bRacing = false;

    public bool bSlow = false;

    public float CurrentTime;

    public int CurrentStage;
    public int MaxStage = 3;

    public bool bDesertWheel, bMountainWheel, bCityWheel;

    public bool bDesertOtherItem, bMountainOtherItem, bCityOtherItem;

    public int CurrentPlayerEngineLever;
    public int MaxPlayerEngineLever = 3;

    public int CurrentMoney;
    public int[] CurrentInventorys;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        CurrentTime = 0;
    }
    void Update()
    {
        if (bRacing)
            CurrentTime += Time.deltaTime;
    }
}
