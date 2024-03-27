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

    public AudioSource EngineFoward, BackEngine, Drift, Booster, GetBoosterItem, GetMoneyItem, GetShopItem, SandStorm;
    private bool _bEngineFoward, _bBackEngine;

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

            if (Time.timeScale != 1)
            {
                EngineFoward.Stop();
                _bEngineFoward = false;
            }

            if (!_bEngineFoward && Time.timeScale != 0)
            {
                _bEngineFoward = true;
                _bBackEngine = false;

                EngineFoward.Play();
                BackEngine.Stop();

                if (EngineFoward.pitch < 2)
                    EngineFoward.pitch += Input.GetAxis("Vertical") * Time.deltaTime;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            CarMoveSystem.InputSpeed = Input.GetAxis("Vertical") * _fowardPower / 2;

            if (Time.timeScale != 1)
            {
                BackEngine.Stop();
                _bBackEngine = false;
            }

            if (!_bBackEngine && Time.timeScale != 0)
            {
                _bEngineFoward = false;
                _bBackEngine = true;

                EngineFoward.Stop();
                BackEngine.Play();


                EngineFoward.pitch = 1;
            }          
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

                if (Time.timeScale != 0)
                    Drift.Play();
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

                if (Time.timeScale != 1)
                    Drift.Stop();
            }
        }

        //Booster
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (GameInstance.instance.CurrentInventorys[0] != 0)
            {
                CarMoveSystem.CurrentState = CarMoveSystem.State.Booster;

                if (Time.timeScale != 0)
                    Booster.Play();

                if (Time.timeScale != 1)
                    Booster.Stop();

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
                CarMoveSystem.bGlider = true;
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

            if (Time.timeScale != 0)
                SandStorm.Play();

            if (Time.timeScale != 1)
                SandStorm.Stop();

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
                SandStorm.Stop();

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
            case "Item":
                switch (other.gameObject.layer)
                {
                    case 12:
                        GetBoosterItem.Play();
                        break;
                    case 13:
                        GetMoneyItem.Play();
                        break;
                    case 14:
                        GetShopItem.Play();
                        break;
                }
                
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
