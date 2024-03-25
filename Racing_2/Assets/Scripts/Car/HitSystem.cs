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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            CarMoveSystem.CurrentSpeed = 0;
            CarMoveSystem.InputSpeed = 0;

            Vector3 direction = (other.transform.position - transform.position).normalized;

            SphereCollider.AddForce(new Vector3(-direction.x, -direction.y, -direction.z) * Knockback, ForceMode.Impulse);         
        }

        if (other.gameObject.layer == 11)
        {
            CarMoveSystem.CurrentSpeed = 0;
            CarMoveSystem.InputSpeed = 0;

            Vector3 direction = (other.transform.position - transform.position).normalized;

            SphereCollider.AddForce(new Vector3(-direction.x, -direction.y, -direction.z) * Knockback * 2, ForceMode.Impulse);
        }
    }
}
