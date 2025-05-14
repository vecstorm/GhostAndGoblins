using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public TextMeshProUGUI highScore;
    int topScore;

    public void Start()
    {   
        getHighScore();
        highScore = GetComponent<TextMeshProUGUI>();
        
    }
    public void getHighScore()
    {
        topScore = PointColtroller.instance.getPoints();
        highScore.text = (("Your High Score: ") +topScore.ToString());
    }
    public void Menu()
    {
        HUD hud = FindObjectOfType<HUD>();  // Busca el HUD en la escena

        if (hud != null)
        {
            hud.ReiniciarHUD();  // Llama a la función dentro del HUD
        }

        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    public void TryAgain()
    {
        HUD hud = FindObjectOfType<HUD>();  // Busca el HUD en la escena

        if (hud != null)
        {
            hud.ReiniciarHUD();  // Llama a la función dentro del HUD
        }
        SceneManager.LoadScene(1);
    }

}
