using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoveSystem : MonoBehaviour
{
    public enum State {Move, Drift, Booster }
    public State CurrentState;

    public Rigidbody SphereCollider;

    public float FowardPower;

    public float CurrentSpeed;

    public float InputSpeed;
    public float InputTurn;

    public bool bGround;

    private float _slowDown;

    private float _turnAmount;

    public float BoosterNumber;
    public float BoosterTime;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateMove();

        CurrentSpeed = InputSpeed * _slowDown;
    } 

    private void UpdateMove()
    {
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
            //if (CurrentState == State.Booster)
            //{
                //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, InputTurn * _turnAmount * Time.deltaTime, 0));
            //}
            //else
            //{
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, InputTurn * _turnAmount * Time.deltaTime * Input.GetAxis("Vertical"), 0));
            //}
        }

        //Drift
        if (CurrentState == State.Drift)
        {
            float addSlowDown = 0.3f;
            float addTurnRange = 50;

            _slowDown -= addSlowDown * Time.deltaTime;

            if (_slowDown < 0)           
                _slowDown = 0;
            
            addTurnRange += addTurnRange * Time.deltaTime;
            _turnAmount += addTurnRange * Time.deltaTime;

            if (_turnAmount >= 120)            
                _turnAmount = 120;
            
        }

        //Booster
        if(CurrentState == State.Booster)
        {
            switch(BoosterNumber)
            {
                case 1:
                    CurrentSpeed *= 1.5f;
                    break;
                case 2:
                    CurrentSpeed *= 2;
                    break;
            }
            if (BoosterTime != 0)
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
}
