using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerSystem PlayerSystem;

    public TextMeshProUGUI CurrentMoney, CurrentTime;

    [Header("Engine")]
    public Image CurrentEngine;
    public Color Yellow, Orange, Red;

    [Header("DesertOther")]
    public GameObject OnDesertOther_gb, OffDesertOther_gb;

    [Header("MountainOther")]
    public GameObject OnMountainOther_gb, OffMountainOther_gb;

    [Header("CityOther")]
    public GameObject OnCityOther_gb, OffCityOther_gb;

    void Start()
    {
        
    }

    void Update()
    {
        CurrentMoney.text = GameInstance.instance.CurrentMoney.ToString("N0");
        CurrentTime.text = GameInstance.instance.CurrentTime.ToString("F2");

        switch (GameInstance.instance.CurrentPlayerEngineLever)
        {
            case 1:
                CurrentEngine.color = Yellow;
                break;
            case 2:
                CurrentEngine.color = Orange;
                break;
            case 3:
                CurrentEngine.color = Red;
                break;
        }

        if (GameInstance.instance.bDesertOtherItem)
        {
            if (PlayerSystem.bDesertOtherItem)
            {
                OnDesertOther_gb.SetActive(true);
                OffDesertOther_gb.SetActive(false);
            }
            else
            {
                OnDesertOther_gb.SetActive(false);
                OffDesertOther_gb.SetActive(true);
            }
        }
        if (GameInstance.instance.bMountainOtherItem)
        {
            if (PlayerSystem.bMountainOtherItem)
            {
                OnMountainOther_gb.SetActive(true);
                OffMountainOther_gb.SetActive(false);
            }
                
            else
            {
                OnMountainOther_gb.SetActive(false);
                OffMountainOther_gb.SetActive(true);
            }
                
        }            
        //if (GameInstance.instance.bCityOtherItem)
        //{
        //    OffCityOther_gb.SetActive(true);
        //}                  
    }
}
