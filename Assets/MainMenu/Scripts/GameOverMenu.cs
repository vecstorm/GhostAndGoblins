using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }
}
