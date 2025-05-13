using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidasJugador000000000000 : MonoBehaviour
{
    public Image[] vidas;

    public int VidaMaxima = 3;
    private int VidaActual;

    private void Start()
    {
        VidaActual = VidaMaxima;
        actualizarInterfaz();
    }

    void actualizarInterfaz()
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            vidas[i].enabled = i < VidaActual;
        }
        if (VidaActual <= 0)
        {
            ReiniciarEscena();
        }
    }

    void ReiniciarEscena()
    {
        int currectSceneIncex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currectSceneIncex);
    }

    public void RecibirDano(int cantidadDano)
    {
        VidaActual -= cantidadDano;
        VidaActual = Mathf.Clamp(VidaActual, 0, VidaMaxima);
        actualizarInterfaz();
    }

    public void ObtenerVida(int CuraTotal)
    {
        VidaActual += CuraTotal;
        VidaActual = Mathf.Clamp(VidaActual, 0, VidaMaxima);
        actualizarInterfaz();
    }
}
