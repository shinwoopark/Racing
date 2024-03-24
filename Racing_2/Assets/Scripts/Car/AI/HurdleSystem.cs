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

    void Start()
    {
        Player_gb = GameObject.Find("Player").GetComponent<GameObject>();
        Player_tr = Player_gb.GetComponent<Transform>();
    }

    void Update()
    {
        UpdateFollowPlayer();
    }

    private void UpdateFollowPlayer()
    {      
        CarMoveSystem.InputSpeed = Input.GetAxis("Vertical") * FowardPower;

        Vector3 dir = transform.position - Player_tr.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * RotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            FowardPower = 0;
            RotationSpeed = 0;
        }
    }
}
