using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSystem : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;

    public Rigidbody SphereCollider;

    private float _nomallSpeed;

    private float _fowardPower;

    public Image SandStorm_img;
    public GameObject SandStormEffect_gb;

    public bool bDesertOtherItem, bMountainOtherItem;

    private bool _bSandStorm;

    void Start()
    {
        EngineUpgrade();
    }

    void Update()
    {
        if (GameInstance.instance.bRacing)
        {
            UpdateInput();
        }
    }

    private void FixedUpdate()
    {
        if (GameInstance.instance.bRacing)
        {
            UpdateState();
        }
    }

    public void EngineUpgrade()
    {
        _nomallSpeed = 8000;
        switch (GameInstance.instance.CurrentPlayerEngineLever)
        {
            case 0:
                _fowardPower = _nomallSpeed;
                break;
            case 1:
                _fowardPower = _nomallSpeed * 1.333f;
                break;
            case 2:
                _fowardPower = _nomallSpeed * 1.666f;
                break;
            case 3:
                _fowardPower = _nomallSpeed * 2;
                break;
        }
    }

    private void UpdateInput()
    {
        //Foward
        if (Input.GetAxis("Vertical") > 0)
        {
            CarMoveSystem.InputSpeed = Input.GetAxis("Vertical") * _fowardPower;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            CarMoveSystem.InputSpeed = Input.GetAxis("Vertical") * _fowardPower / 2;
        }

        //Turn
        CarMoveSystem.InputTurn = Input.GetAxis("Horizontal");

        //Drift
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CarMoveSystem.InputSpeed != 0 &&
                CarMoveSystem.InputTurn != 0 &&
                CarMoveSystem.CurrentSpeed >= 0 &&
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (GameInstance.instance.CurrentInventorys[0] != 0)
            {
                CarMoveSystem.CurrentState = CarMoveSystem.State.Booster;

                switch (GameInstance.instance.CurrentInventorys[0])
                {
                    case 1:
                        CarMoveSystem.BoosterSpeed = 1.75f * _fowardPower;
                        CarMoveSystem.BoosterTime = 1.5f;
                        break;
                    case 2:
                        CarMoveSystem.BoosterSpeed = 2.5f * _fowardPower;
                        CarMoveSystem.BoosterTime = 2.5f;
                        break;
                }

                GameInstance.instance.CurrentInventorys[0] = GameInstance.instance.CurrentInventorys[1];
                GameInstance.instance.CurrentInventorys[1] = 0;
            }
        }

        //OtherItmes
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (GameInstance.instance.bDesertOtherItem)
            {
                if (bDesertOtherItem)
                    bDesertOtherItem= false;
                else if (!bDesertOtherItem)
                    bDesertOtherItem = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (GameInstance.instance.bMountainOtherItem)
            {
                if (CarMoveSystem.bGlider)
                    CarMoveSystem.bGlider = false;
                else if (!CarMoveSystem.bGlider)
                    CarMoveSystem.bGlider = true;
            }
        }
    }

    private void UpdateState()
    {
        if (_bSandStorm)
        {
            SandStormEffect_gb.SetActive(true);
            if (bDesertOtherItem)
            {
                SandStorm_img.color -= new Color(0, 0, 0, 0.2f * Time.deltaTime);

                if (SandStorm_img.color.a < 0)
                    SandStorm_img.color = new Color(0, 0, 0, 0);
            }
            else
            {
                SandStorm_img.color += new Color(0, 0, 0, 0.2f * Time.deltaTime);
               
                if (SandStorm_img.color.a > 0.95f)
                    SandStorm_img.color = new Color(0, 0, 0, 0.95f);
            }
        }
        else
        {
            SandStormEffect_gb.SetActive(false);

            SandStorm_img.color -= new Color(0, 0, 0, 0.2f * Time.deltaTime);

            if (SandStorm_img.color.a < 0)
                SandStorm_img.color = new Color(0, 0, 0, 0);
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
                _bSandStorm = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "SandStorm":
                _bSandStorm = false;
                break;
        }
    }
}
