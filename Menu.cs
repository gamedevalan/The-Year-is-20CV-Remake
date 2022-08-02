using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject aboutInfo;
    public GameObject creditInfo;
    public GameObject optionInfo;

    public static bool endingScreenShow;
    public GameObject endingScreen;

    private void Start()
    {
        if (endingScreen != null) {
            endingScreen.SetActive(endingScreenShow);
        }
    }

    public void BeginGame()
    {
        SceneManager.LoadScene(GameManager.lastScene);
    }

    public void ShowAbout()
    {
        aboutInfo.SetActive(true);
    }

    public void ShowCredit()
    {
        creditInfo.SetActive(true);
    }

    public void ShowOption()
    {
        optionInfo.SetActive(true);
    }

    public void HideAbout()
    {
        aboutInfo.SetActive(false);
    }

    public void HideCredit()
    {
        creditInfo.SetActive(false);
    }

    public void HideOption()
    {
        optionInfo.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToEndScreen()
    {
        SceneManager.LoadScene("End");
    }
}
