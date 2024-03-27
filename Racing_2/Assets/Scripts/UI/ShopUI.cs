using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public PlayerSystem PlayerSystem;

    public AudioSource Buy;

    public LockOn[] LockOns;
    public GameObject[] SoldOut;
    public bool[] bLockOff;
    public bool[] bSoldOut;

    void Start()
    {
        for (int i = 0; i < bSoldOut.Length; i++)
        {
            bSoldOut[i] = false; 
        }
    }

    void Update()
    {
        for (int i = 0; i < SoldOut.Length; i++)
        {
            if (bSoldOut[i])
                SoldOut[i].SetActive(true);
        }

        for (int i = 0; i < LockOns.Length; i++)
        {
            if (bLockOff[i])
            {
                Debug.Log("!");
                LockOns[i].bLockOff = true;
            }
                
        }
    }

    public void BuyDesertWheel()
    {
        if (GameInstance.instance.CurrentMoney >= 3000000 && !bSoldOut[3])
        {
            Buy.Play();
            GameInstance.instance.CurrentMoney -= 3000000;
            GameInstance.instance.bDesertWheel = true;
            bSoldOut[3] = true;
        }     
    }

    public void BuyMountainWheel()
    {
        if (GameInstance.instance.CurrentMoney >= 3000000 && !bSoldOut[4])
        {
            Buy.Play();
            GameInstance.instance.CurrentMoney -= 3000000;
            GameInstance.instance.bMountainWheel = true;
            bSoldOut[4] = true;
        }
            
    }

    public void BuyCityWheel()
    {
        if (GameInstance.instance.CurrentMoney >= 3000000 && !bSoldOut[5])
        {
            Buy.Play();
            GameInstance.instance.CurrentMoney -= 3000000;
            GameInstance.instance.bCityWheel = true;
            bSoldOut[5] = true;
        }
    }

    public void BuyEngine1()
    {
        if (GameInstance.instance.CurrentMoney >= 3000000 && !bSoldOut[0])
        {
            Buy.Play();
            GameInstance.instance.CurrentMoney -= 3000000;
            GameInstance.instance.CurrentPlayerEngineLever++;
            PlayerSystem.EngineUpgrade();
            bSoldOut[0] = true;
            bLockOff[0] = true;
        }         
    }

    public void BuyEngine2()
    {
        if (!bLockOff[0])
            return;
        if (GameInstance.instance.CurrentMoney >= 6000000 && !bSoldOut[1])
        {
            Buy.Play();
            GameInstance.instance.CurrentMoney -= 6000000;
            GameInstance.instance.CurrentPlayerEngineLever++;
            PlayerSystem.EngineUpgrade();
            bSoldOut[1] = true;
            bLockOff[1] = true;
        }            
    }

    public void BuyEngine3()
    {
        if (!bLockOff[1])
            return;
        if (GameInstance.instance.CurrentMoney >= 9000000 && !bSoldOut[2])
        {
            Buy.Play();
            GameInstance.instance.CurrentMoney -= 9000000;
            GameInstance.instance.CurrentPlayerEngineLever++;
            PlayerSystem.EngineUpgrade();
            bSoldOut[2] = true;         
        }           
    }

    public void BuyDesertOherItem()
    {
        if (GameInstance.instance.CurrentMoney >= 3000000 && !bSoldOut[6])
        {
            Buy.Play();
            GameInstance.instance.CurrentMoney -= 3000000;
            GameInstance.instance.bDesertOtherItem = true;
            bSoldOut[6] = true;
        }           
    }

    public void BuyMounainOherItem()
    {
        if (GameInstance.instance.CurrentMoney >= 4000000 && !bSoldOut[7])
        {
            Buy.Play();
            GameInstance.instance.CurrentMoney -= 4000000;
            GameInstance.instance.bMountainOtherItem = true;
            bSoldOut[7] = true;
        }          
    }

    public void BuyCityOherItem()
    {
        if (GameInstance.instance.CurrentMoney >= 5000000 && !bSoldOut[8])
        {
            Buy.Play();
            GameInstance.instance.CurrentMoney -= 5000000;
            GameInstance.instance.bCityOtherItem = true;
            bSoldOut[8] = true;
        }         
    }
}
