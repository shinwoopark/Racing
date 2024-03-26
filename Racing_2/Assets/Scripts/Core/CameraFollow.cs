using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target;

    public float CameraSpeed;
    public float TrunSpeed;

    private Vector3 _targetPos;

    void FixedUpdate()
    {
        _targetPos = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);

        transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * CameraSpeed);

        transform.rotation = Quaternion.Lerp(transform.rotation, Target.transform.rotation, Time.deltaTime * TrunSpeed);
    }
}
