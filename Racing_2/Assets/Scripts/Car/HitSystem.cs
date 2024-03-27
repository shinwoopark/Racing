using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSystem : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;

    public Rigidbody SphereCollider;

    public float Knockback;

    public AudioSource Hit;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finish")
        {
            if (gameObject.tag == "Player")
                GameManager.instance.StartCoroutine(GameManager.instance.EndRacing(true));
            else if(gameObject.tag == "Enemy")
                GameManager.instance.StartCoroutine(GameManager.instance.EndRacing(false));
        }

        if (other.gameObject.layer == 3 || other.gameObject.layer == 11)
        {
            CarMoveSystem.CurrentSpeed = 0;
            CarMoveSystem.InputSpeed = 0;

            Vector3 direction = (other.transform.position - transform.position).normalized;

            SphereCollider.AddForce(new Vector3(-direction.x, -direction.y, -direction.z) * Knockback, ForceMode.Impulse);         
        }

        if (gameObject.tag == "Player")
        {
            if (other.gameObject.layer == 3 || other.gameObject.layer == 10)     
                Hit.Play();             
        }            
    }
}
