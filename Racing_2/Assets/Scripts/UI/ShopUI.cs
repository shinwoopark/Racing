using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public PlayerSystem PlayerSystem;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void BuyDesertWheel()
    {
        if (GameInstance.instance.CurrentMoney >= 3000000)
        {
            GameInstance.instance.CurrentMoney -= 3000000;
            GameInstance.instance.bDesertWheel = true;
        }     
    }

    public void BuyMountainWheel()
    {
        if (GameInstance.instance.CurrentMoney >= 3000000)
        {
            GameInstance.instance.CurrentMoney -= 3000000;
            GameInstance.instance.bMountainWheel = true;
        }
            
    }

    public void BuyCityWheel()
    {
        if (GameInstance.instance.CurrentMoney >= 3000000)
        {
            GameInstance.instance.CurrentMoney -= 3000000;
            GameInstance.instance.bCityWheel = true;
        }
    }

    public void BuyEngine1()
    {
        if (GameInstance.instance.CurrentMoney >= 3000000)
        {
            GameInstance.instance.CurrentMoney -= 3000000;
            GameInstance.instance.CurrentPlayerEngineLever++;
            PlayerSystem.EngineUpgrade();
        }         
    }

    public void BuyEngine2()
    {
        if (GameInstance.instance.CurrentMoney >= 5000000)
        {
            GameInstance.instance.CurrentMoney -= 5000000;
            GameInstance.instance.CurrentPlayerEngineLever++;
            PlayerSystem.EngineUpgrade();
        }
            
    }

    public void BuyEngine3()
    {
        if (GameInstance.instance.CurrentMoney >= 10000000)
        {
            GameInstance.instance.CurrentMoney -= 10000000;
            GameInstance.instance.CurrentPlayerEngineLever++;
            PlayerSystem.EngineUpgrade();
        }
           
    }

    public void BuyDesertOherItem()
    {
        if (GameInstance.instance.CurrentMoney >= 3000000)
        {
            GameInstance.instance.CurrentMoney -= 3000000;
            GameInstance.instance.bDesertOtherItem = true;
        }           
    }

    public void BuyMounainOherItem()
    {
        if (GameInstance.instance.CurrentMoney >= 4000000)
        {
            GameInstance.instance.CurrentMoney -= 4000000;
            GameInstance.instance.bDesertOtherItem = true;
        }          
    }

    public void BuyCityOherItem()
    {
        if (GameInstance.instance.CurrentMoney >= 5000000)
        {
            GameInstance.instance.CurrentMoney -= 5000000;
            GameInstance.instance.bDesertOtherItem = true;
        }
            
    }
}
