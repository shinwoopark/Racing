using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;

    public Rigidbody SphereCollider;

    public Transform WayPoint;
    private int _wayPointNumber;

    public float FowardPower;
    public float RotationSpeed;

    [Header("Evasion")]
    public LayerMask CarCheck;
    public float SideSpeed;
    public float RayLenth;

    bool leftSafe = true;
    bool rightSafe = true;

    void Start()
    {
        _wayPointNumber = 0;
    }

    private void FixedUpdate()
    {
        if (GameInstance.instance.bRacing)
        {
            UpdateFolloWayPoint();
            UpdateEvasion();
        }          
    }

    private void UpdateFolloWayPoint()
    {
        CarMoveSystem.InputSpeed = FowardPower;

        Vector3 dir = WayPoint.GetChild(_wayPointNumber).transform.position - transform.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * RotationSpeed);
    }

    private void UpdateEvasion()
    {
        RaycastHit Hardlehit;
        RaycastHit Hardlehit1;
        RaycastHit Hardlehit2;

        int dir = 2;

        Debug.DrawRay(transform.position, transform.forward * RayLenth, Color.red);
        Debug.DrawRay(transform.position, transform.right * RayLenth, Color.red);
        Debug.DrawRay(transform.position, -transform.right * RayLenth, Color.red);

        if (Physics.Raycast(transform.position, -transform.right, out Hardlehit1, RayLenth, CarCheck))
            leftSafe = false;
        else if (Physics.Raycast(transform.position, transform.right, out Hardlehit2, RayLenth, CarCheck))
            rightSafe = false;         

        if (Physics.Raycast(transform.position, transform.forward, out Hardlehit, RayLenth, CarCheck))
        {
            if (leftSafe && rightSafe)
                dir = Random.Range(0, 2);
            else if (!leftSafe && !rightSafe)
                dir = Random.Range(0, 2);
            else if(leftSafe && !rightSafe)
                dir = 1;
            else if (!leftSafe && rightSafe)
                dir = 0;

            if (dir == 0)
                SphereCollider.AddForce(Vector3.left * SideSpeed, ForceMode.Impulse);
            if (dir == 1)
                SphereCollider.AddForce(Vector3.right * SideSpeed, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == WayPoint.GetChild(_wayPointNumber))
        {
            _wayPointNumber++;
        }

        if (other.gameObject.tag == "Finish")
        {
            GameManager.instance.EndRacing(false);
        }
    }
}
