using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void onClickPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void onClickCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void onClickExit()
    {
        Application.Quit();
    }

    public void onClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
