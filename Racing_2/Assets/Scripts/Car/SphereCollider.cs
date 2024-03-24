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
        if (collision.gameObject.tag == "Ground")
        {
            CarMoveSystem.bGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            CarMoveSystem.bGround = false;
        }
    }
}
