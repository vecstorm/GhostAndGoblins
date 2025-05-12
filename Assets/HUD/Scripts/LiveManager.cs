using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;


public class LiveManager : MonoBehaviour
{

    public static LiveManager Instance;

    public HUD hud;
    public GameObject gameOver;

    private int vida = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PerderVida()
    {       
        vida -= 1;       
        hud.DesactivarVida(vida);
        /*if(vida < 0)
        {
           

            SceneManager.LoadScene(1);
            
            gameOver.SetActive(true);

        }*/
    }

    public void RecuperarVida()
    {
        hud.ActivarVida(vida);
        vida += 1;

    }
}
