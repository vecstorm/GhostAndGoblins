using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;


public class LiveManager : MonoBehaviour
{

    public static LiveManager Instance;
    public SpawnerPlayer playerSpawner;

    public HUD hud;
    public GameObject gameOver;

    private int vidasTotales = 2;
    private int vida = 1;

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
        
        if (playerSpawner == null)
        {
  
            playerSpawner = FindObjectOfType<SpawnerPlayer>();
        }
    }
    private void Start()
    {
           playerSpawner.SpawnCharacter();
           vida = 1;         // Restaurar golpes
    }

    public void PerderVida()
    {
        vida--; // Reduce golpes

        if (vida <= 0)
        {
            vidasTotales--; // Pierde una vida completa

            if (vidasTotales > 0)
            {
                Debug.Log("Perdió una vida, recargando escena...");
                vida = 2; // Restaurar golpes para la siguiente vida
                perderVidaYReload();
                
            }
            else
            {
                Debug.Log("has perdido");
            }
        }

    }
    public void perderVidaYReload()
    {
        Debug.Log("No quedan vidas, reiniciando nivel.");
        hud.DesactivarVida(vidasTotales);
        Time.timeScale = 1; // Detiene todo el movimiento en la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia la escena actual
    }

    public void RecuperarVida()
    {
        hud.ActivarVida(vidasTotales);
        vidasTotales += 1;

    }
}
