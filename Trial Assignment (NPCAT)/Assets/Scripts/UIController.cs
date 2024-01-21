using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void EnterReductor()
    {
        SceneManager.LoadScene("Reductor");
    }
    public void ExitReductor()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void ExitApplication()
    {
        Application.Quit();
    }
}
