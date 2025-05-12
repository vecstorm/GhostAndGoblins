using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class HUD : MonoBehaviour
{
    

    public GameObject[] vidas;
    /*    public GameObject[] weapons;



        public void changeWeapon()
        {
            if (wepon)

        }*/


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
