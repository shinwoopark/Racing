using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlowDown : MonoBehaviour
{
    public Image[] SlowDowns;

    public bool[] bDowns;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateSlowDown();
    }

    public void UpdateSlowDown()
    {
        for (int i = 0; i < SlowDowns.Length; i++)
        {
            if(SlowDowns[i].color.a <= 0.1)
                bDowns[i] = false;              
            else if (SlowDowns[i].color.a >= 0.9)
                bDowns[i] = true;

            if (bDowns[i])
                SlowDowns[i].color -= new Color(0, 0, 0, 0.5f * Time.deltaTime);
            else
                SlowDowns[i].color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
        }
    }
}
