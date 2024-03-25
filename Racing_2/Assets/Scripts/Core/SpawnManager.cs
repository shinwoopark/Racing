using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public GameObject SuperPolice_gb, Police_gb, Bus_gb, Truck_gb;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Spawn(int number, Vector3 spawnPos, float rototin)
    {
        GameObject AI = new GameObject();

        if (number == 0)
            number = Random.Range(1, 5);

        switch (number)
        {   
            case 1:
                AI = SuperPolice_gb;
                break;
            case 2:
                AI = Police_gb;
                break;
            case 3:
                AI = Bus_gb;
                break;
            case 4:
                AI = Truck_gb;
                break;
        }
        Instantiate(AI, spawnPos, Quaternion.Euler(0, rototin, 0));
    }
}
