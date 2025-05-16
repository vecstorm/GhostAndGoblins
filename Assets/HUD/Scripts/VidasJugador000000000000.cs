using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class VidasJugador000000000000 : MonoBehaviour
{
    public Image[] vidas;

    public int vidaMaxima = 3;
    private int vidaActual;

    audioManagerScript audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManagerScript>();
    }

    private void Start()
    {
        // Buscar todas las im�genes con el tag "Vida" en la escena
        GameObject[] imagenesDeVidaObj = GameObject.FindGameObjectsWithTag("Vida");

        // Asignar las im�genes encontradas al array 'vidas'
        vidas = new Image[imagenesDeVidaObj.Length];
        for (int i = 0; i < imagenesDeVidaObj.Length; i++)
        {
            // Aseg�rate de que cada GameObject tenga un componente Image
            vidas[i] = imagenesDeVidaObj[i].GetComponent<Image>();
        }

        // Asignar la vida m�xima
        vidaActual = vidaMaxima;
        ActualizarInterfaz();
    }

    void ActualizarInterfaz()
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            vidas[i].enabled = i < vidaActual;
        }
        if (vidaActual <= 0)
        {
            EscenaGameOver();
        }
    }

    void EscenaGameOver()
    {
        
        SceneManager.LoadScene("GameOver");
    }


    public void RecibirDano(int cantidadDano)
    {
        vidaActual -= cantidadDano;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        Debug.Log("quitamos vidas");
        Debug.Log(vidaActual);
        ActualizarInterfaz();
    }

    public void ObtenerVida(int CuraTotal)
    {
        vidaActual += CuraTotal;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarInterfaz();
    }
}
