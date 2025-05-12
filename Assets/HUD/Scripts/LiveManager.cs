using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;


public class LiveManager : MonoBehaviour
{

    public static LiveManager Instance;

    public HUD hud;

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
        if (vida < 0)
        {
            SceneManager.LoadScene(1);
            vida = 3;
        }
        hud.DesactivarVida(vida);
    }

    public void RecuperarVida()
    {
        hud.ActivarVida(vida);
        vida += 1;

        
    }
}
