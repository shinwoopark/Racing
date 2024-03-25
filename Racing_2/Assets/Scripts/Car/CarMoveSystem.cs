using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoveSystem : MonoBehaviour
{
    [HideInInspector]
    public enum State {Move, Drift, Booster}
    //[HideInInspector]
    public State CurrentState;

    public Rigidbody SphereCollider;

    public Transform LeftFrontWheel, RightFrontWheel;
    public Transform[] Wheels;

    public LayerMask CheckGround;
    public float RayLenth;
    //[HideInInspector]
    public float CurrentSpeed;

    //[HideInInspector]
    public float InputSpeed;
    //HideInInspector]
    public float InputTurn;

    //[HideInInspector]
    public bool bGround;

    private float _driftSlowDown;

    private float _trapSlowDown;

    private float _turnAmount;

    public float BoosterSpeed;

    //[HideInInspector]
    public float BoosterTime;

    void Start()
    {
        SphereCollider.transform.parent = null;
    }

    void Update()
    {
        UpdateWheels();

        UpdateCheckGround();

        transform.position = SphereCollider.transform.position;
    }

    private void FixedUpdate()
    {
        UpdateMove();     
    }

    private void UpdateCheckGround()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, RayLenth * -transform.up, Color.blue);

        if (Physics.Raycast(transform.position, -transform.up, out hit, RayLenth, CheckGround))
        {
            Debug.Log(hit.transform.gameObject.tag);

            bGround = true;

            if (hit.transform.gameObject.tag == "Trap" || hit.transform.gameObject.tag == "Pool")
            {                          
                if (gameObject.tag == "Player")
                {
                    switch (GameInstance.instance.CurrentStage)
                    {
                        case 1:
                            if (!GameInstance.instance.bDesertWheel)
                                _trapSlowDown = 0.5f;
                            break;
                        case 2:
                            if (!GameInstance.instance.bMountainWheel)
                                _trapSlowDown = 0.5f;
                            break;
                        case 3:
                            if (!GameInstance.instance.bCityWheel)
                                _trapSlowDown = 0.5f;
                            break;
                    }

                    if (hit.transform.gameObject.tag == "Pool")
                    {
                        _trapSlowDown = 0.5f;
                    }
                }
                else
                    _trapSlowDown = 0.5f;
            }
            else
                _trapSlowDown = 1;
        }
        else
            bGround = false;
    }

    private void UpdateMove()
    {
        if(CurrentState == State.Move || CurrentState == State.Drift)
        {
            CurrentSpeed = InputSpeed * _driftSlowDown * _trapSlowDown;
        }

        //Foward
        if (bGround)
        {
            SphereCollider.drag = 3;
            SphereCollider.AddForce(transform.forward * CurrentSpeed);
        }
        else
        {
            SphereCollider.drag = 0.1f;
            SphereCollider.AddForce(transform.up * -3000);
        }

        //Turn
        if (bGround)
        {
            if (CurrentState == State.Booster)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, InputTurn * _turnAmount * Time.deltaTime, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, InputTurn * _turnAmount * Time.deltaTime * Input.GetAxis("Vertical"), 0));
            }
        }

        //Drift
        if (CurrentState == State.Drift)
        {
            float addSlowDown = 0.3f;
            float addTurnRange = 50;

            _driftSlowDown -= addSlowDown * Time.deltaTime;

            if (_driftSlowDown < 0)
                _driftSlowDown = 0;

            addTurnRange += addTurnRange * Time.deltaTime;
            _turnAmount += addTurnRange * Time.deltaTime;

            if (_turnAmount >= 120)
                _turnAmount = 120;
        }
        else
        {
            _driftSlowDown = 1;

            _turnAmount = 90;
            _turnAmount -= InputSpeed / 200;

            if (_turnAmount >= 90)
            {
                _turnAmount = 90;
            }
        }

        //Booster
        if (CurrentState == State.Booster)
        {
            CurrentSpeed = BoosterSpeed;

            if (BoosterTime > 0)
            {
                BoosterTime -= Time.deltaTime;
            }
            else
            {
                CurrentState = State.Move;
            }
        }
        else
        {
            BoosterTime = 0;
        }
    }

    private void UpdateWheels()
    {
        if (gameObject.tag == "Player")
        {
            //Foward
            for (int i = 0; Wheels.Length > i; i++)
                Wheels[i].eulerAngles += new Vector3(CurrentSpeed * Time.deltaTime, 0, 0);

            //Rotation
            Vector3 LeftFrontWheelEulerAngle = LeftFrontWheel.localRotation.eulerAngles;
            Vector3 RightFrontWheelEulerAngle = RightFrontWheel.localRotation.eulerAngles;

            float EulerYAngle = InputTurn * 45;

            LeftFrontWheel.localRotation = Quaternion.Euler(LeftFrontWheelEulerAngle.x, EulerYAngle - 180, LeftFrontWheelEulerAngle.z);
            RightFrontWheel.localRotation = Quaternion.Euler(RightFrontWheelEulerAngle.x, EulerYAngle, RightFrontWheelEulerAngle.z);
        }      
    }
}
