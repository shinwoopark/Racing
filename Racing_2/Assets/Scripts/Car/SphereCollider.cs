using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollider : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            CarMoveSystem.bGround = true;

            if (collision.gameObject.tag == "Pool")
                CarMoveSystem.CurrentSpeed /= 2;

            if (gameObject.tag == "Player")
            {
                switch (collision.gameObject.tag)
                {
                    case "DesertTrap":
                        if (!GameInstance.instance.bDesertWheel)
                            CarMoveSystem.CurrentSpeed /= 2;
                        break;
                    case "MountainTrap":
                        if (!GameInstance.instance.bMountainWheel)
                            CarMoveSystem.CurrentSpeed /= 2;
                        break;
                    case "CityTrap":
                        if (!GameInstance.instance.bCityWheel)
                            CarMoveSystem.CurrentSpeed /= 2;
                        break;
                }
            }
            else
                CarMoveSystem.CurrentSpeed /= 2;
        }      
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            CarMoveSystem.bGround = false;

             if (collision.gameObject.tag == "Pool")
            CarMoveSystem.CurrentSpeed *= 2;

        if (gameObject.tag == "Player")
        {
            switch (collision.gameObject.tag)
            {
                case "DesertTrap":
                    if (!GameInstance.instance.bDesertWheel)
                        CarMoveSystem.CurrentSpeed *= 2;
                    break;
                case "MountainTrap":
                    if (!GameInstance.instance.bMountainWheel)
                        CarMoveSystem.CurrentSpeed *= 2;
                    break;
                case "CityTrap":
                    if (!GameInstance.instance.bCityWheel)
                        CarMoveSystem.CurrentSpeed *= 2;
                    break;
            }
        }
        else
            CarMoveSystem.CurrentSpeed *= 2;
        }
    }
}
