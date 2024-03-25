using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleSystem : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;

    public float FowardPower;
    public float RotationSpeed;

    public GameObject Player_gb;
    public Transform Player_tr;

    public bool bFindPlayer;

    void Start()
    {
        Player_gb = GameObject.Find("Player");
        Player_tr = Player_gb.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        UpdateMove();
    }

    private void UpdateMove()
    {
        CarMoveSystem.InputSpeed = FowardPower;

        Vector3 dir = new Vector3(0, 0, 0);

        if (bFindPlayer)
        {
            dir = transform.position - Player_tr.position;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-dir), Time.deltaTime * RotationSpeed);
        }            
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            if (bFindPlayer)
            {
                FowardPower = 0;
                RotationSpeed = 0;
            }              
        }
    }
}
