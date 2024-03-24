using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSystem : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;

    public Rigidbody SphereCollider;

    public float Knockback;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (gameObject.tag == "Player")
            {
                switch (collision.gameObject.tag)
                {
                    case "DesertTrap":
                        if(!GameInstance.instance.bDesertWheel)
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
            {
                CarMoveSystem.CurrentSpeed /= 2;
            }
        }       
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;

            SphereCollider.AddForce(new Vector3(-direction.x, -direction.y, -direction.z) * Knockback, ForceMode.Impulse);
        }
    }
}
