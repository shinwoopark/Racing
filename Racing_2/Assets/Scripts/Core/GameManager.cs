using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject GameClearUI, GameOverUI, ShopUI;

    public AudioSource RacingStart, ReadyEngine, Win, lose;

    public TextMeshProUGUI RecodeTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }
    }
    private void Start()
    {
        StartCoroutine(StartRacing());
    }

    private IEnumerator StartRacing()
    {
        yield return new WaitForSeconds(2);
        RacingStart.Play();
        yield return new WaitForSeconds(3);
        ReadyEngine.Stop();
        GameInstance.instance.bRacing = true;
    }

    public IEnumerator EndRacing(bool bplayer)
    {
        GameInstance.instance.bRacing = false;

        yield return new WaitForSeconds(2);

        if (bplayer)
        {
            Win.Play();
            GameClearUI.SetActive(true);
            RecodeTime.text = GameInstance.instance.CurrentTime.ToString("F2");
        }
        else
        {
            lose.Play();
            GameOverUI.SetActive(true);
        }
    }

    public void InputNextScene()
    {

        switch (GameInstance.instance.CurrentStage)
        {
            case 1:
                if (GameInstance.instance.CurrentStage == 1)
                {
                    SceneManager.LoadScene("Stage2");
                    GameInstance.instance.CurrentStage++;
                }
                break;
            case 2:
                if (GameInstance.instance.CurrentStage == 2)
                {
                    SceneManager.LoadScene("Stage3");
                    GameInstance.instance.CurrentStage++;
                }
                break;
            case 3:
                if (GameInstance.instance.CurrentStage == 3)
                {
                    SceneManager.LoadScene("Stage1");
                    GameInstance.instance.CurrentStage++;
                }
                break;
        }
    }

    public void InputReTry()
    {
        switch (GameInstance.instance.CurrentStage)
        {
            case 1:
                if (GameInstance.instance.CurrentStage == 1)
                {
                    SceneManager.LoadScene("Stage1");
                }
                break;
            case 2:
                if (GameInstance.instance.CurrentStage == 2)
                {
                    SceneManager.LoadScene("Stage2");
                }
                break;
            case 3:
                if (GameInstance.instance.CurrentStage == 3)
                {
                    SceneManager.LoadScene("Stage3");
                }
                break;
        }
    }

    public void OpenShop()
    {
        ShopUI.SetActive(true);

        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        ShopUI.SetActive(false);

        Time.timeScale = 1;
    }
}
