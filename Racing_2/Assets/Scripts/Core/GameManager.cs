using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject GameClearUI, GameOverUI, ShopUI;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            instance = this;
        }
    }
    private void Start()
    {
        StartRacing();
    }

    private IEnumerator StartRacing()
    {
        yield return new WaitForSeconds(5);

        yield return new WaitForSeconds(1);

        yield return new WaitForSeconds(1);

        yield return new WaitForSeconds(1);
        GameInstance.instance.bRacing = true;
    }

    public IEnumerator EndRacing(bool bplayer)
    {
        GameInstance.instance.bRacing = false;

        yield return new WaitForSeconds(2);

        if (bplayer)
        {
            GameClearUI.SetActive(true);
        }
        else
        {
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

        Time.timeScale = 0.1f;
    }

    public void CloseShop()
    {
        ShopUI.SetActive(false);

        Time.timeScale = 1;
    }
}
