using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class HUD : MonoBehaviour
{

    public GameObject[] vidas;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DesactivarVida(int indice)
    {
        vidas[indice].SetActive(false);
    }

    public void ActivarVida(int indice)
    {
        vidas[indice].SetActive(true);
    }

    //public void ActualizarPuntos(int puntosTotales)
    //{
    //    puntosTotales.text = puntosTotales.ToString;
    //}
}
