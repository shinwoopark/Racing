using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSystem : MonoBehaviour
{
    enum State {Nomall, SandStorm, Dark}
    State CurrentState;

    public CarMoveSystem CarMoveSystem;

    public Rigidbody SphereCollider;

    public float NomallSpeed;

    public float FowardPower;

    public Image SandStorm_img, Dark_img;
    public GameObject SandStormEffect_gb;

    private bool _bDesertOtherItem, _bMountainOtherItem;

    void Start()
    {
        EngineUpgrade();
    }

    void Update()
    {
        if (GameInstance.instance.bRacing)
        {
            UpdateInput();
            UpdateState();
        }        
    }

    public void EngineUpgrade()
    {
        switch (GameInstance.instance.CurrentPlayerEngineLever)
        {
            case 0:
                FowardPower = NomallSpeed;
                break;
            case 1:
                FowardPower = NomallSpeed * 1.333f;
                break;
            case 2:
                FowardPower = NomallSpeed * 1.666f;
                break;
            case 3:
                FowardPower = NomallSpeed * 2;
                break;
        }
    }

    private void UpdateInput()
    {
        //Foward
        if (Input.GetAxis("Vertical") > 0)
        {
            CarMoveSystem.InputSpeed = Input.GetAxis("Vertical") * FowardPower;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            CarMoveSystem.InputSpeed = Input.GetAxis("Vertical") * FowardPower / 2;
        }

        //Turn
        CarMoveSystem.InputTurn = Input.GetAxis("Horizontal");

        //Drift
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CarMoveSystem.InputSpeed != 0 &&
                CarMoveSystem.CurrentSpeed != 0 &&
                CarMoveSystem.bGround)
            {
                SphereCollider.AddForce(Vector3.up * 500, ForceMode.Impulse);

                CarMoveSystem.CurrentState = CarMoveSystem.State.Drift;
            }
        }

        if (CarMoveSystem.CurrentState == CarMoveSystem.State.Drift)
        {
            if (Input.GetKeyUp(KeyCode.W) ||
               Input.GetKeyUp(KeyCode.A) ||
               Input.GetKeyUp(KeyCode.S) ||
               Input.GetKeyUp(KeyCode.Space) ||
               Input.GetKeyUp(KeyCode.UpArrow) ||
               Input.GetKeyUp(KeyCode.LeftArrow) ||
               Input.GetKeyUp(KeyCode.RightArrow) ||
               CarMoveSystem.CurrentSpeed <= 0)
            {
                CarMoveSystem.CurrentState = CarMoveSystem.State.Move;
            }
        }

        //Booster
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (GameInstance.instance.CurrentInventorys[0] != 0)
            {
                CarMoveSystem.BoosterNumber = GameInstance.instance.CurrentInventorys[0];
                CarMoveSystem.CurrentState = CarMoveSystem.State.Booster;
                GameInstance.instance.CurrentInventorys[0] = GameInstance.instance.CurrentInventorys[1];
                GameInstance.instance.CurrentInventorys[1] = 0;
            }
        }

        //OtherItmes
        if (Input.GetKey(KeyCode.Z))
        {
            if (GameInstance.instance.bDesertOtherItem)
            {
                if (_bDesertOtherItem)
                    _bDesertOtherItem= false;
                else if (!_bDesertOtherItem)
                    _bDesertOtherItem = true;
            }
        }

        if (Input.GetKey(KeyCode.X))
        {
            if (GameInstance.instance.bMountainOtherItem)
            {
                if (_bMountainOtherItem)
                    _bMountainOtherItem = false;
                else if (!_bMountainOtherItem)
                    _bMountainOtherItem = true;
            }
        }
    }

    private void UpdateState()
    {
        switch (CurrentState)
        {
            case State.Nomall:
                SandStormEffect_gb.SetActive(false);

                if (SandStorm_img.color.a > 0)
                {
                    SandStorm_img.color -= new Color(0, 0, 0, 30 * Time.deltaTime);

                    if (SandStorm_img.color.a < 0)
                        SandStorm_img.color = new Color(0, 0, 0, 0);
                }

                if (Dark_img.color.a > 0)
                {
                    Dark_img.color -= new Color(0, 0, 0, 30 * Time.deltaTime);

                    if(Dark_img.color.a < 0)
                            Dark_img.color = new Color(0, 0, 0, 0);
                }
                break;
            case State.SandStorm:
                SandStormEffect_gb.SetActive(true);
                if (_bDesertOtherItem)
                {
                    if (SandStorm_img.color.a > 0)
                    {
                        SandStorm_img.color -= new Color(0, 0, 0, 30 * Time.deltaTime);

                        if (SandStorm_img.color.a > 0.95f)
                            SandStorm_img.color = new Color(0, 0, 0, 242.25f);
                    }
                }
                else
                {
                    if (SandStorm_img.color.a < 0.95f)
                    {
                        SandStorm_img.color += new Color(0, 0, 0, 30 * Time.deltaTime);

                        if (SandStorm_img.color.a < 0)
                            SandStorm_img.color = new Color(0, 0, 0, 0);
                    }
                }              
                break;
            case State.Dark:
                if (_bMountainOtherItem)
                {
                    if (Dark_img.color.a > 0)
                    {
                        Dark_img.color -= new Color(0, 0, 0, 100 * Time.deltaTime);

                        if (Dark_img.color.a > 0.95f)
                            Dark_img.color = new Color(0, 0, 0, 242.25f);
                    }
                }
                else
                {
                    if (Dark_img.color.a < 0.95f)
                    {
                        Dark_img.color += new Color(0, 0, 0, 100 * Time.deltaTime);

                        if (Dark_img.color.a < 0)
                            Dark_img.color = new Color(0, 0, 0, 0);
                    }
                }            
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag )
        {
            case "Finish":
                GameManager.instance.EndRacing(true);
                break;
            case "SandStorm":
                CurrentState = State.SandStorm;
                break;
            case "Dark":
                CurrentState = State.Dark;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "SandStorm":
                CurrentState = State.Nomall;
                break;
            case "Dark":
                CurrentState = State.Nomall;
                break;
        }
    }
}
