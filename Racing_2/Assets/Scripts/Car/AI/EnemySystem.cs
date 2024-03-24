using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;

    public Rigidbody SphereCollider;

    public GameObject[] WayPoints_gb;
    private int _wayPointNumber;

    public float FowardPower;
    public float RotationSpeed;

    [Header("Evasion")]
    public LayerMask CarCheck;
    public float SideSpeed;
    public float RayLenth;

    void Start()
    {
        
    }

    void Update()
    {
        if (GameInstance.instance.bRacing)
        {
            UpdateFolloWayPoint();
            UpdateEvasion();
        }          
    }

    private void UpdateFolloWayPoint()
    {
        CarMoveSystem.InputSpeed = Input.GetAxis("Vertical") * FowardPower;

        Vector3 dir = WayPoints_gb[_wayPointNumber].transform.position - transform.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * RotationSpeed);
    }

    private void UpdateEvasion()
    {
        RaycastHit Hardlehit;

        Debug.DrawRay(transform.position, transform.forward * RayLenth, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out Hardlehit, RayLenth, CarCheck))
        {
            int dir = Random.Range(0, 2);

            if (dir == 0)
                SideSpeed *= -1;

            SphereCollider.AddForce(Vector3.right * SideSpeed, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == WayPoints_gb[_wayPointNumber])
        {
            _wayPointNumber++;
        }

        if (other.gameObject.tag == "Finish")
        {
            GameManager.instance.EndRacing(false);
        }
    }
}
